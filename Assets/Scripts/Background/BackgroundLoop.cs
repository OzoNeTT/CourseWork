using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс зацикливания изображения.
/// <remarks>Данный класс реализует непрерывную циркуляцию фонового изображения.</remarks>
/// </summary>
public class BackgroundLoop : MonoBehaviour
{
    /// <summary>
    /// Скорость движения по горизонтали.
    /// </summary>
    public float speed = 0.1f;
    /// <summary>
    /// Координаты смещения.
    /// </summary>
    private Vector2 offset = Vector2.zero;
    /// <summary>
    /// Материал, содержащий изображение.
    /// </summary>
    private Material material;
    /// <summary>
    /// Функция придает первоначальное состояние, устанавливает материал и смещение.
    /// </summary>
    void Start()
    {
        material = GetComponent<Renderer>().material;
        offset = material.GetTextureOffset("_MainTex");
    }

    /// <summary>
    /// Функция покадрового обновления. Производит смещение.
    /// </summary>
    void Update()
    {
        offset.x += speed * Time.deltaTime;
        material.SetTextureOffset("_MainTex", offset);
    }
}
