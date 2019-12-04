using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Класс управления персонажем.
/// <remarks>
/// Данный класс реализует управление персонажем в игре, проверяет на возможность совершения прыжка и движения в разных направлениях, а также реализует выстрел пулей.
/// </remarks>
/// </summary>
public class PlayerControl : MonoBehaviour
{
    /// <summary>
    /// Переменная, отвечающая за скорость движения.
    /// </summary>
    public float moveSpeed;
    /// <summary>
    /// Проверка на возможность совершить прыжок.
    /// </summary>
    public bool canDoubleJump;
    /// <summary>
    /// Размер пикселя.
    /// </summary>
    private float pixelSize = 0.02f;
    /// <summary>
    /// Клавиша "Движения вправо".
    /// </summary>
    public KeyCode R;
    /// <summary>
    /// Клавиша "Движения влево".
    /// </summary>
    public KeyCode L;
    /// <summary>
    /// Клавиша "Прыжок".
    /// </summary>
    public KeyCode spacebar;
    /// <summary>
    /// Клавиша "Выстрел".
    /// </summary>
    public KeyCode Return;
    /// <summary>
    /// Высота прыжка.
    /// </summary>
    public float jumpheight;
    /// <summary>
    /// Направление взгляда персонажа.
    /// </summary>
    public bool isFacingRight;
    /// <summary>
    /// Координаты точки для проверки нахождения на земле.
    /// </summary>
    public Transform groundCheck;
    /// <summary>
    /// Радиус для проверки нахождения на земле.
    /// </summary>
    public float groundCheckRadius;
    /// <summary>
    /// Слой, отвечающий за то, чем является земля.
    /// </summary>
    public LayerMask whatIsGround;
    /// <summary>
    /// Переменная для проверки находится ли персонаж на земле или нет.
    /// </summary>
    private bool grounded;
    /// <summary>
    /// Количество собранных монет
    /// </summary>
    public int collectedCoins = 0;
    /// <summary>
    /// Кордината X персонажа.
    /// </summary>
    float fx;
    /// <summary>
    /// Кордината Y персонажа.
    /// </summary>
    float fy;
    /// <summary>
    /// Кордината Z персонажа.
    /// </summary>
    float fz;
    /// <summary>
    /// Расстояние отбрасывания.
    /// </summary>
    public float KnockBackDist;
    /// <summary>
    /// Количество отбрасываний.
    /// </summary>
    public float KnockBackCount;
    /// <summary>
    /// Направление, от куда совершается отбрасывание.
    /// </summary>
    public bool KnockFromRight;
    /// <summary>
    /// Настоящая скорость движения персонажа.
    /// </summary>
    private float moveVelocity;
    /// <summary>
    /// Координаты точки, от куда будет выполняться выстрел.
    /// </summary>
    public Transform firePoint;
    /// <summary>
    /// Объект "Пуля".
    /// </summary>
    public GameObject Bullet;
    /// <summary>
    /// Время следующего выстрела.
    /// </summary>
    public double nextFire = -1.0f;
    /// <summary>
    /// Время следующео шага.
    /// </summary>
    double nextStep = -1.0f;
    /// <summary>
    /// Переменная класса статистики персонажа.
    /// </summary>
    private PlaterStats Player;
    /// <summary>
    /// Звук выстрела.
    /// </summary>
    public AudioSource ShootSound;
    /// <summary>
    /// Звук прыжка.
    /// </summary>
    public AudioSource JumpSound;

    /// <summary>
    /// Проверка на возможность движения вправо.
    /// </summary>
    private bool CanMoveRight;
    /// <summary>
    /// Проверка на возможность движения влево.
    /// </summary>
    private bool CanMoveLeft;

    /// <summary>
    /// Функция, вызывающаяся при инициализации объекта на сцене. Придает первоначальное состояние.
    /// </summary>
    void Start()
    {
        nextFire = Time.time;
        Player = FindObjectOfType<PlaterStats>();
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        CanMoveLeft = true;
        CanMoveRight = true;
    }

