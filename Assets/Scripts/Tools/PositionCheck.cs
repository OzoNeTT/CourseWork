using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс проверки на нахождения объекта в точке.
/// </summary>
public class PositionCheck : MonoBehaviour
{
    /// <summary>
    /// Проверка на нахождение объекта в точке.
    /// </summary>
    bool IsPlaced;
    /// <summary>
    /// Установка первоначального состояния.
    /// </summary>
    void Start()
    {
        IsPlaced = false;
    }
    /// <summary>
    /// Проверка на нахождение в тригерзоне некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Enemy2")
        {
            IsPlaced = true;
        }
        else
        {
            IsPlaced = false;
        }
    }
    /// <summary>
    /// Проверка на выход из тригерзоны некоторого объекта.
    /// </summary>
    /// <param name="collision">Коллайдер некоторого объекта.</param>
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Enemy2")
        {
            IsPlaced = false;
        }
    }
    /// <summary>
    /// Функция возвращающая состояние: установлен объект или нет.
    /// </summary>
    /// <returns>Установлен/Не установлен</returns>
    public bool GetPosition()
    {
        return IsPlaced;
    }
}