using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance { get; private set; }
    
    [SerializeField] private AudioSource audioSourceComponent;
    [SerializeField] private AudioClip ropeStrecthSFX;

    void Awake()
    {
        if (Instance == null) Instance = this;
        else Destroy(gameObject);
    }

    public void PlayRopeStretchSFX()
    {
        audioSourceComponent.clip = ropeStrecthSFX;
        audioSourceComponent.Play();
    }
}
