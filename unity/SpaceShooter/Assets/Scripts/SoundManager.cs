using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static SoundManager _instance;
    public static SoundManager Instance { get => _instance; }
    

    [SerializeField] private AudioSource audioSourceComponent;
    [SerializeField] private AudioClip laserSFX, explosionSFX;

    void Awake()
    {
        if (_instance == null) _instance = this;
        else Destroy(gameObject);
    }

    public void PlayLaserSFX()
    {
        audioSourceComponent.clip = laserSFX;
        audioSourceComponent.Play();
    }

    public void PlayExplosionSFX()
    {
        audioSourceComponent.clip = explosionSFX;
        audioSourceComponent.Play();
    }
}
