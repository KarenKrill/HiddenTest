using UnityEngine;

namespace HiddenTest.Gameplay.Abstractions
{
    public interface IItemConfig
    {
        public int Id { get; }
        public Sprite Icon { get; }
        public string Name { get; }
    }
}
