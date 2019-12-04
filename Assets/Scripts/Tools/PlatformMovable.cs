using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// Класс движения платформы.
/// <remarks>Данный класс реализует движение платформы в вертикальном или горизонтальном направлениях с определенной скоростью.</remarks>
/// </summary>
public class PlatformMovable : MonoBehaviour
{
    /// <summary>
    /// Скорость передвижения.
    /// </summary>
    public float speed;
    /// <summary>
    /// Координаты левой точки.
    /// </summary>
    public Transform left;
    /// <summary>
    /// Координаты правой точки.
    /// </summary>
    public Transform right;
    /// <summary>
    /// Направление движения.
    /// </summary>
    public bool isHorizontal;
    /// <summary>
    /// Текущая скорость движения.
    /// </summary>
    float MoveSpeed;
    /// <summary>
    /// Функция задания первоначального состояния. Задается скорость движения платформы.
    /// </summary>
    void Start()
    {
        //speed = 7f;
        MoveSpeed = speed;

    }
    /// <summary>
    /// Функция покадрового обновления. Реализация движения к точкам.
    /// </summary>
    void Update()
    {
        if (isHorizontal)
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x > right.position.x)
            {
                MoveSpeed = -speed;

            }
            else if (transform.position.x < left.position.x)
            {
                MoveSpeed = speed;

            }
        }
        else if (!isHorizontal)
        {
            transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
            if (transform.position.y > right.position.y)
            {
                MoveSpeed = -speed;

            }
            else if (transform.position.y < left.position.y)
            {
                MoveSpeed = speed;

            }
        }
    }
    /// <summary>
    /// Функция возвращающая скорость движения платформы.
    /// </summary>
    /// <returns>Текущая скорость.</returns>
    public float get_Global_speed()
    {
        return MoveSpeed;
    }
}