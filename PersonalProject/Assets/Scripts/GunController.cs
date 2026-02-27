using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private float _fireRange; // 총 사정거리
    [SerializeField] private LayerMask _TargetLayer;
    [SerializeField] private int _maxMagazine = 10; // 최대 탄창 수
    [SerializeField] private int _currentMagazine; // 현재 탄창 수

    private Vector2 _mousePos;
    private IHittable _currentTarget;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _camera = Camera.main;
        _currentMagazine = _maxMagazine;
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Point.performed += OnPoint;
        _inputActions.Player.Fire.performed += OnFire;
    }

    private void OnDisable()
    {
        _inputActions.Player.Point.performed -= OnPoint;
        _inputActions.Player.Fire.performed -= OnFire;

        _inputActions.Disable();
    }

    private void Update()
    {
        DetectTarget();
    }

    void OnPoint(InputAction.CallbackContext ctx)
    {
        _mousePos = ctx.ReadValue<Vector2>();
    }

    void OnFire(InputAction.CallbackContext ctx)
    {
        if(_currentMagazine <= 0) return;
        _currentMagazine--;

        if (_currentTarget == null) return;
        _currentTarget.OnHit();
    }

    void DetectTarget()
    {
        Ray ray = _camera.ScreenPointToRay(_mousePos);
        RaycastHit hit;

        if(Physics.Raycast(ray, out hit, _fireRange, _TargetLayer))
        {
            if(hit.transform.TryGetComponent<IHittable>(out IHittable hittable))
            {
                _currentTarget = hittable;
            }
        }
        else
        {
            _currentTarget = null;
        }
    }
}
