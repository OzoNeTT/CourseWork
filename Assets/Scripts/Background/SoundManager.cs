﻿using System.Collections;
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
    }

    // Update is called once per frame
    public void PlayCoinSound()
    {
        audioSrc.PlayOneShot(coinSound);
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
