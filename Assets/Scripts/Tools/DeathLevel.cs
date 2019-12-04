using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс смертельной зону.
/// <remarks>Данный класс реализует зону, попадая в которую игрок автоматически умирает и возрождается в точке спавна.</remarks>
/// </summary>
public class DeathLevel : MonoBehaviour
{
    /// <summary>
    /// Координаты  точки спавна.
    /// </summary>
    public Transform spawnPoint;
    /// <summary>
    /// Переменная класса управления игроком.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Источник звука, для воспроизведения звука смерти.
    /// </summary>
    public AudioSource DeathSound;
    /// <summary>
    /// Проверка на попадание в коллайдер некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            DeathSound.pitch = Random.Range(0.9f, 1.1f);
            DeathSound.Play();
            FindObjectOfType<PlaterStats>().TakeDamage(10);
            collision.transform.position = spawnPoint.position;
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}