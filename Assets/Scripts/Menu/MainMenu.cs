using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс Главной игры.
/// <remarks>Данный класс реализует главное меню.</remarks>
/// </summary>
public class MainMenu : MonoBehaviour
{
    /// <summary>
    /// Функция запуска игры.
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene("Level0");
    }
    /// <summary>
    /// Функция выхода из игры.
    /// </summary>
    public void ExitGame()
    {
        Application.Quit();
    }

    public void ChooseLevel()
    {
        SceneManager.LoadScene("LevelSelectScene");
    }
}