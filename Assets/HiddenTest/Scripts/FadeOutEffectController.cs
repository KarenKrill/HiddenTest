using UnityEngine;
using DG.Tweening;
using Zenject;
using HiddenTest.Gameplay.Abstractions;

namespace HiddenTest
{
    public class FadeOutEffectController : MonoBehaviour
    {
        [Inject]
        public void Initialize(IGameTaskController<IHiddenObject> taskController)
        {
            _taskController = taskController;
        }

        [SerializeField]
        private float _fadeOutDuration = .6f;

        private IGameTaskController<IHiddenObject> _taskController;

        private void OnEnable()
        {
            _taskController.TaskRemoved += OnTaskRemoved;
        }

        private void OnDisable()
        {
            _taskController.TaskRemoved -= OnTaskRemoved;
        }

        private void OnTaskRemoved(IGameTask<IHiddenObject> gameTask)
        {
            if (gameTask.Context.ParentGameObject.TryGetComponent<SpriteRenderer>(out var spriteRenderer))
            {
                spriteRenderer.DOFade(0, _fadeOutDuration);
            }
            else
            {
                gameTask.Context.ParentGameObject.SetActive(false);
            }
        }
    }
}
