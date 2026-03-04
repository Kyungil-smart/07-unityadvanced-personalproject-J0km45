using System;

public class GameModel
{
    public int Score { get; private set; }
    public float TimeLimit { get; private set; }
    public bool IsGameOver { get; private set; }

    private readonly int _scoreIncrement;

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
    }

    public void UpdateTime(float time)
    {
        if (IsGameOver) return;

        TimeLimit -= time;

        if (TimeLimit <= 0)
        {
            IsGameOver = true;
        }
    }
}
