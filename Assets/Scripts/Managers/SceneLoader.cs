using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;

    public static SceneLoader Get()
    {
        return instance;
    }
    private void Awake()
    {
        if(instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void LoadSceneByName(string nameScene)
    {
        if (GameManager.Get() != null && GameManager.Get().IsGamePaused())
            GameManager.Get().PauseGame();

        AudioManager.Get()?.Play("select");
        if(nameScene == "MainMenu")
        {
            AudioManager.Get()?.StopAllSFX();
            AudioManager.Get()?.Play("menu");
        }
        else if(nameScene == "level1" || nameScene == "level2")
        {
            GameManager.Get()?.ResetEnemiesCount();
            AudioManager.Get()?.StopAllSFX();
            AudioManager.Get()?.Play("game");
        }
        SceneManager.LoadScene(nameScene);
    }
    public IEnumerator AsynchronousLoad(string scene)
    {
        yield return null;

        AsyncOperation ao = SceneManager.LoadSceneAsync(scene);
        ao.allowSceneActivation = false;

        while (!ao.isDone)
            yield return null;

        ao.allowSceneActivation = true;
    }
    public bool CompareActuallyScene(string sceneName){
        if(SceneManager.GetActiveScene() == SceneManager.GetSceneByName(sceneName))
            return true;
        else
            return false;
    }
    public void QuitApplication()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
        Application.Quit();
    }
}
