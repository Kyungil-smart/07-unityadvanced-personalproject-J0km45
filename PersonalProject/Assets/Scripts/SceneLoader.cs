using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    [SerializeField] private string TitleSceneName = "TitleScene";
    [SerializeField] private string GameSceneName = "GameScene";

    public void OnClickStart()
    {
        SceneManager.LoadScene(GameSceneName);
    }

    public void OnClickQuit()
    {
        Application.Quit();
    }

    public void OnClickTitle()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(TitleSceneName);
    }

    public void OnClickRetry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}