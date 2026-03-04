using UnityEngine;

public class GamePresenter : MonoBehaviour
{
    [SerializeField] private float _timeLimit = 60f;
    [SerializeField] private int _scoreIncrement = 10;

    [SerializeField] private GameView _gameView;

    private GameModel _model;

    private void Awake()
    {
        _model = new GameModel(_timeLimit, _scoreIncrement);
    }

    private void Start()
    {
        GameStart();
    }

    private void OnEnable()
    {
        _model.OnScoreChanged += _gameView.UpdateScore;
    }

    private void OnDisable()
    {
        _model.OnScoreChanged -= _gameView.UpdateScore;
    }

    private void Update()
    {
        UpdateTime();
    }

    void GameStart()
    {
        _gameView.UpdateScore(_model.Score);
        _gameView.UpdateTime(_model.TimeLimit);
    }

    void UpdateTime()
    {
        _model.UpdateTime(Time.deltaTime);
        _gameView.UpdateTime(_model.TimeLimit);
    }

    public void AddScore() // 스포너의 점수 추가함수에서 호출
    {
        _model.AddScore();
    }
}
