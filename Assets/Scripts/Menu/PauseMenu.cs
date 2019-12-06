using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс паузы.
/// <remarks>Данный класс реализует меню паузы по нажатию клавиши ESC.</remarks>
/// </summary>
public class PauseMenu : MonoBehaviour
{
    /// <summary>
    /// Проверка на остановку игры.
    /// </summary>
    public static bool GameIsPaused = false;
    /// <summary>
    /// Само меню паузы.
    /// </summary>
    public GameObject pauseMenuUI;

    /// <summary>
    /// Проверка на нажатие клавиши.
    /// </summary>
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else {
                Pause();
            }
        }
    }
    /// <summary>
    /// Функция возобновления игры.
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    /// <summary>
    /// Функция остановки игры.
    /// </summary>
    public void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    /// <summary>
    /// Функция выхода в главное меню.
    /// </summary>
    public void GoMenu()
    {
        Resume();
        PlaterStats.collectedCoins = 0;
        PlaterStats.health = 10;
        PlaterStats.lives = 3;
        SceneManager.LoadScene("MainScene");
    }
}
