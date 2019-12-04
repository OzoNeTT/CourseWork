using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

/// <summary>
/// Класс моба "Призрак".
/// <remarks>Данный класс реализует пведение моба "Призрак", его атаку и передвижение.</remarks>
/// </summary>
public class EnemyGhostGFX : MonoBehaviour
{
    /// <summary>
    /// Переменная пути, для определения траектории движения до цели.
    /// </summary>
    Path path;
    /// <summary>
    /// Координаты цели.
    /// </summary>
    public Transform target;
    /// <summary>
    /// Скорость движения.
    /// </summary>
    public float speed = 400f;
    /// <summary>
    /// Дистанция до следующей точки.
    /// </summary>
    public float nextWaypointDistance = 3f;
    /// <summary>
    /// Текущая точка.
    /// </summary>
    int currentWaypoint = 0;
    /// <summary>
    /// Переменная для проверки на достиженя конца пути.
    /// </summary>
    bool reachedEndOfPath = false;
    /// <summary>
    /// Аниматор для призрака.
    /// </summary>
    public Animator _anim;
    /// <summary>
    /// Поисковик кратчайшего пути.
    /// </summary>
    Seeker seeker;
    /// <summary>
    /// Rigidbody2d который установлен на призраке.
    /// </summary>
    Rigidbody2D rb;
    /// <summary>
    /// Промежуток времени для воспроизведения звука.
    /// </summary>
    float dtime;
    /// <summary>
    /// Промежуток времени для атаки.
    /// </summary>
    float deltatime;
    /// <summary>
    /// Источник звука призрака.
    /// </summary>
    private AudioSource snd;
    /// <summary>
    /// Звук призрака.
    /// </summary>
    private AudioClip GhostSound;
    /// <summary>
    /// Является ли объект живым.
    /// </summary>
    bool isAlive = true;
    /// <summary>
    /// Наносимый урон.
    /// </summary>
    int damage = 2;
    /// <summary>
    /// Пауза воспроизведения звука.
    /// </summary>
    bool soundPaused = false;
    /// <summary>
    /// Дистанция сканирования цели.
    /// </summary>
    public float PlayerRadius = 50;
    /// <summary>
    /// Функция установки первоначальных состояний.
    /// </summary>
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //_anim = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        _anim.SetBool("isPlayer", false);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        snd = GetComponent<AudioSource>();
        GhostSound = Resources.Load<AudioClip>("ghost");
        dtime = Time.time;
        deltatime = Time.time;

    }
    /// <summary>
    /// Обновление критического пути до цели.
    /// </summary>
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    /// <summary>
    /// Функция, вызывающаяся если достигнут конец пути.
    /// </summary>
    /// <param name="p">Некоторый путь.</param>
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    /// <summary>
    /// Функция покадрового обновления для физический объектов. В ней происходит поиск наилучшего пути до цели, следование по нему и атака персонажа.
    /// </summary>
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) <= PlayerRadius && isAlive)
        {

            if (Time.time >= dtime)
            {
                dtime = Time.time + 23f;
                snd.PlayOneShot(GhostSound);
            }
            if (soundPaused)
            {
                snd.UnPause();
            }
            _anim.SetBool("isPlayer", true);
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            snd.Pause();
            soundPaused = true;
            _anim.SetBool("isPlayer", false);
        }
    }
    /// <summary>
    /// Функция смерти призрака.
    /// </summary>
    public void Dies()
    {
        isAlive = false;
        rb.velocity = new Vector3(0, 0, 0);

        StartCoroutine("waitAttack");

    }
    /// <summary>
    /// Корутина ожидания смерти после атаки персонажа.
    /// </summary>
    /// <returns></returns>
    private IEnumerator waitAttack()
    {
        _anim.SetBool("isPlayer", false);
        _anim.SetBool("Dies", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    /// <summary>
    /// Проверка на попадание некоторого объекта в коллайдер призрака.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Time.time >= deltatime && isAlive)
            {
                deltatime = Time.time + 1f;
                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.transform.GetComponent<AudioSource>().Play();
                FindObjectOfType<PlayerControl>().KnockBackCount = 0.1f;
                if (collision.transform.position.x < transform.position.x)
                    FindObjectOfType<PlayerControl>().KnockFromRight = true;
                else
                    FindObjectOfType<PlayerControl>().KnockFromRight = false;
            }
        }
    }


}
