using UnityEngine;
using System.Collections;
using System;

/// <summary>
/// Класс "Робота".
/// <remarks>Данный класс реализует поведение и стрельбу "Робота".</remarks>
/// </summary>
public class Enemy2 : MonoBehaviour {

    // Use this for initialization
    /// <summary>
    /// Объект здоровье. Может с некоторым шансом выпасть после смерти моба.
    /// </summary>
    public GameObject hill;
    /// <summary>
    /// Координаты точки выстрела.
    /// </summary>
    public Transform firePoint;
    /// <summary>
    /// Переменная класса управления персонажем.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Частота выстрелов.
    /// </summary>
    public float RepeatRate; 
    /// <summary>
    /// Объект пуля.
    /// </summary>
    public GameObject Bullet;
    /// <summary>
    /// Радиус поиска цели.
    /// </summary>
    public float radius;
    /// <summary>
    /// Координаты цели.
    /// </summary>
    private Transform target;
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
    /// Время следующего выстрела.
    /// </summary>
    public double nextFire = -1.0f;
    /// <summary>
    /// Звук выстрела.
    /// </summary>
    public AudioSource ShootSound;
    /// <summary>
    /// Аниматор моба "Робот".
    /// </summary>
    Animator _anim;
    /// <summary>
    /// Проверка на смерть объекта.
    /// </summary>
    bool dies = false;
    /// <summary>
    /// Функция установки первоначального состояния.
    /// </summary>
    void Start () {
        _anim = GetComponent<Animator>();
        nextFire = Time.time;
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        GetComponent<Animator>().Play("Enemy2_Idle");
    }
    /// <summary>
    /// Функция смерти. Уничтожение объекта и выпадание жизней с некоторым шансом.
    /// </summary>
    public void Dies()
    {
        dies = true;
        StartCoroutine("waitDies");
        System.Random rnd = new System.Random();
        float a = rnd.Next(0, 2);
        if (a <= 0.25)
        {
            Instantiate(hill, transform.position, transform.rotation);
        }

    }
    /// <summary>
    /// Функция покадрового обновления. Поиск цели и в случае нахождения начало стрельбы.
    /// </summary>
    void Update () {
        if (GameObject.Find("Player 1") != null)
        {
            if (Vector2.Distance(transform.position, target.position) > radius )
            {

            }
            if (Vector2.Distance(transform.position, target.position) < radius && !dies)
            {
                Flip();
                if (Time.time >= nextFire && !_anim.GetBool("dies"))
                {
                    nextFire = Time.time + 2;
                    
                    Shoot();
                }

            }
        }
    }
    /// <summary>
    /// Функция стрельбы. Воспроизводит анимацию стрельбы и создает объект пулю.
    /// </summary>
    public void Shoot()
    {
        ShootSound.pitch = 2f;
        ShootSound.Play();
        GetComponent<Animator>().Play("Enemy2_Shoot");
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
    }
    /// <summary>
    /// Проверка на попадание в коллайдер "Робота" некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !dies)
        {
            
            FindObjectOfType<PlaterStats>().TakeDamage(10);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < transform.position.x)
                Player.KnockFromRight = true;
            else
                Player.KnockFromRight = false;
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
    /// Корутина ожидания смерти объекта.
    /// </summary>
    private IEnumerator waitDies()
    {
        _anim.SetBool("dies", true);
        
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }

}
