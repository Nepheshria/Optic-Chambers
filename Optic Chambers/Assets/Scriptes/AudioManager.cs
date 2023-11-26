using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Slider _musicSlider, _sfxSlider;



 
    public void ToggleMusic()
    {
        Singleton.Instance.ToggleMusic();
    }

    public void ToggleSfx()
    {
        Singleton.Instance.ToggleSfx();
    }

    public void MusicVolume()
    {
        Singleton.Instance.MusicVolume(_musicSlider.value);
    }

    public void SfxVolume()
    {
        Singleton.Instance.SfxVolume(_sfxSlider.value);
    }
}
