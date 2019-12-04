using UnityEngine;
using System.Collections;

/// <summary>
/// Класс Шипы.
/// <remarks>Данный класс описывает объект Шипы, его нанесение урона игроку.</remarks>
/// </summary>
public class SpikesController : MonoBehaviour
{
    /// <summary>
    /// Наносимый игрок.
    /// </summary>
    public int damage = 4;

    /// <summary>
    /// Проверка на нахождение в тригерзоне шипов некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
        }
    }
}