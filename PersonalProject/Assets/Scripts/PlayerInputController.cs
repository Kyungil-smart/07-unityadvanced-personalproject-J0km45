using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour
{
    [SerializeField] private GamePresenter _presenter;
    public PlayerInputActions InputActions { get; private set; }

    private void Awake()
    {
        InputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        InputActions.Enable();

        _presenter.OnInputLockChanged += InputLock;
    }

    private void OnDisable()
    {
        _presenter.OnInputLockChanged -= InputLock;

        InputActions.Disable();
    }

    void InputLock(bool isLocked)
    {
        if(isLocked)
        {
            InputActions.Player.Disable();
        }
        else
        {
            InputActions.Player.Enable();
        }
    }
}