using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class Singleton : MonoBehaviour
{
    // Start is called before the first frame update
    public static Singleton Instance;
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;
    public GameObject winscreen;
    public GameObject MainMenu;
    public GameObject LevelsMenu;
    Scene scene;
    void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void Start()
    {
        PlayMusic("Menu");
    }
    public void PlayMusic(string name)
    {
        Sound s = Array.Find(musicSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            musicSource.clip = s.clip;
            musicSource.Play();
        }
    }
    public void PlaySfx(string name)
        {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound not found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);
        }
    }
    public void ToggleMusic()
    {
        musicSource.mute = !musicSource.mute!;
    }
    public void ToggleSfx()
    {
        sfxSource.mute = !sfxSource.mute!;
    }
    public void MusicVolume(float volume)
    {
        musicSource.volume = volume;
    }
    public void SfxVolume(float volume)
    {
        sfxSource.volume = volume;
    }
    public void LevelVictory()
    {   
        PauseGame();
        winscreen.SetActive(true);
    }
    public void NextLevel()
    {
        PauseGame();

        winscreen.SetActive(false);
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync($"level{scene.buildIndex + 1}");
    }
    
    public void BackMainScreen()
    {
        PauseGame();

        winscreen.SetActive(false);
        SceneManager.LoadSceneAsync("MainMenu");
        Singleton.Instance.PlaySfx("Menu_back");
        Singleton.Instance.musicSource.Stop();
        Singleton.Instance.PlayMusic("Menu");
    }
    public void BackLevelScreen()
    {
        PauseGame();

        winscreen.SetActive(false);
        SceneManager.LoadSceneAsync("MainMenu");
        Singleton.Instance.PlaySfx("Menu_back");
        MainMenu.SetActive(false);
        LevelsMenu.SetActive(true);
        Singleton.Instance.musicSource.Stop();
        Singleton.Instance.PlayMusic("Menu");
    }

    public static bool gameIsPaused;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            gameIsPaused = !gameIsPaused;
            PauseGame();
        }
    }
    void PauseGame ()
    {
        if(gameIsPaused)
        {
            Time.timeScale = 0f;
        }
        else 
        {
            Time.timeScale = 1;
        }
    }
}
