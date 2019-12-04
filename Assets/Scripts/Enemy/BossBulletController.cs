using UnityEngine;
using System.Collections;
/// <summary>
/// Класс управления пулей босса.
/// <remarks>Данный класс описывает движение пули, выпущенной атакой босса, ее наносимый урон и направление движения.</remarks>
/// </summary>
public class BossBulletController : MonoBehaviour {
    /// <summary>
    /// Переменная класса статистики игрока.
    /// </summary>
    private PlayerControl Player;
    /// <summary>
    /// Скорость движения пули.
    /// </summary>
    public float MoveSpeed = 50;
    /// <summary>
    /// Урон от пули.
    /// </summary>
    public int damage = 5;
    /// <summary>
    /// Одноразовая позиция игрока.
    /// </summary>
    public Vector3 oldplayerpos;
    /// <summary>
    /// Физический Rigidbody2D для описания движения объекта "пуля". 
    /// </summary>
    private Rigidbody2D rb;
    /// <summary>
    /// Функция установки первоначальных значений. Определяет позицию игрока.
    /// </summary>
	void Start () {
        Player = FindObjectOfType<PlayerControl>();
        oldplayerpos.x = Player.transform.position.x;
        oldplayerpos.z = Player.transform.position.z;
        rb = GetComponent<Rigidbody2D>();
        oldplayerpos.y = Player.transform.position.y + 4;
    
	}

    /// <summary>
    /// Функция покадрового обновления. Описывает движения пули в направлении игрока, а по достижению координат уничтожает объект "Пуля".
    /// </summary>
    void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, oldplayerpos, MoveSpeed * Time.deltaTime);

        if (this.transform.position == oldplayerpos)
            Destroy(this.gameObject);
    }
    /// <summary>
    /// Проверка на попадание в тригерзону пули некоторого объекта.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Destroy(this.gameObject);
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
            
        }
        if(other.tag == "Borders")
        {
            Destroy(this.gameObject);
        }
        if(other.gameObject.layer == 8)
        {
            Destroy(this.gameObject);
        }
        
    }
}
