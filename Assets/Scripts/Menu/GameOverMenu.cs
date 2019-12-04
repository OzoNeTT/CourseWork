using UnityEngine;
using System.Collections;

/// <summary>
/// Класс конца игры.
/// <remarks>Данный класс реализует </remarks>
/// </summary>
public class GameOverMenu : MonoBehaviour
{
    /// <summary>
    /// Номер уровня.
    /// </summary>
    public static int levelnumber;
    /// <summary>
    /// экземпляр класса.
    /// </summary>
    public static GameOverMenu instance = null;
    /// <summary>
    /// Функция вызывающаяся при запуске сцены.
    /// </summary>
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
    /// <summary>
    /// Функция "Перезапуска игры".
    /// </summary>
    public void StartGame()
    {
        Application.LoadLevel(levelnumber);
    }
    /// <summary>
    /// Нажатие на кнопку "Выход из игры".
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}