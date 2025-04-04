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
        if (!UserSession.isBgmEnabled)
            audioManager.transform.GetChild(0).GetComponent<AudioSource>().volume = 0f;
        if (!UserSession.isSfxEnabled)
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
        UserSession.bgmVolume = volume;
        if (volume != 0f)
        {
            UserSession.isBgmEnabled = true;
            audioManager.UnPauseBGM();
        }
    }
    
    public void SetSfxVolume()
    {
        var volume = GameObject.Find("Sfx Volume Slider").transform.GetChild(0).GetComponent<Slider>().value;
        audioManager.transform.GetChild(1).GetComponent<AudioSource>().volume = volume;
        SfxDisabledImage.SetActive(volume == 0f);
        UserSession.sfxVolume = volume;
        if (volume != 0f)
        {
            UserSession.isSfxEnabled = true;
            audioManager.EnableSFX();
        }
    }

    public void PlaySfxClip()
    {
        audioManager.PlaySfx(audioManager.CoinCollectClip);
    }

    public void RequestDbToUpdateAudioSettings()
    {
        DbRepository.UpdateUserAudioSettings();
    }
}
