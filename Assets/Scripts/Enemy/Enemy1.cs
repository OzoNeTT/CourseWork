using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

/// <summary>
/// Класс моба "Рыцарь".
/// <remarks>В данном классе описано поведения моба "Рыцарь", его атака, движение, анимации и поиск игрока.</remarks>
/// </summary>
public class Enemy1 : MonoBehaviour
{
    /// <summary>
    /// Переменная класса управления персонажем.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Объект здоровье. Может с некоторым шансом выпасть после смерти моба.
    /// </summary>
    public GameObject hill;
    /// <summary>
    /// Координаты цели.
    /// </summary>
    private Transform target;
    /// <summary>
    /// Радиус поиска цели.
    /// </summary>
    public float radius;
    /// <summary>
    /// Наносимый урон.
    /// </summary>
    public int damage = 3;


    /// <summary>
    /// Скорость передвижения.
    /// </summary>
    public float MoveSpeed;
    /// <summary>
    /// Координата Х.
    /// </summary>
    float fx;
    /// <summary>
    /// Координата У.
    /// </summary>
    float fy;
    /// <summary>
    /// Координата Z.
    /// </summary>
    float fz;
    /// <summary>
    /// Промежуток времени для атаки.
    /// </summary>
    float dtime;
    /// <summary>
    /// Необходимость анимации атаки.
    /// </summary>
    bool attack = false;
    /// <summary>
    /// Необходимость анимации состояния покоя.
    /// </summary>
    bool stay = false;
    /// <summary>
    /// Скорость передвижения первоначальная.
    /// </summary>
    float moveSorig;
    /// <summary>
    /// Проверка на смерть объекта.
    /// </summary>
    bool dies = false;
    /// <summary>
    /// Аниматор моба "Рыцарь".
    /// </summary>
    Animator _anim;
    /// <summary>
    /// Функция установки первоначального состояния.
    /// </summary>
    void Start()
    {
        _anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dtime = Time.time;
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        _anim.SetBool("attack", false);
        _anim.SetBool("walking", false);
        moveSorig = MoveSpeed;
        

    }
    /// <summary>
    /// Функция смерти. Уничтожение объекта и выпадание жизней с некоторым шансом.
    /// </summary>
    public void Dies()
    {
        dies = true;
        StartCoroutine("waitAttack");
        System.Random rnd = new System.Random();
        float a = rnd.Next(0, 2);
        if (a <= 0.25)
        {
            Instantiate(hill, transform.position, transform.rotation);
        }

    }
    /// <summary>
    /// Функция покадрового обновления. Поиск цели и движение к ней, в случае достижения ее начало атаки.
    /// </summary>
    void Update()
    {
        
        if (attack && !dies)
        {
            SoundManager.sndMan.PlaySword();
            _anim.SetBool("attack", true);
            attack = false;
        }
        if (stay && !dies)
        {
            _anim.SetBool("walking", false);
            stay = false;
        }

        if (this.gameObject != null)
        {
            if (Vector2.Distance(transform.position, target.position) > radius)
            {
                _anim.SetBool("walking", false);
            }
            if (Vector2.Distance(transform.position, target.position) < radius && MoveSpeed != 0 && !stay && !dies)
            {
                transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);

                _anim.SetBool("attack", false);
                _anim.SetBool("walking", true);
                Flip();
            }
        }
    }
    /// <summary>
    /// Функция проверки захода в коллайдер моба "Рыцарь" некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            MoveSpeed = 0f;
            stay = true;

            if (Time.time >= dtime && !dies)
            {

                attack = true;
                dtime = Time.time + 1f;
                
                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.GetComponent<AudioSource>().Play();
            }
        }


    }
    /// <summary>
    /// Функция проверки нахождения в тригерзоне моба.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            MoveSpeed = 0f;
            stay = true;
            if (Time.time >= dtime && !dies)
            {
                attack = true;
                dtime = Time.time + 1f;

                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.GetComponent<AudioSource>().Play();
            }
        }
        if(collision.tag == "Borders")
        {
            MoveSpeed = 0f;
            stay = true;
            if(transform.position.x > collision.transform.position.x && target.position.x > collision.transform.position.x)
            {
               stay = false;
                StartCoroutine("wait");
            }
            else if (transform.position.x < collision.transform.position.x && target.position.x < collision.transform.position.x)
            {
                stay = false;
                StartCoroutine("wait");
            }
        }
        
    }
    /// <summary>
    /// Функция проверки на выход с коллайдера моба.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlaterStats>().get_health() == 6)
            {
                _anim.SetBool("attack", false);
                _anim.SetBool("walking", false);
                stay = false;
                attack = false;
            }
            StartCoroutine("wait");
    
        }
      
    }
    /// <summary>
    /// Функция отражения по вертикали в зависимости от позиции игрока.
    /// </summary>
    public void Flip()
    {

        if (Player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        else if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(fx, fy, fz);
    }
    /// <summary>
    /// Корутина, реализующая простой.
    /// </summary>
    private IEnumerator wait()
    {
        MoveSpeed = moveSorig;
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// Корутина ожидания смерти.
    /// </summary>
    private IEnumerator waitAttack()
    {
        MoveSpeed = 0f;
        _anim.SetBool("attack", false);
        _anim.SetBool("walking", false);
        _anim.SetBool("dies", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
}
