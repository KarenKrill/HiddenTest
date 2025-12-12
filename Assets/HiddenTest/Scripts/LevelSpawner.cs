using UnityEngine;
using Zenject;
using HiddenTest.Gameplay.Abstractions;

namespace HiddenTest
{
    public class LevelSpawner : MonoBehaviour
    {
        [Inject]
        public void Initialize(ILevelSession levelSession,
            IGameConfig gameConfig,
            IInstantiator instantiator)
        {
            _levelSession = levelSession;
            _gameConfig = gameConfig;
            _instantiator = instantiator;
            _levelSession.ActiveLevelChanged += OnActiveLevelChanged;
            OnActiveLevelChanged(_levelSession.ActiveLevel);
        }

        [SerializeField]
        private Transform _levelParent;

        private ILevelSession _levelSession;
        private IGameConfig _gameConfig;
        private IInstantiator _instantiator;

        private void OnActiveLevelChanged(int levelIndex)
        {
            if (_levelParent != null && levelIndex >= 0 && levelIndex < _gameConfig.LevelsConfig.Count)
            {
                var levelConfig = _gameConfig.LevelsConfig[levelIndex];
                _ = _instantiator.InstantiatePrefab(levelConfig.LevelPrefab, _levelParent);
            }
        }
    }
}
