using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// Класс громкости.
/// <remarks>Данный класс служит для контролирования громкости игры.</remarks>
/// </summary>
public class VolumeControl : MonoBehaviour
{
    /// <summary>
    /// Слайдер для отображения "ползунка" громкости.
    /// </summary>
    public Slider slider;
    /// <summary>
    /// Уровень громкости.
    /// </summary>
    public Text valueCount;

    /// <summary>
    /// Покадровое обновление. Проверка на изменение положения ползунка и установка значения.
    /// </summary>
    void Update()
    {
        valueCount.text = slider.value.ToString();
        AudioListener.volume = slider.value;
    }
}
