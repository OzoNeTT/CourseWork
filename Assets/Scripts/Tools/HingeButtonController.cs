using UnityEngine;
using System.Collections;

/// <summary>
/// Класс кнопки.
/// <remarks>Данный класс реализует нажатие кнопки при попадании пули игрока.</remarks>
/// </summary>
public class HingeButtonController : MonoBehaviour
{

    /// <summary>
    /// Объект двери.
    /// </summary>
    public GameObject hinge;
    /// <summary>
    /// Величина сдвига при нажатии.
    /// </summary>
    float x = -5f;
    /// <summary>
    /// Проверка на попадание в коллайдер некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер другого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            SoundManager.sndMan.PlayDoorOpening();
            hinge.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.Translate(x, transform.position.y, transform.position.z);
        }
    }
}