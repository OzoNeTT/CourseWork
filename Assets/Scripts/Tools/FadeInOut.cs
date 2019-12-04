using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/// <summary>
/// Класс затемнения экрана.
/// <remarks>Данный класс реализует затемнение экрана при появлении на новом уровне и при уходе с него.</remarks>
/// </summary>
public class FadeInOut : MonoBehaviour
{
    /// <summary>
    /// Проверка на достижение конца сцены.
    /// </summary>
    public static bool sceneEnd;
    /// <summary>
    /// Скорость затемнения.
    /// </summary>
    public float fadeSpeed = 1.5f;
    /// <summary>
    /// Изображение для затемнения.
    /// </summary>
    private Image _image;
    /// <summary>
    /// Проверка на начало сцены.
    /// </summary>
    private bool sceneStarting;
    /// <summary>
    /// Функция, вызывающаяся при загрузке сцены. В ней происходит затемнение.
    /// </summary>
    void Awake()
    {
        _image = GetComponent<Image>();
        _image.enabled = true;
        sceneStarting = true;
        sceneEnd = false;
        Cursor.visible = false;
    }
    /// <summary>
    /// По кадровое обновление. Проверка на начало или конец сцены.
    /// </summary>
    void Update()
    {
        if (sceneStarting) StartScene();
        if (sceneEnd) EndScene();
    }
    /// <summary>
    ///  Реализация снятия затемнения при начале сцены.
    /// </summary>
    void StartScene()
    {
        _image.color = Color.Lerp(_image.color, Color.clear, fadeSpeed * Time.deltaTime);

        if (_image.color.a <= 0.01f)
        {
            _image.color = Color.clear;
            _image.enabled = false;
            sceneStarting = false;
            Cursor.visible = true;
        }
    }
    /// <summary>
    /// Реализация затемнения при конце сцены.
    /// </summary>
    void EndScene()
    {
        _image.enabled = true;
        _image.color = Color.Lerp(_image.color, Color.black, fadeSpeed * Time.deltaTime);

        if (_image.color.a >= 0.95f)
        {
            Cursor.visible = false;
            _image.color = Color.black;
        }
    }
}