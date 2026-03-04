using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameView : MonoBehaviour
{
    [SerializeField] TMP_Text _scoreText;
    [SerializeField] TMP_Text _timeText;
    [SerializeField] TMP_Text _curMagazineText;
    [SerializeField] TMP_Text _maxMagazineText;
    [SerializeField] Image _crosshair;
    [SerializeField] Sprite _normalCrosshair;
    [SerializeField] Sprite _aimCrosshair;

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
