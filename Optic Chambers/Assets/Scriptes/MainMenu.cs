using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{

    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync($"Level{level}");
        Singleton.Instance.musicSource.Stop();
        Singleton.Instance.PlayMusic("Level");
    }
    public void GoTemplate()
    {
        SceneManager.LoadSceneAsync("Templatelvl");
        Singleton.Instance.musicSource.Stop();
        Singleton.Instance.PlayMusic("Level");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
