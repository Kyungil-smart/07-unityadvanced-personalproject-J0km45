using System;

public class GameModel
{
    public int Score { get; private set; }
    public float TimeLimit { get; private set; }
    public bool IsGameOver { get; private set; }

    private int _scoreIncrement;

    public event Action<int> OnScoreChanged;
    public event Action<bool> OnGameOver;

    public GameModel(float _timeLimit, int scoreIncrement)
    {
        Score = 0;
        TimeLimit = _timeLimit;
        _scoreIncrement = scoreIncrement;
        IsGameOver = false;
    }

    public void AddScore()
    {
        Score += _scoreIncrement;
        OnScoreChanged?.Invoke(Score);
    }

    public void UpdateTime(float time)
    {
        if (IsGameOver) return;

        TimeLimit -= time;

        if (TimeLimit <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        IsGameOver = true;
        OnGameOver?.Invoke(IsGameOver);
    }
}
