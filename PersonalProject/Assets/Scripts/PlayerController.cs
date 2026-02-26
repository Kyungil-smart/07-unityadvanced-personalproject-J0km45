using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _moveSpeed = 1f;
    [SerializeField] private float _jumpHeight = 0.5f;
    [SerializeField] private float _groundCheckDistance = 0.2f;  // 레이를 쏠 거리
    [SerializeField] private LayerMask _groundLayer;  // 지면으로 인식할 레이어

    private CharacterController _controller;

    private Vector3 _velocity;
    private Vector2 _moveInput;
    private bool _jumpInput;
    private bool _sprintInput;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.Player.Move.performed += OnMove;
        _inputActions.Player.Move.canceled += MoveCancel;
        _inputActions.Player.Jump.started += OnJump;
        _inputActions.Player.Sprint.performed += OnSprint;
        _inputActions.Player.Sprint.canceled += SprintCancel;
    }

    private void OnDisable()
    {
        _inputActions.Player.Move.performed -= OnMove;
        _inputActions.Player.Move.canceled -= MoveCancel;
        _inputActions.Player.Jump.started -= OnJump;
        _inputActions.Player.Sprint.performed -= OnSprint;
        _inputActions.Player.Sprint.canceled -= SprintCancel;

        _inputActions.Disable();
    }

    private void Update()
    {
        bool grounded = IsGrounded();

        if (grounded)
        {
            _velocity.y = -2f;
        }

        // 점프
        if (_jumpInput && grounded)
        {
            _velocity.y = Mathf.Sqrt(_jumpHeight * -2f * Physics.gravity.y);
        }
        _jumpInput = false;

        // 이동
        Vector3 move = transform.right * _moveInput.x + transform.forward * _moveInput.y;
        float currentSpeed = _sprintInput ? _moveSpeed * 2f : _moveSpeed;
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
        if (ctx.started)
        {
            _jumpInput = true;
        }
    }

    void OnSprint(InputAction.CallbackContext ctx)
    {
        _sprintInput = true;
    }

    void SprintCancel(InputAction.CallbackContext ctx)
    {
        _sprintInput = false;
    }
}
