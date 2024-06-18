using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;
    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        GameObject.Find("BGM Volume Slider").transform.GetChild(0).GetComponent<Slider>().value =
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume;
        GameObject.Find("Sfx Volume Slider").transform.GetChild(0).GetComponent<Slider>().value = audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume;
    }
    
    public void SetBGMVolume()
    {
        var volume = GameObject.Find("BGM Volume Slider").transform.GetChild(0).GetComponent<Slider>().value;
        audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("BGMVolume", volume);
    }
    
    public void SetSfxVolume()
    {
        var volume = GameObject.Find("Sfx Volume Slider").transform.GetChild(0).GetComponent<Slider>().value;
        audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume = volume;
        PlayerPrefs.SetFloat("SfxVolume", volume);
    }

    public void PlaySfxClip()
    {
        audioManager.PlaySfx(audioManager.CoinCollectClip);
    }
}
