using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class level : MonoBehaviour
{
    public void BackMainScreen()
    {
        SceneManager.LoadSceneAsync("MainMenu");
    }

}
