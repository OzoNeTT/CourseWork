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
    private AudioClip LaserSound;
    private AudioClip drinkHP;

    private AudioClip SwordSound;
    private AudioClip Bark;
    private AudioClip DogDeath;
    public CameraShake camShake;
    
   
    void Start()
    {
        sndMan = this;
        camShake = FindObjectOfType<CameraShake>();
        audioSrc = GetComponent<AudioSource>();
        coinSound = Resources.Load<AudioClip>("CoinPickup");
        hitSound = Resources.Load<AudioClip>("Shoot");
        Enemy2hitSound = Resources.Load<AudioClip>("Shoot");
        deathSound = Resources.Load<AudioClip>("Death");
        doorOpen = Resources.Load<AudioClip>("DoorOpen");
        explosion = Resources.Load<AudioClip>("Explosion");
        runningSteps = Resources.Load<AudioClip>("Step");
        LaserSound = Resources.Load<AudioClip>("laser");
        drinkHP = Resources.Load<AudioClip>("DrinkHP");
        SwordSound = Resources.Load<AudioClip>("Sword");
        Bark = Resources.Load<AudioClip>("Bark");
        DogDeath = Resources.Load<AudioClip>("DogHert");
    }

    // Update is called once per frame
    public void PlayDrinkHP()
    {
        audioSrc.PlayOneShot(drinkHP);
    }
    public void PlayDogDeath()
    {
        audioSrc.PlayOneShot(DogDeath);
    }
    public void PlayBark()
    {
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        audioSrc.PlayOneShot(Bark);
    }
    public void PlayCoinSound()
    {
        audioSrc.PlayOneShot(coinSound);
    }
    public void PlaySword()
    {
        audioSrc.PlayOneShot(SwordSound);
    }
    public void PlayHitSound()
    {
        StartCoroutine(camShake.Shake(.2f, .6f));
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
        StartCoroutine(camShake.Shake(.2f, .6f));
        audioSrc.PlayOneShot(explosion);
    }
    public void PlayRunning()
    {
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        audioSrc.PlayOneShot(runningSteps);
    }
    public void PlayLaser()
    {
        StartCoroutine(camShake.Shake(.2f, .3f));
        audioSrc.PlayOneShot(LaserSound);
    }

}
