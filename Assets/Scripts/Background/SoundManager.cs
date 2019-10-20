using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;
    private AudioSource audioSrc;
    private AudioClip coinSound;
    private AudioClip hitSound;

    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSound = Resources.Load<AudioClip>("CoinPickup");
        hitSound = Resources.Load<AudioClip>("Shoot");
    }

    // Update is called once per frame
    public void PlayCoinSound()
    {
        audioSrc.PlayOneShot(coinSound);
    }

    public void PlayHitSound()
    {
        audioSrc.PlayOneShot(hitSound);
    }
}
