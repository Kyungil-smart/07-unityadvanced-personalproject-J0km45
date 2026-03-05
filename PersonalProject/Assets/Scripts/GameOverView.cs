using UnityEngine;
using TMPro;

public class GameOverView : MonoBehaviour
{
    [SerializeField] private GameObject _resultPanel;
    [SerializeField] private TMP_Text _totalScore;
    [SerializeField] private TMP_Text _bestScore;

    public void ShowGameOver(bool isGameOver)
    {
        _resultPanel.SetActive(isGameOver);
    }

    public void UpdateScore(int totalScore, int bestScore)
    {
        _totalScore.text = $"SCORE : {totalScore}";
        _bestScore.text = $" BEST SCORE : {bestScore}";
    }

    public void CursorLock(bool showCursor)
    {
        Cursor.lockState = showCursor ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = showCursor;
    }
}