using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; }
    

    [SerializeField] private AudioSource audioSourceComponent;
    [SerializeField] private AudioClip landSFX, deathSFX, iceBreakSFX, gameOverSFX;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    public void PlayLandSFX()
    {
        audioSourceComponent.clip = landSFX;
        audioSourceComponent.Play();
    }

    public void PlayDeathSFX()
    {
        audioSourceComponent.clip = deathSFX;
        audioSourceComponent.Play();
    }

    public void PlayIceBreakSFX()
    {
        audioSourceComponent.clip = iceBreakSFX;
        audioSourceComponent.Play();
    }

    public void PlayGameOverSFX()
    {
        audioSourceComponent.clip = gameOverSFX;
        audioSourceComponent.Play();
    }

}
