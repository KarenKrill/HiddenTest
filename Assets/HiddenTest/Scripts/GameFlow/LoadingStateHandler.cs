#nullable enable
using System;
using System.Threading;
using UnityEngine;
using Cysharp.Threading.Tasks;
using KarenKrill.UniCore.StateSystem.Abstractions;
using KarenKrill.ContentLoading.Abstractions;
using HiddenTest.Gameplay.Abstractions;
using HiddenTest.UI.Presenters.Abstractions;

namespace HiddenTest.GameFlow
{
    using Abstractions;

    public class LoadingStateHandler : PresentableStateHandlerBase<GameState>, IStateHandler<GameState>
    {
        public override GameState State => GameState.Loading;

        public LoadingStateHandler(ILogger logger,
            IStateSwitcher<GameState> stateSwitcher,
            IContentLoaderPresenter contentLoaderPresenter,
            ISceneLoader sceneLoader,
            ILevelSession levelSession) : base(contentLoaderPresenter)
        {
            _logger = logger;
            _stateSwitcher = stateSwitcher;
            _contentLoaderPresenter = contentLoaderPresenter;
            _sceneLoader = sceneLoader;
            _levelSession = levelSession;
        }

        public override void Enter(GameState prevState, object? context = null)
        {
            _logger.Log(nameof(LoadingStateHandler), nameof(Enter));
            base.Enter(prevState);

            // Speeding up scene loading by reducing the frame rate
            _initialThreadPriority = Application.backgroundLoadingPriority;
            Application.backgroundLoadingPriority = UnityEngine.ThreadPriority.High;

            var cancellationToken = Application.exitCancellationToken;
            LoadLoadingSceneAsync(cancellationToken).ContinueWith(() =>
            {
                LoadSceneAsync(context, cancellationToken).Forget();
            }).Forget();
        }

        public override void Exit(GameState nextState)
        {
            _logger.Log(nameof(LoadingStateHandler), nameof(Exit));
            Application.backgroundLoadingPriority = _initialThreadPriority;
            base.Exit(nextState);
        }

        private static readonly string _loadingSceneName = "LoadingScene";
        private static readonly string _mainMenuSceneName = "MainMenuScene";
        private static readonly string _levelSceneBaseName = "LevelSceneBase";
        private static readonly string _menuLoadingStageText = "Loading main menu...";
        private static readonly string _levelLoadingStageText = "Loading a level...";
        private static readonly string _successStatusText = "Press any key to start";

        private readonly ILogger _logger;
        private readonly IStateSwitcher<GameState> _stateSwitcher;
        private readonly IContentLoaderPresenter _contentLoaderPresenter;
        private readonly ISceneLoader _sceneLoader;
        private readonly ILevelSession _levelSession;

        private UnityEngine.ThreadPriority _initialThreadPriority;

        private async UniTask LoadLoadingSceneAsync(CancellationToken cancellationToken)
        {
            await _sceneLoader.LoadAsync(_loadingSceneName, cancellationToken: cancellationToken);
            // Waiting one frame to ensure that the loading scene is fully initialized
            await UniTask.Yield();
        }

        private async UniTask LoadSceneAsync(object? context, CancellationToken cancellationToken)
        {
            _contentLoaderPresenter.ProgressValue = 0;
            if (context is LevelLoadContext loadingContext && loadingContext.LevelIndex >= 0)
            {
                _contentLoaderPresenter.StageText = _levelLoadingStageText;
                _levelSession.ActiveLevel = loadingContext.LevelIndex;
                await _sceneLoader.LoadAsync(_levelSceneBaseName,
                    new SceneLoadParameters(progressAction: OnSceneLoadProgressChanged,
                        activationRequestAction: OnActivationRequested),
                    cancellationToken);
                _stateSwitcher.TransitTo(GameState.LevelGameplay, loadingContext);
            }
            else
            {
                _contentLoaderPresenter.StageText = _menuLoadingStageText;
                _levelSession.ActiveLevel = -1;
                await _sceneLoader.LoadAsync(_mainMenuSceneName,
                    new SceneLoadParameters(progressAction: OnSceneLoadProgressChanged,
                        activationRequestAction: OnActivationRequested),
                    cancellationToken);
                _stateSwitcher.TransitTo(GameState.MainMenu, null);
            }
        }

        private void OnSceneLoadProgressChanged(float progress)
        {
            _contentLoaderPresenter.ProgressValue = progress;
        }

        private void OnActivationRequested(Action allowActivationAction)
        {
            _contentLoaderPresenter.ProgressValue = 1;
            void OnContentLoaderPresenterContinue()
            {
                _contentLoaderPresenter.EnableContinue = false;
                _contentLoaderPresenter.Continue -= OnContentLoaderPresenterContinue;
                allowActivationAction?.Invoke();
            }
            _contentLoaderPresenter.Continue += OnContentLoaderPresenterContinue;
            _contentLoaderPresenter.StatusText = _successStatusText;
            _contentLoaderPresenter.EnableContinue = true;
        }
    }
}
