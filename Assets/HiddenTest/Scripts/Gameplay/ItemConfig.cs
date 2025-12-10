using UnityEngine;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    [CreateAssetMenu(fileName = nameof(ItemConfig), menuName = "Scriptable Objects/" + nameof(ItemConfig))]
    public class ItemConfig : ScriptableObject, IItemConfig
    {
        public int Id => GetInstanceID();

        [field: SerializeField]
        public Sprite Icon { get; private set; }

        [field: SerializeField]
        public string Name { get; private set; }
    }
}
