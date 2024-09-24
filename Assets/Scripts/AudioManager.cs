using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------ Audio Sources ------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("------ Audio Clips ------")] 
    [SerializeField] private AudioClip backgroundMusic;
    [SerializeField] private AudioClip speedUpClip;
    [SerializeField] private AudioClip higherJumpClip;
    [SerializeField] private AudioClip invertedControlsClip;
    [SerializeField] private AudioClip doubleJumpClip;
    [SerializeField] private AudioClip teleportClip;
    [SerializeField] private AudioClip jumpClip;
    [SerializeField] private AudioClip waterSplashClip;
    [SerializeField] private AudioClip landingClip;
    [SerializeField] private AudioClip playerHitClip;
    [SerializeField] private AudioClip swingingMaceClip;
    [SerializeField] private AudioClip enemyDeathClip;
    [SerializeField] private AudioClip thunderClip;
    [SerializeField] private AudioClip shootingBulletClip;
    [SerializeField] private AudioClip spikeDropperClip;
    [SerializeField] private AudioClip coinCollectClip;
    [SerializeField] private AudioClip fallingGroundClip;
    [SerializeField] private AudioClip expireBuffClip;
    [SerializeField] private AudioClip hoverButtonClip;
    [SerializeField] private AudioClip buttonClickClip;
    [SerializeField] private AudioClip pauseGameClip;
    [SerializeField] private AudioClip unpauseGameClip;
    private bool bgmEnabled = true;
    
    [SerializeField] private bool sfxEnabled = true;

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        if(bgmEnabled)
            musicSource.Play();
        sfxSource.ignoreListenerPause = true;
        sfxSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1);
        musicSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        if (PlayerPrefs.GetString("BgmEnabled") == "false")
        {
            PauseBGM();
        }
        if (PlayerPrefs.GetString("SfxEnabled") == "false")
        {
            DisableSFX();
        }
    }

    public void PauseBGM()
    {
        musicSource.Pause();
        bgmEnabled = false;
    }

    public void UnPauseBGM()
    {
        musicSource.UnPause();
        musicSource.volume = PlayerPrefs.GetFloat("BGMVolume", 0.3f);
        bgmEnabled = true;
    }
    
    public void StopBGM()
    {
        musicSource.Stop();
        bgmEnabled = false;
    }

    public void EnableSFX()
    {
        sfxEnabled = true;
        sfxSource.volume = PlayerPrefs.GetFloat("SfxVolume", 1);
    }
    
    public void DisableSFX()
    {
        sfxEnabled = false;
    }
    
    public void PlaySfx(AudioClip clip)
    {
        if(sfxEnabled)
            sfxSource.PlayOneShot(clip);
    }
    public AudioClip SpeedUpClip
    {
        get => speedUpClip;
    }

    public AudioClip HigherJumpClip
    {
        get => higherJumpClip;
    }
    
    public AudioClip InvertedControlsClip
    {
        get => invertedControlsClip;
    }
    
    public AudioClip DoubleJumpClip
    {
        get => doubleJumpClip;
    }
    
    public AudioClip TeleportClip
    {
        get => teleportClip;
    }
    
    public AudioClip JumpClip
    {
        get => jumpClip;
    }
    
    public AudioClip WaterSplashClip
    {
        get => waterSplashClip;
    }
    
    public AudioClip LandingClip
    {
        get => landingClip;
    }
    
    public AudioClip PlayerHitClip
    {
        get => playerHitClip;
    }
    
    public AudioClip EnemyDeathClip
    {
        get => enemyDeathClip;
    }
    
    public AudioClip ThunderClip
    {
        get => thunderClip;
    }
    
    public AudioClip ShootingBulletClip
    {
        get => shootingBulletClip;
    }
    
    public AudioClip SpikeDropperClip
    {
        get => spikeDropperClip;
    }
    
    public AudioClip CoinCollectClip
    {
        get => coinCollectClip;
    }
    
    public AudioClip FallingGroundClip
    {
        get => fallingGroundClip;
    }
    
    public AudioClip ExpireBuffClip
    {
        get => expireBuffClip;
    }
    
    public AudioClip HoverButtonClip
    {
        get => hoverButtonClip;
    }
    
    public AudioClip ButtonClickClip
    {
        get => buttonClickClip;
    }
    
    public AudioClip PauseGameClip
    {
        get => pauseGameClip;
    }
    
    public AudioClip UnpauseGameClip
    {
        get => unpauseGameClip;
    }

    public AudioClip SwingingMaceClip => swingingMaceClip;
    public bool SfxEnabled
    {
        get => sfxEnabled;
        set => sfxEnabled = value;
    }
    
    public void ClickButton()
    {
        PlaySfx(ButtonClickClip);
    }
    
}
