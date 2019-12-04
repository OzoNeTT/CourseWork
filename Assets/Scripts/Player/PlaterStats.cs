using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// Класс статистики персонажа.
/// <remarks> Класс, предназначенный для хранения информации об игроке (количество жизней, здоровье, очки, неуязвимость)</remarks>
/// </summary>
public class PlaterStats : MonoBehaviour
{
    /// <summary>
    /// Здоровье игрока.
    /// </summary>
    public static int  health = 10;
    /// <summary>
    /// Жизни игрока.
    /// </summary>
    public static int lives = 3;
    /// <summary>
    /// Время моргания персонажа.
    /// </summary>
    public float flickerDuration = 0.1f;
    /// <summary>
    /// Время нахождения в режиме моргания.
    /// </summary>
    private float flickerTime = 0f;
    /// <summary>
    /// Переменная класса рендеринга текстур. 
    /// </summary>
    private SpriteRenderer spriteRenderer;
    /// <summary>
    /// Проверка на нахождение в режиме неуязвимости.
    /// </summary>
    public bool isImmune = false;
    /// <summary>
    /// Длительность режима неуязвимости.
    /// </summary>
    public float immunityDuraction = 1f;
    /// <summary>
    /// Время нахождения в режиме неуязвимости.
    /// </summary>
    private float immunityTime = 0f;
    /// <summary>
    /// Количество собранных монет.
    /// </summary>
    public static  int collectedCoins = 0;
    /// <summary>
    /// Очки для отображения в худе.
    /// </summary>
    public Text scoreUI;
    /// <summary>
    /// Жизни для отображения в худе.
    /// </summary>
    public Text LiveUI;
    /// <summary>
    /// Здоровье для отображения в худе.
    /// </summary>
    public Slider healthUI;
    /// <summary>
    /// Точка спавна персонажа.
    /// </summary>
    [SerializeField] Transform spawnPoint;

    /// <summary>
    /// Функция придает первоначальное состояние. Получения объекта "SpriteRenderer" от игрока.
    /// </summary>
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    /// <summary>
    /// Функция обновления каждый фрейм. В ней происходит обновление худа и проверки на то, жив ли игрок или нет.
    /// </summary>
    void Update()
    {
        scoreUI.text = "" + PlaterStats.collectedCoins.ToString();
        LiveUI.text = "" + PlaterStats.lives.ToString();
        healthUI.value = health;
        if (this.isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if (immunityTime >= immunityDuraction)
            {
                this.isImmune = false;
                this.spriteRenderer.enabled = true;
            }
        }
        if (health > 10 && lives == 3)
        {
            health = 10;
        }
        else if(health > 10 && lives < 3)
        {
            health = 2;
            lives++;
        }
    }

    /// <summary>
    /// Функция моргания игрока.
    /// </summary>
    void SpriteFlicker()
    {
        if (this.flickerTime < this.flickerDuration)
        {
            this.flickerTime = this.flickerTime + Time.deltaTime;
        }
        else if (this.flickerTime >= this.flickerDuration)
        {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            this.flickerTime = 0;
        }
    }
    /// <summary>
    /// Функция получения урона
    /// </summary>
    /// <param name="damage">Урон, наносимый игроку.</param>
    public void TakeDamage(int damage)
    {
        if (this.isImmune == false)
        {
            GetComponent<Animator>().Play("TakeDamage");
            PlaterStats.health = PlaterStats.health - damage;
            if (PlaterStats.health < 0f)
                PlaterStats.health = 0;
            if (PlaterStats.lives > 0f && PlaterStats.health == 0f)
            {
                SoundManager.sndMan.PlayDeathSound();
                PlaterStats.health = 10;
                PlaterStats.lives--;
                transform.position = spawnPoint.position;
            }
            else if (PlaterStats.lives == 0 && PlaterStats.health == 0)
            {
                Debug.Log("GAMEOVER!!");
                PlaterStats.collectedCoins = 0;
                PlaterStats.lives = 3;
                PlaterStats.health = 10;
                GameOverMenu.levelnumber = Application.loadedLevel;
                Destroy(this.gameObject);
                Application.LoadLevel("GameOverScene");
            }
            PlayHitReaction();
        }


    }
    /// <summary>
    /// Функция прорисовки получения урона.
    /// </summary>
    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0f;
    }
    /// <summary>
    /// Функция сбора монеты.
    /// </summary>
    /// <param name="coinValue">Количество монет.</param>
    public void CollectCoin(int coinValue)
    {
        PlaterStats.collectedCoins = PlaterStats.collectedCoins + coinValue;
    }
    /// <summary>
    /// Функция получения количества здоровья.
    /// </summary>
    /// <returns>Возвращает текущее здоровье.</returns>
    public int get_health()
    {
        return health;
    }
}
