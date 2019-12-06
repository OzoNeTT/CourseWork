using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using System;
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
    /// Текст для вывода количество очков после игры.
    /// </summary>
    public Text ScoreTextUI;
    /// <summary>
    /// Переменная, хранящая название текущей сцены.
    /// </summary>
    public string lname;
    /// <summary>
    /// Функция установки первоначальных значений.
    /// </summary>
    void Start()
    {
        if(ScoreTextUI != null)
            ScoreTextUI.text = PlaterStats.collectedCoins.ToString();
        lname = SceneManager.GetActiveScene().name;
    }
    /// <summary>
    /// Функция вызывающаяся при запуске сцены.
    /// </summary>
    void Awake()
    {
        if(lname == "WinScene")
        {
            if (!Directory.Exists("Results"))
            {
                Directory.CreateDirectory("Results");
            }
            if (!File.Exists("Results/result.txt"))
            {
                File.Create("Results/result.txt");
            }
            File.AppendAllText("Results/result.txt", "Game result: " + PlaterStats.collectedCoins + Environment.NewLine);
        }
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
        PlaterStats.collectedCoins = 0;
        PlaterStats.health = 10;
        PlaterStats.lives = 3;
        SceneManager.LoadScene("MainScene");
    }
    /// <summary>
    /// Нажатие на кнопку "Выход из игры".
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }
}