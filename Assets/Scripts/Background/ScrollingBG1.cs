using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс движения фоновых изображений.
/// <remarks>Данный класс реализует плавное движение фоновых изображений путем их склейки и трансформирования.</remarks>
/// </summary>
public class ScrollingBG1 : MonoBehaviour
{
    /// <summary>
    /// Размер фонового изображения.
    /// </summary>
    public float backgroundSize;
    /// <summary>
    /// Координаты камеры.
    /// </summary>
    private Transform cameraTransform;
    /// <summary>
    /// Массив координат фоновых изображений
    /// </summary>
    private Transform[] layers;
    /// <summary>
    /// Область видимости.
    /// </summary>
    private float viewZone = 10;
    /// <summary>
    /// Левый индекс (крайне левое изображение).
    /// </summary>
    private int leftIndex;
    /// <summary>
    /// Правый индекс (крайне правое изображение).
    /// </summary>
    private int rightIndex;
    /// <summary>
    /// Функция придает первоначальное состояние. Установка координат, трансформирование и задание индексов для фоновых изображений.
    /// </summary>
    private void Start()
    {
        cameraTransform = Camera.main.transform;
        layers = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++) {
            layers[i] = transform.GetChild(i);
        }
        leftIndex = 0;
        rightIndex = layers.Length - 1;
    }
    /// <summary>
    /// Функция покадрового обновления. Перемещение фоновых изображений в зависимости от движения камеры.
    /// </summary>
    private void Update()
    {
        if (cameraTransform.position.x < (layers[leftIndex].transform.position.x + viewZone))
            ScrollLeft();
        if (cameraTransform.position.x > (layers[rightIndex].transform.position.x - viewZone))
            ScrollRight();
    }
    /// <summary>
    /// Функция перемещения фоновых изображений влево.
    /// </summary>
    private void ScrollLeft()
    {
        int lastRight = rightIndex;
        layers[rightIndex].position = Vector3.right * (layers[leftIndex].position.x - backgroundSize);
        leftIndex = rightIndex;
        rightIndex--;
        if(rightIndex < 0)
        {
            rightIndex = layers.Length - 1;
        }
    }
    /// <summary>
    /// Функция перемещения фоновых изображений вправо.
    /// </summary>
    private void ScrollRight()
    {
        int lastLeft = leftIndex;
        layers[leftIndex].position = Vector3.right * (layers[rightIndex].position.x + backgroundSize);
        rightIndex = leftIndex;
        leftIndex++;
        if (leftIndex == layers.Length)
        {
            leftIndex = 0;
        }
    }

}
