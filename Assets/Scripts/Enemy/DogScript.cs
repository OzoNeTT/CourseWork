using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
/// <summary>
/// Класс моба "Собака".
/// <remarks>Данный класс описывает поведение моба "Собака" его патрулирование, поиск игрока и атаку.</remarks>
/// </summary>
public class DogScript : MonoBehaviour
{
    /// <summary>
    /// Скорость передвижения.
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// Направление движения.
    /// </summary>
    public bool moveRight;
    /// <summary>
    /// Количество здоровья.
    /// </summary>
    int health;
    /// <summary>
    /// Объект здоровье.
    /// </summary>
    public GameObject hill;
    //wall
    /// <summary>
    /// Координаты точки для проверки стенок.
    /// </summary>
    public Transform wallCheck;
    /// <summary>
    /// Радиус проверки стенок.
    /// </summary>
    public float wallCheckRadius;
    /// <summary>
    /// Слой, отвечающий за то, чем является стена.
    /// </summary>
    public LayerMask whatIsWall;
    /// <summary>
    /// Переменная, показывающая, достиг ли моб стены или нет.
    /// </summary>
    public bool walled;

    //edge check
    /// <summary>
    /// Проверка на нахождения на угле.
    /// </summary>
    private bool notAtEdge;
    /// <summary>
    /// Координаты точки для проверки на углы.
    /// </summary>
    public Transform egdeCheck;
    /// <summary>
    /// Разница времени для атаки.
    /// </summary>
    float dtime;

    //player
    /// <summary>
    /// Радиус поиска игрока.
    /// </summary>
    public float Radius;
    /// <summary>
    /// Переменная класса управления игроком.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Аниматор моба.
    /// </summary>
    private Animator _anim;
    /// <summary>
    /// Координаты точки для отрисовки радиуса для проверки на нахождение в нем игрока.
    /// </summary>
    public Transform playerCheck;
    /// <summary>
    /// Радиус проверки.
    /// </summary>
    public float playerCheckRadius;
    /// <summary>
    /// Слой, показывающий, чем является объект "Игрок".
    /// </summary>
    public LayerMask whatIsPlayer;
    /// <summary>
    /// Переменная, показывающая, достиг ли моб игрока.
    /// </summary>
    public bool playered;
    /// <summary>
    /// Переменная, отвечающая за атаку моба.
    /// </summary>
    public static bool isAttacking = false;
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
    /// Первоначальная скорость моба "Собака".
    /// </summary>
    float OrigMoveSpeed;
    /// <summary>
    /// Переменная показывающая состояния моба, жив или нет.
    /// </summary>
    bool dies = false;
    /// <summary>
    /// Вспомогательная переменная, для атаки.
    /// </summary>
    bool canEnter = true;
    /// <summary>
    /// Функция для придания первоначального сотояния.
    /// </summary>
    void Start()
    {
        health = 6;
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Player = FindObjectOfType<PlayerControl>();
        _anim = GetComponent<Animator>();
        OrigMoveSpeed = moveSpeed;
        dtime = Time.time;
    }
    /// <summary>
    /// Функция обработки смерти моба.
    /// </summary>
    void Die()
    {
        SoundManager.sndMan.PlayDogDeath();
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
    /// Корутина ожидания смерти.
    /// </summary>
    private IEnumerator waitAttack()
    {
        moveSpeed = 0f;
        _anim.SetBool("Dies", true);
        yield return new WaitForSeconds(.8f);
        Destroy(this.gameObject);
    }
    /// <summary>
    /// Функция получения урона.
    /// </summary>
    /// <param name="a">Количество урона.</param>
    public void takeDamage(int a)
    {
        health -= a;
    }
    /// <summary>
    /// Функция покадрового обновления, в ней происходит передвижения моба, поиск игрока, его атака и обработка звуков с анимациями.
    /// </summary>
    void Update()
    {
        walled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(egdeCheck.position, wallCheckRadius, whatIsWall);

        playered = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);
        if (Vector2.Distance(transform.position, Player.transform.position) > Radius && !dies)
        {
            moveSpeed = OrigMoveSpeed;
            canEnter = true;
            _anim.SetBool("IsRun", false);
            if (walled)
            {
                Flip2();
            }
            if (moveRight)
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                //transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }    
            // Патруль
        }
        else if(Vector2.Distance(transform.position, Player.transform.position) <= Radius && !dies)
        {
            // Атака игрока 
            if (canEnter)
            {
                moveSpeed = OrigMoveSpeed + 10;
                canEnter = false;
            }

            Flip();
            _anim.SetBool("IsRun", true);
            
            if (moveRight)
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
               
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                //transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (playered)
            {
                _anim.SetBool("IsAttacking", true);
            }
            else
            {
                _anim.SetBool("IsAttacking", false);
            }
        }
        
        if (health < 0)
        {
            health = 6;
            Die();
        }
    }
    /// <summary>
    /// Функция отражения модели моба по вертикали в зависимости от позиции игрока.
    /// </summary>
    public void Flip()
    {

        if (Player.transform.position.x > transform.position.x)
        {
            moveRight = true;
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        }
        else if (Player.transform.position.x < transform.position.x)
        {
            moveRight = false;
            transform.localScale = new Vector3(fx, fy, fz);
        }
    }
    /// <summary>
    /// Функция отражения модели моба по вертикали в зависимости от достижения стенок.
    /// </summary>
    public void Flip2()
    {

        if (wallCheck.position.x < transform.position.x)
        {
            moveRight = true;
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        }
        else if (wallCheck.position.x > transform.position.x)
        {
            moveRight = false;
            transform.localScale = new Vector3(fx, fy, fz);
        }
    }
    /// <summary>
    /// Проверка на попадание в тригерзону моба некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !dies)
        {

            moveSpeed = 0f;
            if (Time.time >= dtime)
            {
                dtime = Time.time + .75f;
                SoundManager.sndMan.PlayBark();
                FindObjectOfType<PlaterStats>().TakeDamage(2);
            }
        }
    }
    /// <summary>
    /// Проверка на нахождение в тригерзоне моба некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоорого объекта.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !dies)
        {
            moveSpeed = 0f;
            if (Time.time >= dtime)
            {
                dtime = Time.time + .75f;
                SoundManager.sndMan.PlayBark();
                FindObjectOfType<PlaterStats>().TakeDamage(2);
            }
        }
    }
    /// <summary>
    /// Проверка на выход из тригерзоны некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player" && !dies)
        {
            moveSpeed = OrigMoveSpeed;
            canEnter = true;
        }
    }
}

