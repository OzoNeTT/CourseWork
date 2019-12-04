using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс воспроизведения музыки.
/// <remarks>Данный класс служит для воспроизведения звуковых эффектов тех объектов, которые могут быть уничтожены. А также в данном классе применяется эффект "тряски" камеры.</remarks>
/// </summary>
public class SoundManager : MonoBehaviour
{
    /// <summary>
    /// Основная переменная класса, служит для воспроизведения аудиоклипов из других классов. 
    /// </summary>
    public static SoundManager sndMan;
    /// <summary>
    /// Источник звука.
    /// </summary>
    private AudioSource audioSrc;
    /// <summary>
    /// Звук подбора монеты.
    /// </summary>
    private AudioClip coinSound;
    /// <summary>
    /// Звук попадание пули персонажа.
    /// </summary>
    private AudioClip hitSound;
    /// <summary>
    /// Звук попадания пули "Врага2".
    /// </summary>
    private AudioClip Enemy2hitSound;
    /// <summary>
    /// Звук смерти.
    /// </summary>
    private AudioClip deathSound;
    /// <summary>
    /// Звук открытия двери.
    /// </summary>
    private AudioClip doorOpen;
    /// <summary>
    /// Звук взрыва.
    /// </summary>
    private AudioClip explosion;
    /// <summary>
    /// Звук шагов.
    /// </summary>
    private AudioClip runningSteps;
    /// <summary>
    /// Звук лазера.
    /// </summary>
    private AudioClip LaserSound;
    /// <summary>
    /// Звук пополнения здоровья.
    /// </summary>
    private AudioClip drinkHP;
    /// <summary>
    /// Звук подбора ключей.
    /// </summary>
    private AudioClip keywav;
    /// <summary>
    /// Звук меча.
    /// </summary>
    private AudioClip SwordSound;
    /// <summary>
    /// Звук лая.
    /// </summary>
    private AudioClip Bark;
    /// <summary>
    /// Звук смерти собаки.
    /// </summary>
    private AudioClip DogDeath;
    /// <summary>
    /// Переменная класса "тряски" камеры.
    /// </summary>
    public CameraShake camShake;
    
   /// <summary>
   /// В данном методе происходит инициализация всех переменных. В том числе и привязка звуковых эффектов из корневой дирректории.
   /// </summary>
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
        keywav = Resources.Load<AudioClip>("key");
    }

    // Update is called once per frame
    /// <summary>
    /// Одноразвое воспроизведение звука подбора ключей.
    /// </summary>
    public void PlayKeyWav()
    {
        audioSrc.PlayOneShot(keywav);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука пополнения здоровья.
    /// </summary>
    public void PlayDrinkHP()
    {
        audioSrc.PlayOneShot(drinkHP);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука смерти собаки.
    /// </summary>
    public void PlayDogDeath()
    {
        audioSrc.PlayOneShot(DogDeath);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука лая.
    /// </summary>
    public void PlayBark()
    {
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        audioSrc.PlayOneShot(Bark);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука подбора монеты.
    /// </summary>
    public void PlayCoinSound()
    {
        audioSrc.PlayOneShot(coinSound);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука удара мечом.
    /// </summary>
    public void PlaySword()
    {
        audioSrc.PlayOneShot(SwordSound);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука попадания пули персонажа и запуск "тряски" камеры.
    /// </summary>
    public void PlayHitSound()
    {
        StartCoroutine(camShake.Shake(.2f, .6f));
        audioSrc.PlayOneShot(hitSound);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука попадание пули "Врага2".
    /// </summary>
    public void PlayEnemy2hitSound()
    {
        audioSrc.PlayOneShot(Enemy2hitSound);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука смерти.
    /// </summary>
    public void PlayDeathSound()
    {
        audioSrc.PlayOneShot(deathSound);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука открытия двери.
    /// </summary>
    public void PlayDoorOpening()
    {
        audioSrc.PlayOneShot(doorOpen);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука взрыва и запуск "тряски" камеры.
    /// </summary>
    public void PlayExplosion()
    {
        StartCoroutine(camShake.Shake(.2f, .6f));
        audioSrc.PlayOneShot(explosion);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука шагов.
    /// </summary>
    public void PlayRunning()
    {
        audioSrc.pitch = Random.Range(0.8f, 1.2f);
        audioSrc.PlayOneShot(runningSteps);
    }
    /// <summary>
    /// Одноразвое воспроизведение звука лазера и запуск "тряски" камеры.
    /// </summary>
    public void PlayLaser()
    {
        StartCoroutine(camShake.Shake(.2f, .3f));
        audioSrc.PlayOneShot(LaserSound);
    }

}
