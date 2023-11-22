using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(fileName = "Level Manager", menuName = "Managers/Level Manager")]
public class LevelManager : ScriptableObject
{
    [Header("Events")]
    [SerializeField] FloatEvent _levelLoading;
    [Header("Components")]
    [SerializeField] GameObject _levelLoader;

    public void LoadLevel(int p_sceneIndex)
    {
        SceneManager.LoadScene(p_sceneIndex);
    }

    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void LoadLevelAsync(int p_sceneIndex)
    {
        LevelLoader levelLoader = Instantiate(_levelLoader).GetComponent<LevelLoader>();
        levelLoader.StartCoroutine(LoadLevelAsyncTask(p_sceneIndex, levelLoader));
    }

    public void LoadNextLevelAsync()
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void RestartLevelAsync(int p_sceneIndex)
    {
        LoadLevelAsync(SceneManager.GetActiveScene().buildIndex);
    }

    public void QuitApplication()
    {
        Application.Quit();
    }

    IEnumerator LoadLevelAsyncTask(int p_sceneIndex, MonoBehaviour p_levelLoader)
    {
        AsyncOperation loadSceneAsync = SceneManager.LoadSceneAsync(p_sceneIndex);

        while (!loadSceneAsync.isDone)
        {
            float progressValue = Mathf.Clamp01(loadSceneAsync.progress / 0.9f);
            _levelLoading.Raise(progressValue);
            yield return null;
        }

        Destroy(p_levelLoader);
    }
}
