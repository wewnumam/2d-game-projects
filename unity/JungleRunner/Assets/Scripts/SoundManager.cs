using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; }
    

    [SerializeField] private AudioSource audioSourceComponent;
    [SerializeField] private AudioClip killSFX, jumpSFX, deathSFX;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    public void PlayKillSFX()
    {
        audioSourceComponent.clip = killSFX;
        audioSourceComponent.Play();
    }

    public void PlayJumpSFX()
    {
        audioSourceComponent.clip = jumpSFX;
        audioSourceComponent.Play();
    }

    public void PlayDeathSFX()
    {
        audioSourceComponent.clip = deathSFX;
        audioSourceComponent.Play();
    }
}
