using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс босса.
/// <remarks>Данный класс описывают движение босса, его атаку и взаимодействие с игроком.</remarks>
/// </summary>
public class BossScript : MonoBehaviour
{
    //points for moving
    /// <summary>
    /// Массив координат точек перемещения босса.
    /// </summary>
    public Transform[] points;
    /// <summary>
    /// Скорость перемещения.
    /// </summary>
    float Speed;
    /// <summary>
    /// Переменная класса управления игроком.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Координата точки выстрела.
    /// </summary>
    public Transform firepoint;
    /// <summary>
    /// Объект пуля.
    /// </summary>
    public GameObject Bullet;
    /// <summary>
    /// Объект моб "Рыцарь".
    /// </summary>
    public GameObject Enemies;
    /// <summary>
    /// Объект моб "Робот".
    /// </summary>
    public GameObject Enemies2;
    /// <summary>
    /// Звук атаки босса.
    /// </summary>
    private AudioClip BossSound;
    /// <summary>
    /// Источник звука для воспроизведения клипов.
    /// </summary>
    public AudioSource sndM;
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
    /// Количество здоровья босса.
    /// </summary>
    public int health;
    /// <summary>
    /// Пременная, показывающая зашел ли босс в одну из позиций или нет.
    /// </summary>
    bool enter = true;
    /// <summary>
    /// Следующая позиция босса.
    /// </summary>
    int point = 0;
    /// <summary>
    /// Текущая позиция босса.
    /// </summary>
    int currentPoint = 0;
    /// <summary>
    /// Отображение здоровья босса в худе.
    /// </summary>
    public Slider healthUI;
    /// <summary>
    /// Рандомайзер для определения следующей позиции.
    /// </summary>
    System.Random rnd;
    /// <summary>
    /// Массив позиций для босса.
    /// </summary>
    public GameObject[] target_2;
    /// <summary>
    /// Переменная показывающая состояния босса, жив или нет.
    /// </summary>
    bool dies = false;
    /// <summary>
    /// Аниматор текстур.
    /// </summary>
    Animator _anim;
    /// <summary>
    /// Функция для придания первоначального сотояния.
    /// </summary>
    void Start()
    {
        GetComponent<Animator>().Play("Boss_Appear");
        //InvokeRepeating("SpawnEnemies", 1f, 8);
        Player = FindObjectOfType<PlayerControl>();
        health = 100;
        BossSound = Resources.Load<AudioClip>("BossSound");
        StartCoroutine("boss");
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Speed = 1f;
        rnd = new System.Random();
        _anim = GetComponent<Animator>();
    }
    /// <summary>
    /// Функция, отвечающая за спавн врагов в разных позициях.
    /// </summary>
    public void SpawnEnemies()
    {
        sndM.PlayOneShot(BossSound);
        GetComponent<Animator>().Play("Boss_Skill");
        _anim.SetBool("Spawn", true);
        if (health <= 50)
        {

            if (target_2.Length <= 2)
            {
                points[0].GetComponent<Animator>().Play("PointSpawn");
                points[1].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies2, points[0].position, points[0].rotation);
                Instantiate(Enemies2, points[1].position, points[1].rotation);
            }
            if (points[3].GetComponent<PositionCheck>().GetPosition() == false)
            {
                points[3].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies, points[3].position, points[3].rotation);
            }
            if (points[4].GetComponent<PositionCheck>().GetPosition() == false)
            {
                points[4].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies, points[4].position, points[4].rotation);
            }
        }
        else
        {
            if (target_2.Length <= 2) {
                points[0].GetComponent<Animator>().Play("PointSpawn");
                points[1].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies2, points[0].position, points[0].rotation);
                Instantiate(Enemies2, points[1].position, points[1].rotation);
            }
        }
    }
    /// <summary>
    /// Функция обработки смерти босса.
    /// </summary>
    void Die()
    {
        dies = true;
        StartCoroutine("waitDies");
        SceneManager.LoadScene("WinScene");
        
        
    }
    /// <summary>
    /// Корутина для ожидания смерти.
    /// </summary>
    private IEnumerator waitDies()
    {
        _anim.SetBool("Dies", true);

        
        GameObject[] Enemys1 = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] Enemys2 = GameObject.FindGameObjectsWithTag("Enemy2");
        for(int i = 0; i < Enemys1.Length; i++)
        {
            Destroy(Enemys1[i].gameObject);
        }
        for (int i = 0; i < Enemys2.Length; i++)
        {
            Destroy(Enemys2[i].gameObject);
        }
        Destroy(this.gameObject);
        yield return new WaitForSeconds(1f);
    }
    /// <summary>
    /// Функция покадрового обновления, проверка на состояние и обновление показателя здоровья.
    /// </summary>
    void Update()
    {
        _anim.SetBool("Spawn", false);
        target_2 = GameObject.FindGameObjectsWithTag("Enemy");
        healthUI.value = health;
        if (GameObject.Find("Player 1") != null)
        {
            Flip();
        }
        if (health < 50)
        {
            Speed = 3f;

            if (FindObjectOfType<BossBulletController>() != null) 
            {
                FindObjectOfType<BossBulletController>().MoveSpeed = 40;
            }
        }
        if (health < 0)
        {
            Die();
        }
    }
    /// <summary>
    /// Проверка на попадание в коллайдер босса некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {


            FindObjectOfType<PlaterStats>().TakeDamage(2);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
        }
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            health -= 4;
            
        }

    }
    /// <summary>
    /// Корутина для обработки движения босса и вызова его атаки.
    /// </summary>
    IEnumerator boss()
    {
        //First
        while (!dies)
        {
            if (health >= 50)
            {
                while (this.transform.position.x != points[0].position.x)
                {

                    this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(points[0].position.x, this.transform.position.y), Speed);
                    currentPoint = 0;
                    yield return null;
                }
                //transform.localScale = new Vector3(-1 * fx, fy, fz);
                int i = 0;
                yield return new WaitForSeconds(1);
                while (i < 10)
                {

                    Instantiate(Bullet, firepoint.position, firepoint.rotation);
                    i++;
                    yield return new WaitForSeconds(.5f);
                }
                //second
                while (this.transform.position != points[2].position)
                {

                    this.transform.position = Vector2.MoveTowards(this.transform.position, points[2].position, Speed);
                    currentPoint = 2;

                    yield return null;
                }
                i = 0;


                SpawnEnemies();

                yield return new WaitForSeconds(1);
                while (i < 10)
                {

                    Instantiate(Bullet, firepoint.position, firepoint.rotation);
                    i++;
                    yield return new WaitForSeconds(.5f);
                }
                //third
                while (this.transform.position.x != points[1].position.x)
                {

                    this.transform.position = Vector2.MoveTowards(this.transform.position, points[1].position, Speed);
                    currentPoint = 1;
                    yield return null;
                }
                //transform.localScale = new Vector3(fx, fy, fz);
                i = 0;
                yield return new WaitForSeconds(1);
                while (i < 10)
                {

                    Instantiate(Bullet, firepoint.position, firepoint.rotation);
                    i++;
                    yield return new WaitForSeconds(.5f);
                }
            }
            else
            {
                int i = 0;
                int randnum = rnd.Next(0, 1);
                if (currentPoint == 0)
                {
                    point = (randnum == 0) ? 1 : 2;
                }
                else if (currentPoint == 1)
                {
                    point = (randnum == 0) ? 2 : 0;
                }
                else if (currentPoint == 2)
                {
                    point = (randnum == 0) ? 0 : 1;
                }

                while (this.transform.position != points[point].position)
                {

                    this.transform.position = Vector2.MoveTowards(this.transform.position, points[point].position, Speed);
                    currentPoint = point;

                    yield return null;
                }
                i = 0;


                SpawnEnemies();

                yield return new WaitForSeconds(2);
                while (i < 10)
                {

                    Instantiate(Bullet, firepoint.position, firepoint.rotation);
                    i++;
                    yield return new WaitForSeconds(1f);
                }
            }
            //4


        }
    }
    /// <summary>
    /// Функция отражения модели босса по вертикали в зависимости от позиции игрока.
    /// </summary>
    public void Flip()
    {

        if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        else if (Player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(fx, fy, fz);
    }

}
