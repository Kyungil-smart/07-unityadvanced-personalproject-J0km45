using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;  // 이동 속도
    [SerializeField] private float _jumpHeight = 0.5f;  // 점프 높이
    [SerializeField] private float _mouseSens = 30f;  // 마우스 감도
    [SerializeField] private float _pitchMin = -60f;  // 시점 최소 각도
    [SerializeField] private float _pitchMax = 80f;  // 시점 최대 각도
    [SerializeField] private Transform _viewPoint;

    [SerializeField] private float _groundCheckDistance = 0.2f;  // 레이를 쏠 거리
    [SerializeField] private LayerMask _groundLayer;  // 지면으로 인식할 레이어

    private CharacterController _controller;
    private GunController _gun;

    private float _curMoveSpeed;
    private float _curMouseSens;
    private Vector3 _velocity;
    private Vector2 _moveInput;
    private bool _jumpInput;
    private bool _sprintInput;
    private Vector2 _rotationInput;
    private float _pitch;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _gun = GetComponent<GunController>();
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += MoveCancel;
        _inputActions.Player.Jump.performed += OnJump;
        _inputActions.Player.Sprint.performed += OnSprint;
        _inputActions.Player.Sprint.canceled += SprintCancel;
        _inputActions.Player.Rotation.performed += OnRotation;
        _inputActions.Player.Rotation.canceled += RotationCancel;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= MoveCancel;
        _inputActions.Player.Jump.performed -= OnJump;
        _inputActions.Player.Sprint.performed -= OnSprint;
        _inputActions.Player.Sprint.canceled -= SprintCancel;
        _inputActions.Player.Rotation.performed -= OnRotation;
        _inputActions.Player.Rotation.canceled -= RotationCancel;

        _inputActions.Disable();
    }

    private void Update()
    {
        _curMoveSpeed = _gun.IsAiming ? _moveSpeed * 0.5f : _moveSpeed;
        _curMouseSens = _gun.IsAiming ? _mouseSens * 0.2f : _mouseSens;

        // 회전
        float mouseX = _rotationInput.x * _curMouseSens * Time.deltaTime;
        float mouseY = _rotationInput.y * _curMouseSens * Time.deltaTime;
        transform.Rotate(Vector3.up * mouseX);

        _pitch -= mouseY;
        _pitch = Mathf.Clamp(_pitch, _pitchMin, _pitchMax);
        _viewPoint.localRotation = Quaternion.Euler(_pitch, 0f, 0f);

        bool grounded = IsGrounded();

        // 바닥에 고정
        if (grounded && _velocity.y < 0f)
        {
            _velocity.y = -2f;
        }

        // 점프
        if (_jumpInput && grounded)
        {
            if(_gun.IsAiming) _gun.SetAiming(false);
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }
        _jumpInput = false;

        // 이동
        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        float currentSpeed = _sprintInput ? _curMoveSpeed * 2f : _curMoveSpeed;
        _controller.Move(move * currentSpeed * Time.deltaTime);

        _velocity.y += Physics.gravity.y * Time.deltaTime;
        _controller.Move(_velocity * Time.deltaTime);
    }

    bool IsGrounded()
    {
        if (_controller.isGrounded) return true;

        Vector3 ray = transform.position + Vector3.up * 0.1f;
        if (Physics.Raycast(ray, Vector3.down, out RaycastHit hit, _groundCheckDistance, _groundLayer)) return true;

        return false;
    }

    void OnMove(InputAction.CallbackContext ctx)
    {
        _moveInput = ctx.ReadValue<Vector2>();
    }

    void MoveCancel(InputAction.CallbackContext ctx)
    {
        _moveInput = Vector2.zero;
    }

    void OnJump(InputAction.CallbackContext ctx)
    {
        _jumpInput = true;
    }

    void OnSprint(InputAction.CallbackContext ctx)
    {
        _sprintInput = true;
    }

    void SprintCancel(InputAction.CallbackContext ctx)
    {
        _sprintInput = false;
    }

    void OnRotation(InputAction.CallbackContext ctx)
    {
        _rotationInput = ctx.ReadValue<Vector2>();
    }

    void RotationCancel(InputAction.CallbackContext ctx)
    {
        _rotationInput = Vector2.zero;
    }
}
