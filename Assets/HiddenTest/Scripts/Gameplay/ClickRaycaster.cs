using UnityEngine;
using UnityEngine.InputSystem;

namespace HiddenTest.Gameplay
{
    using Abstractions;

    public class ClickRaycaster : MonoBehaviour
    {
        [SerializeField]
        private InputActionReference _clickActionReference;
        [SerializeField]
        private LayerMask _layerMask;
        [SerializeField]
        private int _raycastDistance = 10;
        [SerializeField]
        private bool _cameraAutodetect = true;
        [SerializeField]
        private Camera _camera;

        private readonly RaycastHit2D[] _hits = new RaycastHit2D[1];

        private void Awake()
        {
            if (_cameraAutodetect)
            {
                _camera = Camera.main;
            }
        }
        private void OnEnable()
        {
            _clickActionReference.action.performed += OnPointerClick;
        }
        private void OnDisable()
        {
            _clickActionReference.action.performed -= OnPointerClick;
        }

        private void OnPointerClick(InputAction.CallbackContext ctx)
        {
            if (ctx.action.WasReleasedThisFrame())
            {
                var pos = _camera.ScreenToWorldPoint(Pointer.current.position.ReadValue());
                var hitsCount = Physics2D.RaycastNonAlloc(pos, Vector2.zero, _hits, _raycastDistance, _layerMask);
                if (hitsCount > 0)
                {
                    var hit = _hits[0];
                    if (hit.collider != null && hit.collider.TryGetComponent(out IClickHandler clickHandler))
                    {
                        clickHandler.OnClick();
                    }
                }
            }
        }
    }
}
