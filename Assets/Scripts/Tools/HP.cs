using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс здоровья.
/// <remarks>Данный класс реализует объект, при подборе которого игрок пополняет здоровье.</remarks>
/// </summary>
public class HP : MonoBehaviour
{
    /// <summary>
    /// Проверка на попадание в тригерзону объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            PlaterStats.health += 2;
            SoundManager.sndMan.PlayDrinkHP();
            Destroy(this.gameObject);
        }
    }
}