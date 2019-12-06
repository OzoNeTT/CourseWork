using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// Класс выбора уровня.
/// <remarks>Данный класс служит для собственного выбора уровня для игры в главном меню.</remarks>
/// </summary>
public class LoadLevel : MonoBehaviour
{
    /// <summary>
    /// Вызывается при нажатии кнопки "Back". Возвращает в меню.
    /// </summary>
    public void OnButtonBack()
    {
        SceneManager.LoadScene("MainScene");
    }
    /// <summary>
    /// Вызывается при нажатии кнопки "Level 1". Загружает уровень.
    /// </summary>
    public void OnButtonLevel0()
    {
        SceneManager.LoadScene("Level0");
    }/// <summary>
     /// Вызывается при нажатии кнопки "Level 2". Загружает уровень.
     /// </summary>
    public void OnButtonLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    /// <summary>
    /// Вызывается при нажатии кнопки "Level 3". Загружает уровень.
    /// </summary>
    public void OnButtonLevel2()
    {
        SceneManager.LoadScene("Level2");
    }
    /// <summary>
    /// Вызывается при нажатии кнопки "Boss". Загружает уровень.
    /// </summary>
    public void OnButtonBoss()
    {
        SceneManager.LoadScene("Level3");
    }

}
