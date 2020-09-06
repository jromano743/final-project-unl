using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager sharedInstance;
    AudioSource audiosource;

    [Header("Sonidos")]
    public AudioClip grraplinGun;
    public AudioClip pickUpCoin;
    public AudioClip pickUpKey;
    public AudioClip dead;
    public AudioClip reset;

    private void Awake()
    {
        //Patron singleton
        if (sharedInstance == null)
        {
            sharedInstance = this;
        }
        else if (sharedInstance != this)
        {
            Destroy(gameObject);
        }

    }
    private void Start()
    {
        audiosource = GetComponent<AudioSource>();
    }

    public void PlaySound(AudioClip sound)
    {
        audiosource.PlayOneShot(sound);
    }
}
