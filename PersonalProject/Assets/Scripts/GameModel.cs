public class GameModel
{
    public int Score { get; private set; }
    public float TimeLimit { get; private set; }
    public bool IsWaveStart { get; private set; }
    public bool IsGamePause { get; private set; }
    public bool IsGameOver { get; private set; }
    private readonly int _scoreIncrement;

    public GameModel(float _timeLimit, int scoreIncrement)
    {
        Score = 0;
        TimeLimit = _timeLimit;
        _scoreIncrement = scoreIncrement;
    }

    public void AddScore()
    {
        Score += _scoreIncrement;
    }

    public void SetWaveStart(bool isWaveStart)
    {
        IsWaveStart = isWaveStart; 
    }

    public void TogglePause()
    {
        IsGamePause = !IsGamePause;
    }

    public void SetGameOver(bool isGameOver)
    {
        IsGameOver = isGameOver;
    }

    public void DecreaseTime(float time)
    {
        TimeLimit -= time;
    }

    public void SetTimeToZero()
    {
        TimeLimit = 0;
    }
}
