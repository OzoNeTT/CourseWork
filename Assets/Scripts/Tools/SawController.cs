using UnityEngine;
using System.Collections;

/// <summary>
/// Класс Пилы.
/// <remarks>Данный класс реализует поведения объекта "пила", его вращение и нанесение урона.</remarks>
/// </summary>
public class SawController : MonoBehaviour
{
    /// <summary>
    /// Наносимый урон.
    /// </summary>
    public int damage = 10;
    /// <summary>
    /// Источник звука для пилы.
    /// </summary>
    public AudioSource SawSound;
    /// <summary>
    /// Проверка на заход в тригерзону пилы некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SawSound.Play();
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            other.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                other.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }
}