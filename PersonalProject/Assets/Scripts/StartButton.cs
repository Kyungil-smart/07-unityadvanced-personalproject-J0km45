using UnityEngine;

public class StartButton : MonoBehaviour
{
    [SerializeField] private TargetSpawner _spawner;
    [SerializeField] private GamePresenter _presenter;
    private bool _isPressed;
    public void Press()
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
