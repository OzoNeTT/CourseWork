using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Скрипт лазера.
/// <remarks>Данный класс реализует лазер, его переодическое срабатывание и нанесение урона игроку.</remarks>
/// </summary>
public class LaserScript : MonoBehaviour
{
    /// <summary>
    /// Временной интервал между атакой лазера.
    /// </summary>
    float dtime = 0;
    /// <summary>
    /// Координаты цели.
    /// </summary>
    private Transform target;
    /// <summary>
    /// Радиус обнаружения.
    /// </summary>
    public float radius;

    /// <summary>
    /// Функция задания првоначального состояния, установка анимации и обнаружение цели.
    /// </summary>
    void Start()
    {
        GetComponent<Animator>().Play("LaserIdle");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    /// <summary>
    /// Функция покадрового обновления. Проверка на попадание в радиус действия и после попадания начало срабатывания.
    /// </summary>
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < radius)
        {
            dtime += Time.deltaTime;

            if (dtime >= 3.5)
            {
                SoundManager.sndMan.PlayLaser();
                dtime = 0;
            }
            if (dtime >= 3)
            {
                GetComponent<Animator>().Play("laserActive");

            }
        }

    }
    /// <summary>
    /// Проверка на выход из тригерзоны лазера некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {

            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }
    /// <summary>
    /// Проверка на нахождение в тригерзоне лазера некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {

            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }
    /// <summary>
    /// Проверка на попадание в тригерзону лазера некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {

            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }

}