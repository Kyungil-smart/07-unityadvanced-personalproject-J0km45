using System;
using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 90f; // 제한시간
    [SerializeField] private int _scoreIncrement = 10; // 점수증가량

    [SerializeField] private GameView _gameView;
    [SerializeField] private PauseView _pauseView;
    [SerializeField] private GameOverView _gameOverView;

    [SerializeField] private AudioClip _gameOverClip;
    [SerializeField] private AudioClip _scoreClip;

    private GameModel _model;
    private bool _isPaused;
    private bool _isEnd;
    public event Action<bool> OnInputLockChanged;

    private void Awake()
    {
        _model = new GameModel(_timeLimit, _scoreIncrement);
    }

    private void Start()
    {
        GameStart();
    }

    private void Update()
    {
        if (_model.IsWaveStart && !_model.IsGamePause)
        {
            UpdateTime();
        }

        if (_model.IsGamePause != _isPaused)
        {
            GamePause();
            _isPaused = _model.IsGamePause;
        }

        if (_model.IsGameOver && !_isEnd)
        {
            _isEnd = true;
            GameOver();
            ShowGameResult();
        }
    }

    void GameStart()
    {
        _gameOverView.ShowGameOver(_model.IsGameOver);
        _gameOverView.CursorLock(_model.IsGameOver);
        _gameView.UpdateScore(_model.Score);
        _gameView.UpdateTime(_model.TimeLimit);
    }

    public void WaveStart()
    {
        _model.SetWaveStart(true);
    }

    public void AddScore()
    {
        _model.AddScore();
        AudioManager.Instance.PlaySound(AudioManager.Instance._source, _scoreClip);
        _gameView.UpdateScore(_model.Score);
    }

    void UpdateTime()
    {
        if (_model.IsGameOver) return;

        _model.DecreaseTime(Time.deltaTime);
        if (_model.TimeLimit <= 0)
        {
            _model.SetTimeToZero();
            _model.SetGameOver(true);
        }

        _gameView.UpdateTime(_model.TimeLimit);
    }

    void GamePause()
    {
        Time.timeScale = _model.IsGamePause ? 0 : 1;
        _pauseView.ShowGamePause(_model.IsGamePause);
        _pauseView.CursorLock(!_model.IsGamePause);
        OnInputLockChanged?.Invoke(_model.IsGamePause);
    }

    public void TogglePause()
    {
        if (_model.IsGameOver) return;
        _model.TogglePause();
    }

    void GameOver()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.PlaySound(AudioManager.Instance._source, _gameOverClip);
        _gameOverView.ShowGameOver(_model.IsGameOver);
        _gameOverView.CursorLock(_model.IsGameOver);
        OnInputLockChanged?.Invoke(_model.IsGameOver);
    }

    void ShowGameResult()
    {
        int totalScore = _model.Score;
        int bestScore = PlayerPrefs.GetInt("BestScore", 0);

        if (totalScore > bestScore)
        {
            PlayerPrefs.SetInt("BestScore", totalScore);
            bestScore = totalScore;
        }

        _gameOverView.UpdateScore(totalScore, bestScore);
    }
}