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

    private void Start()
    {
        musicSource.clip = backgroundMusic;
        musicSource.Play();
    }

    public void StopBGM()
    {
        musicSource.Stop();
    }
    
    public void PlaySfx(AudioClip clip)
    {
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
    
}
