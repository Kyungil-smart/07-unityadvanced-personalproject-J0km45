using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 90f; // 제한시간
    [SerializeField] private int _scoreIncrement = 10; // 점수증가량

    [SerializeField] private GameView _gameView;
    [SerializeField] private GameOverView _gameOverView;

    private GameModel _model;
    private bool _isEnd;

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
        if (_model.IsWaveStart)
        {
            UpdateTime();
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
        _gameOverView.CursorLock(!_model.IsGameOver);
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

    void GameOver()
    {
        Time.timeScale = 0f;
        _gameOverView.ShowGameOver(_model.IsGameOver);
        _gameOverView.CursorLock(!_model.IsGameOver);   
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