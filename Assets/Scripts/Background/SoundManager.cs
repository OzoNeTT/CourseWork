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
    private AudioClip doorOpen;
    private AudioClip explosion;
    private AudioClip runningSteps;
    void Start()
    {
        sndMan = this;
        audioSrc = GetComponent<AudioSource>();
        coinSound = Resources.Load<AudioClip>("CoinPickup");
        hitSound = Resources.Load<AudioClip>("Shoot");
        Enemy2hitSound = Resources.Load<AudioClip>("Shoot");
        deathSound = Resources.Load<AudioClip>("Death");
        doorOpen = Resources.Load<AudioClip>("DoorOpen");
        explosion = Resources.Load<AudioClip>("Explosion");
        runningSteps = Resources.Load<AudioClip>("Step");
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
    public void PlayDoorOpening()
    {
        audioSrc.PlayOneShot(doorOpen);
    }
    public void PlayExplosion()
    {
        audioSrc.PlayOneShot(explosion);
    }
    public void PlayRunning()
    {
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        audioSrc.PlayOneShot(runningSteps);
    }

}
