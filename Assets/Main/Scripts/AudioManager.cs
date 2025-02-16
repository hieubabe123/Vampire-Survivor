using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{

    public static AudioManager instance;
    [Header("----------------- Audio Source -----------------")]
    [SerializeField] private AudioSource musicSource;
    [SerializeField] private AudioSource SFXSource;


    [Header("----------------- Audio Clip -----------------")]
    public AudioClip background;
    public AudioClip playerDeath;
    public AudioClip upgradeItem;
    public AudioClip shootEnemy;
    public AudioClip enemyDeath;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public void Start()
    {
        musicSource.clip = background;
        musicSource.Play();
    }

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }
}
