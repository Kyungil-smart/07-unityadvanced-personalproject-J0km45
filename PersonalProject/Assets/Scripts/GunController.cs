using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    [SerializeField] private float _fireRange = 10f; // 총 사정거리
    [SerializeField] private int _maxMagazine = 10; // 최대 탄창 수
    [SerializeField] private int _currentMagazine; // 현재 탄창 수
    [SerializeField] private float _reloadTime = 1f; // 재장전 시간
    [SerializeField] float normalFOV = 60f;
    [SerializeField] float aimFOV = 10f;
    [SerializeField] private Transform _gunPos;
    [SerializeField] private LayerMask _TargetLayer;
    [SerializeField] private Camera _weaponCamera;

    public bool IsAiming { get; private set; }

    private Camera _camera;
    
    private Vector2 _mousePos;
    private IHittable _currentTarget;
    private bool _isReloading;
    private PlayerInputActions _inputActions;
    private Quaternion _originalGunPos;

    private void Awake()
    {
        _camera = Camera.main;
        _currentMagazine = _maxMagazine;
        _inputActions = new PlayerInputActions();
        _originalGunPos = _gunPos.localRotation;
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Point.performed += OnPoint;
        _inputActions.Player.Fire.performed += OnFire;
        _inputActions.Player.Reload.performed += OnReload;
        _inputActions.Player.Aiming.performed += OnAiming;
        _inputActions.Player.Aiming.canceled += AimingCancel;
    }

    private void OnDisable()
    {
        _inputActions.Player.Point.performed -= OnPoint;
        _inputActions.Player.Fire.performed -= OnFire;
        _inputActions.Player.Reload.performed -= OnReload;
        _inputActions.Player.Aiming.performed -= OnAiming;
        _inputActions.Player.Aiming.canceled -= AimingCancel;

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
        if (_isReloading) return;

        if(_currentMagazine <= 0) return;
        _currentMagazine--;

        if (_currentTarget == null) return;
        _currentTarget.OnHit();
    }

    void OnReload(InputAction.CallbackContext ctx)
    {
        if (_currentMagazine == _maxMagazine) return;
        if (_isReloading) return;
        
        if(IsAiming) SetAiming(false);

        StartCoroutine(ReloadCoroutine());
    }

    void OnAiming(InputAction.CallbackContext ctx)
    {
        if (_isReloading) return;

        SetAiming(true);
    }

    void AimingCancel(InputAction.CallbackContext ctx)
    {
        SetAiming(false);
    }

    public void SetAiming(bool isAiming)
    {
        IsAiming = isAiming;
        _weaponCamera.enabled = !isAiming;
        _camera.fieldOfView = isAiming ? aimFOV : normalFOV;
    }

    IEnumerator MoveGun(Quaternion gunPos)
    {
        float angle = Quaternion.Angle(_gunPos.localRotation, gunPos);

        while (Quaternion.Angle(_gunPos.localRotation, gunPos) > 0.1f)
        {
            _gunPos.localRotation = Quaternion.RotateTowards(_gunPos.localRotation, gunPos, Time.deltaTime * (angle / _reloadTime) * 2);

            yield return null;
        }
    }

    IEnumerator ReloadCoroutine()
    {
        _isReloading = true;
        Quaternion downGunPos = Quaternion.Euler(40f, 0f, 0f);
        yield return MoveGun(downGunPos);

        _currentMagazine = _maxMagazine;
        yield return MoveGun(_originalGunPos);

        _isReloading = false;
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
