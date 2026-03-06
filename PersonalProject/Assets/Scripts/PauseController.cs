using UnityEngine;
using UnityEngine.InputSystem;

public class PauseController : MonoBehaviour
{
    [SerializeField] private GamePresenter _presenter;

    [SerializeField] private PlayerInputController _input;

    private void Awake()
    {
        _input.Init();
    }

    private void OnEnable()
    {
        _input.InputActions.UI.Pause.performed += OnPause;
    }

    private void OnDisable()
    {
        _input.InputActions.UI.Pause.performed -= OnPause;
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