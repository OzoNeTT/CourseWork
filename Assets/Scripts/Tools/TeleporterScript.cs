using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс телепорта.
/// <remarks>Данный класс реализует "телепорт" или возможность перемещаться между уровнями.</remarks>
/// </summary>
public class TeleporterScript : MonoBehaviour
{
    /// <summary>
    /// Переменная класса статистики игрока.
    /// </summary>
    private PlaterStats Player;
    /// <summary>
    /// Название текущего уровня.
    /// </summary>
    string lname;
    /// <summary>
    /// Необходимость анимации.
    /// </summary>
    bool notAnim;
    // Start is called before the first frame update
    /// <summary>
    /// Функция установки первоначального состояния, в зависимости от ситации обработка текстур.
    /// </summary>
    void Start()
    {
        if (GetComponent<Animator>() == null)
        {
            notAnim = true;
        }
        Player = FindObjectOfType<PlaterStats>();
        if (!notAnim)
            GetComponent<Animator>().Play("Teleporter_Idle");
        lname = SceneManager.GetActiveScene().name;
    }

    /// <summary>
    /// Проверка на заход в тригерзону телепорта некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {


        if (collision.tag == "Player")
        {
            FadeInOut.sceneEnd = true;
            if (!notAnim)
                GetComponent<Animator>().Play("Teleporter_Boom");

            Invoke("loadLevel", 1f);

        }
    }
    /// <summary>
    /// Функция загрузки следующего уровня.
    /// </summary>
    private void loadLevel()
    {

        if (lname == "Level0")
        {
            SceneManager.LoadScene("Level1");
        }
        else if (lname == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if (lname == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
    }
}