using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс трансформирования фона.
/// <remarks>Класс трансформирует фоновое изображение к масштабу камеры.</remarks>
/// </summary>
public class Background : MonoBehaviour
{
    // Start is called before the first frame update
    /// <summary>
    /// Функция покадрового обновления для трансформирования объекта "Background".
    /// </summary>
    void Start()
    {
        var height = Camera.main.orthographicSize * 2F;
        var width = height * Screen.width / Screen.height;

        if(gameObject.name == "Background")
        {
            transform.localScale = new Vector3(width, height, 0);
        }
        else
        {
            transform.localScale = new Vector3(width + 3f, 5, 0);
        }
    }
}
