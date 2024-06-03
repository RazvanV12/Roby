using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreyCloud : MonoBehaviour
{
    [SerializeField] private GameObject lightning1;

    [SerializeField] private GameObject lightning2;

    [SerializeField] private GameObject lightning3;

    [SerializeField] private float lightningTime = 0.35f;

    [SerializeField] private float timeBetweenLightnings = 2f;
    
    [SerializeField] private AudioManager audioManager;
    private Renderer _renderer;
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("Audio").GetComponent<AudioManager>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _renderer = GetComponent<Renderer>();
        StartCoroutine(EnableLightnings());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private IEnumerator EnableLightnings()
    {
        while (true)
        {
            if(_renderer.isVisible)
                audioManager.PlaySfx(audioManager.ThunderClip);
            lightning1.SetActive(true);
            lightning2.SetActive(true);
            lightning3.SetActive(true);
            yield return new WaitForSeconds(lightningTime);
            lightning1.SetActive(false);
            lightning2.SetActive(false);
            lightning3.SetActive(false);
            yield return new WaitForSeconds(timeBetweenLightnings);
        }
    }
}