    /// <summary>
    /// Функция обновления каждый фрейм. В ней определены проверки на перемещение игрока и прыжок, а так же стрельба.
    /// </summary>
    void Update()
    {
        
        moveVelocity = 0;
        if (Input.GetKey(R) && CanMoveRight)
            moveVelocity = moveSpeed;
        else if (Input.GetKey(L) && CanMoveLeft)
            moveVelocity = -moveSpeed;
        //knockback
        if (moveVelocity != 0 && grounded)
        {
            if (Time.time >= nextStep)
            {
                nextStep = Time.time + 0.25;
                SoundManager.sndMan.PlayRunning();
            }
            
        }

        if (KnockBackCount <= 0)
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(moveVelocity, GetComponent<Rigidbody2D>().velocity.y);
            flip();
        }
        else
        {
            
            if (KnockFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(-KnockBackDist, GetComponent<Rigidbody2D>().velocity.y);
            if (!KnockFromRight)
                GetComponent<Rigidbody2D>().velocity = new Vector2(KnockBackDist, GetComponent<Rigidbody2D>().velocity.y);
            KnockBackCount -= Time.deltaTime;
        }

        //jump
        if (Input.GetKeyDown(spacebar))
        {
            if (grounded)
            {
                GetComponent<Animator>().Play("Jump");
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpheight));
                canDoubleJump = true;
                JumpSound.pitch = Random.Range(0.9f, 1.1f);
                JumpSound.Play();
            }
            else if (canDoubleJump)
            {
                GetComponent<Animator>().Play("Jump");
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpheight));
                canDoubleJump = false;
                JumpSound.pitch = Random.Range(0.9f, 1.1f);
                JumpSound.Play();
            }

           

        }

        //shoot
        if (Input.GetKeyDown(Return))
        {
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 0.4;
                Shoot();
            }
        }

        GetComponent<Animator>().SetFloat("Velocity", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
        GetComponent<Animator>().SetBool("Grounded", grounded);
    }

    /// <summary>
    /// Функция инвертирования. Трансформирует направление пресонажа в зависимости от направления перемещения.
    /// </summary>
    void flip()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            if (transform.localScale.x > 0)
            {
                transform.localScale = new Vector3(-1 * transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
            else if (transform.localScale.x < 0)
            {
                transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y, transform.localScale.z);
            }
        }
    }
    /// <summary>
    /// Обновление каждый фрейм для физичиеских объектов.
    /// </summary>
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    /// <summary>
    /// Функция стрельбы. Генерирует пулю.
    /// </summary>
    public void Shoot()
    {

        GetComponent<Animator>().Play("Fire");
        ShootSound.Play();
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);

    }

    /// <summary>
    /// Заход в тригер зону персонажа.
    /// </summary>
    /// <param name="other">Коллайдер объекта, попавшего в тригерзону.</param>
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Coin>())
        {
            Player.CollectCoin(1);
            Destroy(other.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }
    }

    /// <summary>
    /// Нахождение в некотором коллайдере.
    /// </summary>
    /// <param name="collision">Коллайдер другого объетка.</param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "MovePlatform")
        {
           //Transform cur = collision.transform;
           //cur.localScale = this.transform.localScale;
            transform.parent = collision.transform;
        }
    }

    /// <summary>
    /// Заход на некоторый коллайдер.
    /// </summary>
    /// <param name="collision">Коллайдер другого объетка.</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Borders")
        {
            if (collision.transform.localScale.x <= transform.localScale.x)
            {
                CanMoveLeft = true;
                CanMoveRight = false;
    
            }
            else if (collision.transform.localScale.x >= transform.localScale.x)
            {
                CanMoveRight = true;
                CanMoveLeft = false;
    
            }
        }
        if (collision.gameObject.tag == "Door")
        {
            if (collision.transform.localScale.x >= transform.localScale.x)
            {
                CanMoveLeft = true;
                CanMoveRight = false;

            }
            else if (collision.transform.localScale.x <= transform.localScale.x)
            {
                CanMoveRight = true;
                CanMoveLeft = false;

            }
        }

    }

    /// <summary>
    /// Выход с некоторого колайдера.
    /// </summary>
    /// <param name="collision">Коллайдер другого объетка.</param>
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Borders")
        {
            CanMoveRight = true;
            CanMoveLeft = true;
        }
        if (collision.gameObject.tag == "Door")
        {
            CanMoveRight = true;
            CanMoveLeft = true;
        }
        if (collision.gameObject.tag == "MovePlatform")
        {
            transform.parent = null;
        }
    }

}
