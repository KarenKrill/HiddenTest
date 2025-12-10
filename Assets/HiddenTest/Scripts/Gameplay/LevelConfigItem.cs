using System;
using UnityEngine;

namespace HiddenTest.Gameplay
{
    [Serializable]
    public class LevelConfigItem
    {
        [HideInInspector]
        public string name;

        [field: SerializeField]
        public bool Disabled { get; private set; }

        [field: SerializeField]
        public ItemConfig Config { get; private set; }
    }
}
