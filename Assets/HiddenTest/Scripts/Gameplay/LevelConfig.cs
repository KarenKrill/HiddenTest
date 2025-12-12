using System.Collections.Generic;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    [CreateAssetMenu(fileName = nameof(LevelConfig), menuName = "Scriptable Objects/" + nameof(LevelConfig))]
    public class LevelConfig : ScriptableObject, ILevelConfig
    {
        [field: SerializeField]
        public TaskViewType TaskViewType { get; private set; }

        [field: SerializeField, Min(1)]
        public int MaxTaskCount { get; private set; } = 3;

        [field: SerializeField]
        public bool TimeLimitEnabled { get; private set; } = true;

        [field: SerializeField, Min(0)]
        public float TimeLimitInSeconds { get; private set; }

        public IReadOnlyDictionary<int, IItemConfig> EnabledItems
        {
            get
            {
                if (_enabledItems == null || _enabledItems.Count == 0)
                {
                    InitEnabledItemsList();
                }
                return _enabledItems;
            }
        }

        [field: SerializeField]
        public AudioClip BackgroundMusic { get; private set; }

        [field: SerializeField]
        public GameObject LevelPrefab { get; private set; }

        [SerializeField]
        private List<LevelConfigItem> _items = new();

        private Dictionary<int, IItemConfig> _enabledItems;

        private void OnValidate()
        {
            foreach (var item in _items)
            {
                if (item.Config != null)
                {
                    item.name = item.Config.Name;
                }
                else
                {
                    item.name = null;
                }
            }
        }

        private void InitEnabledItemsList()
        {
            _enabledItems = new();
            foreach (var item in _items)
            {
                if (!item.Disabled)
                {
                    _enabledItems.Add(item.Config.Id, item.Config);
                }
            }
        }
    }
}
