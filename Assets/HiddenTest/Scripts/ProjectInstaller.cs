using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cysharp.Threading.Tasks;
using Zenject;
using KarenKrill.UniCore.Diagnostics;
using KarenKrill.UniCore.Logging;
using KarenKrill.UniCore.StateSystem;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.UniCore.UI.Presenters;
using KarenKrill.UniCore.UI.Presenters.Abstractions;
using KarenKrill.UniCore.UI.Views;
using KarenKrill.UniCore.Utilities;
using KarenKrill.Audio;
using KarenKrill.ContentLoading;
using HiddenTest.GameFlow.Abstractions;
using HiddenTest.GameFlow;

namespace HiddenTest
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            InstallLogging();
            InstallGameFlow();
            InstallViewFactory();
            InstallPresenters();
            InstallGameConfig();
            InstallDiagnostics();
            InstallAudio();
            InstallContentLoading();
        }

        [SerializeField]
        private Transform _uiRootTransform;
        [SerializeField]
        private List<GameObject> _uiPrefabs;
        [SerializeField]
        private GameSettingsConfig _gameSettingsConfig;
        [SerializeField]
        private DiagnosticsProvider _diagnosticsProvider;
        [SerializeField]
        private AudioController _audioController;

        private ILogger _logger;

        private void OnApplicationQuit()
        {
            var gameStateNavigator = Container.Resolve<IGameStateNavigator>();
            if (gameStateNavigator.State != GameState.Exit)
            {
                gameStateNavigator.Exit();
            }
        }

        private void InstallLogging()
        {
#if DEBUG
            Container.Bind<ILogger>().To<Logger>().FromNew().AsSingle().WithArguments(new DebugLogHandler());
#else
            Container.Bind<ILogger>().To<StubLogger>().FromNew().AsSingle();
#endif
            UniTaskScheduler.UnobservedTaskException += OnUnobservedTaskException;
        }

        private void OnUnobservedTaskException(System.Exception ex)
        {
            _logger ??= Container.TryResolve<ILogger>();
            _logger?.LogError(nameof(ProjectInstaller), string.Format("Unobserved task exception {0}", ex));
        }

        private void InstallGameFlow()
        {
            Container.Bind<IStateMachine<GameState>>()
                .To<StateMachine<GameState>>()
                .AsSingle()
                .WithArguments(new GameStateGraph())
                .OnInstantiated((context, instance) =>
                {
                    if (instance is IStateMachine<GameState> stateMachine)
                    {
                        context.Container.Bind<IStateSwitcher<GameState>>().FromInstance(stateMachine.StateSwitcher);
                    }
                })
                .NonLazy();

            var stateHandlerTypes = ReflectionUtilities.GetInheritorTypes(typeof(IStateHandler<GameState>), System.Type.EmptyTypes);
            foreach (var stateHandlerType in stateHandlerTypes)
            {
                Container.BindInterfacesTo(stateHandlerType).AsSingle();
            }

            Container.BindInterfacesTo<ManagedStateMachine<GameState>>().AsSingle().OnInstantiated((context, target) =>
            {
                if (target is ManagedStateMachine<GameState> managedStateMachine)
                {
                    managedStateMachine.Start();
                }
            }).NonLazy();

            Container.BindInterfacesAndSelfTo<GameStateNavigator>().AsSingle();
        }

        private void InstallViewFactory()
        {
            if (_uiRootTransform == null)
            {
                _uiRootTransform = FindFirstObjectByType<Canvas>(FindObjectsInactive.Exclude).transform;
                if (_uiRootTransform == null)
                {
                    var canvasGO = new GameObject(nameof(Canvas));
                    var canvas = canvasGO.AddComponent<Canvas>();
                    canvas.renderMode = RenderMode.ScreenSpaceOverlay;
                    canvasGO.AddComponent<CanvasScaler>();
                    canvasGO.AddComponent<GraphicRaycaster>();
                    _uiRootTransform = canvas.transform;
                }
            }
            Container.BindInterfacesAndSelfTo<ViewFactory>().AsSingle().WithArguments(_uiRootTransform.gameObject, _uiPrefabs);
        }

        private void InstallPresenters()
        {
            Container.BindInterfacesAndSelfTo<PresenterNavigator>().AsTransient();
            var presenterTypes = ReflectionUtilities.GetInheritorTypes(typeof(IPresenter));
            foreach (var presenterType in presenterTypes)
            {
                Container.BindInterfacesTo(presenterType).FromNew().AsSingle();
            }
        }

        private void InstallGameConfig()
        {
            Container.BindInterfacesAndSelfTo<GameSettingsConfig>().FromInstance(_gameSettingsConfig).AsSingle();
        }

        private void InstallDiagnostics()
        {
            Container.BindInterfacesAndSelfTo<DiagnosticsProvider>().FromInstance(_diagnosticsProvider).AsSingle();
        }

        private void InstallAudio()
        {
            Container.BindInterfacesAndSelfTo<AudioController>().FromInstance(_audioController).AsSingle();
        }

        private void InstallContentLoading()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().FromNew().AsSingle();
        }
    }
}
