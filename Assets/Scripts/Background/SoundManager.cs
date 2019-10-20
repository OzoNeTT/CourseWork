using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager sndMan;
    private AudioSource audioSrc;
    private AudioClip coinSound;
    private AudioClip hitSound;
    private AudioClip Enemy2hitSound;
    private AudioClip deathSound;
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSound = Resources.Load<AudioClip>("CoinPickup");
        hitSound = Resources.Load<AudioClip>("Shoot");
        Enemy2hitSound = Resources.Load<AudioClip>("Shoot");
        deathSound = Resources.Load<AudioClip>("Death");
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

    public void PlayEnemy2hitSound()
    {
        audioSrc.PlayOneShot(Enemy2hitSound);
    }

    public void PlayDeathSound()
    {
        audioSrc.PlayOneShot(deathSound);
    }
}
