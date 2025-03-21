using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{

    [SerializeField] private AudioManager audioManager;

    [SerializeField] private GameObject BGMDisabledImage;
    [SerializeField] private GameObject SfxDisabledImage;

    private void Awake()
    {
        BGMDisabledImage = GameObject.Find("BGM Disabled Image");
        BGMDisabledImage.SetActive(false);
        SfxDisabledImage = GameObject.Find("Sfx Disabled Image");
        SfxDisabledImage.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
        if (PlayerPrefs.GetString("BgmEnabled") == "false")
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume = 0f;
        if (PlayerPrefs.GetString("SfxEnabled") == "false")
            audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume = 0f;
        GameObject.Find("BGM Volume Slider").transform.GetChild(0).GetComponent<Slider>().value =
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume;
        GameObject.Find("Sfx Volume Slider").transform.GetChild(0).GetComponent<Slider>().value = audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume;
    }
    
    public void SetBGMVolume()
    {
        var volume = GameObject.Find("BGM Volume Slider").transform.GetChild(0).GetComponent<Slider>().value;
        audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume = volume;
        BGMDisabledImage.SetActive(volume == 0f);
        PlayerPrefs.SetFloat("BGMVolume", volume);
        if (volume != 0f)
        {
            PlayerPrefs.SetString("BgmEnabled", "true");
            audioManager.UnPauseBGM();
        }
        PlayerPrefs.Save();
    }
    
    public void SetSfxVolume()
    {
        var volume = GameObject.Find("Sfx Volume Slider").transform.GetChild(0).GetComponent<Slider>().value;
        audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume = volume;
        SfxDisabledImage.SetActive(volume == 0f);
        PlayerPrefs.SetFloat("SfxVolume", volume);
        if (volume != 0f)
        {
            PlayerPrefs.SetString("SfxEnabled", "true");
            audioManager.EnableSFX();
        }
        PlayerPrefs.Save();
    }

    public void PlaySfxClip()
    {
        audioManager.PlaySfx(audioManager.CoinCollectClip);
    }
}
