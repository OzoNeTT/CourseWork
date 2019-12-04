using UnityEngine;
using System.Collections;

/// <summary>
/// Класс пули.
/// <remarks> Отвечает за направление и скорость движения пули, а так же проверяет на попадание.</remarks>
/// </summary>
public class BulletController : MonoBehaviour {
    /// <summary>
    /// Скорость по координате X.
    /// </summary>
    public float speedx;
    /// <summary>
    /// Скорость по координате Y.
    /// </summary>
    public float speedy;
    /// <summary>
    /// Объект игрок.
    /// </summary>
    private PlayerControl player;
    /// <summary>
    /// Урон выстрела.
    /// </summary>
    public float damage=10;
    /// <summary>
    /// Координата по X.
    /// </summary>
    float fx;
    /// <summary>
    /// Координата по Y.
    /// </summary>
    float fy;
    /// <summary>
    /// Координата по Z.
    /// </summary>
    float fz;

    /// <summary>
    /// Функция инициализации объекта "Пуля".
    /// </summary>
    void Start () {
        player = FindObjectOfType<PlayerControl>();
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        flip();
        if (player.transform.localScale.x<0)
        {
            speedx = -speedx;
        }
        
	}
	
	// Update is called once per frame
    /// <summary>
    /// Обновление каждый фрейм. Движения пули.
    /// </summary>
	void Update () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);
	}
    /// <summary>
    /// Проверка на то, куда попала пуля.
    /// </summary>
    /// <param name="other">Коллайдер некоторого объекта.</param>
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Dog")
        {
            SoundManager.sndMan.PlayHitSound();
            other.GetComponent<DogScript>().takeDamage(3);
            Destroy(this.gameObject);
        }
        if (other.tag == "Enemy")
        {
            SoundManager.sndMan.PlayHitSound();
            //Destroy(other.gameObject);
            other.GetComponent<Enemy1>().Dies();
            Destroy(this.gameObject);
        }
        if (other.tag == "Ghost")
        {
            SoundManager.sndMan.PlayHitSound();
            //Destroy(other.gameObject);
            other.GetComponent<EnemyGhostGFX>().Dies();
            Destroy(this.gameObject);
        }
        if (other.tag == "BossBullet")
        {
            SoundManager.sndMan.PlayHitSound();
            Destroy(other.gameObject);
            Destroy(this.gameObject);
        }
        if (other.tag == "Enemy2")
        {
            SoundManager.sndMan.PlayExplosion();
            other.GetComponent<Enemy2>().Dies();
            Destroy(this.gameObject);
        }
        if (other.tag == "Boss")
        {
            SoundManager.sndMan.PlayHitSound();
          
        }
        if (other.tag == "Borders" || other.tag == "Button" || other.tag == "Door")
        {
            SoundManager.sndMan.PlayHitSound();
            Destroy(this.gameObject);
            
        }

    }
    /// <summary>
    /// Функция отражения по вертикали, в зависимости от направления выстрела.
    /// </summary>
    public void flip()
    {

        if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3( fx, fy, fz);
        else if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
    }
}
