using UnityEngine;
using System.Collections;
/// <summary>
/// Класс управления пули "Робота". 
/// <remarks> Данный класс реализует движение пули и ее попадание в некоторый объект.</remarks>
/// </summary>
public class Enemy2BulletController : MonoBehaviour {
    /// <summary>
    /// Скорость движения по координате Х.
    /// </summary>
    public float speedx;
    /// <summary>
    /// Скорость движения по координате У.
    /// </summary>
    public float speedy = 0;
    /// <summary>
    /// Наносимый урон.
    /// </summary>
    public int damage;
    /// <summary>
    /// Объект игрок.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Координата по Х.
    /// </summary>
    float fx;
    /// <summary>
    /// Координата по У.
    /// </summary>
    float fy;
    /// <summary>
    /// Координата по Z.
    /// </summary>
    float fz;

    /// <summary>
    /// Функция придания первоначального состояния. Определение направления движения пули.
    /// </summary>
    void Start()
    {
        Player = FindObjectOfType<PlayerControl>();

        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Flip();
        if (Player.transform.position.x < transform.position.x)
        {
            speedx = -20;
        }
        if (Player.transform.position.x > transform.position.x)
        {
            speedx = 20;
        }

    }
    /// <summary>
    /// Функция покадрового обновления. Движение пули.
    /// </summary>
    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);

    }
    /// <summary>
    /// Функция проверки на попадание в коллайдер пули.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundManager.sndMan.PlayEnemy2hitSound();
            Destroy(this.gameObject);
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
        }
        if (other.tag == "Borders" || other.tag == "Door")
        {
            SoundManager.sndMan.PlayEnemy2hitSound();
            Destroy(this.gameObject);
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
}
