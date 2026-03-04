using UnityEngine;

public class StartButton : MonoBehaviour, IInteractable
{
    [SerializeField] private TargetSpawner _spawner;
    [SerializeField] private GamePresenter _presenter;
    private bool _isPressed;

    public void OnInteract(PlayerController player)
    {
        if (!_isPressed)
        {
            _isPressed = true;
            _spawner.GameStart();
            _presenter.WaveStart();
            transform.localPosition += Vector3.down * 0.15f;
        }
    }
}
