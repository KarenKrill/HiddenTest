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

        [field: SerializeField]
        public bool TimeLimitEnabled { get; private set; } = true;

        [field: SerializeField]
        public float TimeLimitInSeconds { get; private set; }

        public IReadOnlyList<IItemConfig> Items
        {
            get
            {
                if (_enabledItems == null)
                {
                    InitEnabledItemsList();
                }
                return _enabledItems;
            }
        }

        [SerializeField]
        private List<LevelConfigItem> _items = new();

        private List<ItemConfig> _enabledItems = null;

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
                    _enabledItems.Add(item.Config);
                }
            }
        }
    }
}
