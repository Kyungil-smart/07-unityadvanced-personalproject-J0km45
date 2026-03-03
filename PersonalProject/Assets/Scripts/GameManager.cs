using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private int _score = 0;
    [SerializeField] private int _scoreIncrement = 10;
    [SerializeField] private float _timeLimit = 60f;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        Init();
    }

    void Init()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    private void Start()
    {
        CursorLock(true);
    }

    private void Update()
    {
        if(!IsGameOver)
        {
            _timeLimit -= Time.deltaTime;

            if( _timeLimit <= 0 )
            {
                GameOver();
            }
        }
    }

    public void AddScore()
    {
        _score += _scoreIncrement;
        Debug.Log($"Score: {_score}");
    }

    private void GameOver()
    {
        IsGameOver = true;
        CursorLock(false);
        Debug.Log("Game Over!");
    }
    private void CursorLock(bool isLocked)
    {
        Cursor.lockState = isLocked ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !isLocked;
    }
}
