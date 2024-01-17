using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;
public class UIManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static UIManager Instance;
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
        winscreen.SetActive(true);
    }
    public void NextLevel()
    {
        Debug.Log("Test");
        scene = SceneManager.GetActiveScene();
        SceneManager.LoadSceneAsync($"level{scene.buildIndex + 1}");
        winscreen.SetActive(false);
    }
    
    public void BackMainScreen()
    {
        
        winscreen.SetActive(false);
        SceneManager.LoadSceneAsync("MainMenu");
        UIManager.Instance.PlaySfx("Menu_back");
        UIManager.Instance.musicSource.Stop();
        UIManager.Instance.PlayMusic("Menu");
    }
    public void BackLevelScreen()
    {

        winscreen.SetActive(false);
        SceneManager.LoadSceneAsync("MainMenu");
        UIManager.Instance.PlaySfx("Menu_back");
        MainMenu.SetActive(false);
        LevelsMenu.SetActive(true);
        UIManager.Instance.musicSource.Stop();
        UIManager.Instance.PlayMusic("Menu");
    }
}
