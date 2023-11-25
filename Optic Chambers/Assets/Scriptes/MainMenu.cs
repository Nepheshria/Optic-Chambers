using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class MainMenu : MonoBehaviour
{
    public void PlayGame(int level)
    {
        SceneManager.LoadSceneAsync($"Level{level}");
    }
    public void GoTemplate()
    {
        SceneManager.LoadSceneAsync("Templatelvl");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
