using UnityEngine;

public class PauseView : MonoBehaviour
{
    [SerializeField] private GameObject _pausePanel;

    public void ShowGamePause(bool isGameOver)
    {
        _pausePanel.SetActive(isGameOver);
    }

    public void CursorLock(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}