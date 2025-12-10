using System;
using UnityEngine;
using Zenject;

namespace HiddenTest.Gameplay
{
    using Abstractions;
    
    public class HiddenObjectBehaviour : MonoBehaviour, IHiddenObject, IClickHandler
    {
        public IItemConfig Config => _config;
        public GameObject ParentGameObject => gameObject;

        public event Action<IHiddenObject> Clicked;

        [Inject]
        public void Initialize(ILevelItemsRegistry levelItemsRegistry)
        {
            _levelItemsRegistry = levelItemsRegistry;
        }

        public void OnClick() => Clicked?.Invoke(this);

        [SerializeField]
        private ItemConfig _config;
        private ILevelItemsRegistry _levelItemsRegistry;

        private void OnEnable()
        {
            _levelItemsRegistry.Register(_config.Id, this);
        }
        private void OnDisable()
        {
            _levelItemsRegistry.Unregister(_config.Id);
        }
    }
}
