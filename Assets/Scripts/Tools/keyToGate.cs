using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс ключа.
/// <remarks>Данный класс реализует открытие двери при подборе ключа.</remarks>
/// </summary>
public class keyToGate : MonoBehaviour
{
    /// <summary>
    /// Объект двери.
    /// </summary>
    public GameObject gate;
    /// <summary>
    /// Проверка на попадание в тригерзону ключа некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объект.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            SoundManager.sndMan.PlayKeyWav();
            Destroy(this.gameObject);
            Destroy(gate);
        }
    }
}