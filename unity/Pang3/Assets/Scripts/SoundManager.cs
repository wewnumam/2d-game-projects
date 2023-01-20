using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set;}

    [SerializeField] AudioSource audioSourceComponent;
    [SerializeField] AudioClip gunShootSFX, ballExplosion;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayGunShootSFX()
    {
        audioSourceComponent.clip = gunShootSFX;
        audioSourceComponent.Play();
    }

    public void PlayBallExplosionSFX()
    {
        audioSourceComponent.clip = ballExplosion;
        audioSourceComponent.Play();
    }
}
