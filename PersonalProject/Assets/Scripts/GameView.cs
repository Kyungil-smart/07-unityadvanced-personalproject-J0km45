using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _curMagazineText;
    [SerializeField] private TMP_Text _maxMagazineText;
    [SerializeField] private Image _crosshair;
    [SerializeField] private Sprite _normalCrosshair;
    [SerializeField] private Sprite _aimCrosshair;

    public void UpdateScore(int score)
    {
        _scoreText.text = $"{score}";
    }

    public void UpdateTime(float time)
    {
        int timeint = (int)time;
        timeint = Mathf.Max(timeint, 0);

        int minutes = timeint / 60;
        int seconds = timeint % 60;

        _timeText.text = $"{minutes:D2}:{seconds:D2}";
    }

    public void UpdateMagazine(int curMagazine, int maxMagazine)
    {
        _curMagazineText.text = $"{curMagazine}";
        _maxMagazineText.text = $"/ {maxMagazine}";
    }

    public void UpdateCrosshair(bool aiming)
    {
        _crosshair.sprite = aiming ? _aimCrosshair : _normalCrosshair;
    }
}
