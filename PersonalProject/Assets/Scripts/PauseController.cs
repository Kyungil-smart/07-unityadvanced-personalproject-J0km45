using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GamePresenter _presenter;

    private PlayerInputActions _inputActions;

    private void Awake()
    {
        _inputActions = new PlayerInputActions();
    }

    private void OnEnable()
    {
        _inputActions.Enable();

        _inputActions.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _inputActions.UI.Pause.performed -= OnPause;

        _inputActions.Disable();
    }

    void OnPause(InputAction.CallbackContext ctx)
    {
        OnClickPause();
    }

    public void OnClickPause()
    {
        if (_presenter == null) return;
        _presenter.TogglePause();
    }
}