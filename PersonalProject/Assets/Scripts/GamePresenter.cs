using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 90f; // 제한시간
    [SerializeField] private int _scoreIncrement = 10; // 점수증가량

    [SerializeField] private GameView _gameView;
    [SerializeField] private GameOverView _gameOverView;

    private GameModel _model;

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
        UpdateTime();

        if (_model.IsGameOver)
        {
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

    public void AddScore()
    {
        _model.AddScore();
        _gameView.UpdateScore(_model.Score);
    }

    void UpdateTime()
    {
        _model.UpdateTime(Time.deltaTime);
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