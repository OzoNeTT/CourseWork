using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public bool canDoubleJump;
    private float pixelSize = 0.02f;
    public KeyCode R;
    public KeyCode L;
    public KeyCode spacebar;
    public float jumpheight;
    public bool isFacingRight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    public int collectedCoins = 0;
    float fx, fy, fz;
    public float KnockBackDist;
    public float KnockBackCount;
    public bool KnockFromRight;
    private float moveVelocity;
    public KeyCode Return;
    public Transform firePoint;
    public GameObject Bullet;
    public double nextFire = -1.0f;
    double nextStep = -1.0f;
    private PlaterStats Player;
    public AudioSource ShootSound;
    public AudioSource JumpSound;
    

    private bool CanMoveRight;
    private bool CanMoveLeft;

    // Start is called before the first frame update
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

    // Update is called once per frame
    void Update()
    {
        
        moveVelocity = 0;
        //if (Input.GetKey(R))
        //    moveVelocity = moveSpeed;
        //else if (Input.GetKey(L))
        //    moveVelocity = -moveSpeed;
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
    void flip()
    {
        if (GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            transform.localScale = new Vector3(fx, fy, fz);

        }
        else if (GetComponent<Rigidbody2D>().velocity.x < 0)
        {
            transform.localScale = new Vector3(-1 * fx, fy, fz);

        }
    }
    void FixedUpdate()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundCheckRadius, whatIsGround);
    }

    public void Shoot()
    {
        

        GetComponent<Animator>().Play("Fire");
        ShootSound.Play();
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
        //if (Input.GetKey(KeyCode.UpArrow))
        //{
        //
        //    if (Input.GetKey(L) || Input.GetKey(R))
        //        bullet.GetComponent<BulletController>().speedy = (bullet.GetComponent<BulletController>().speedx / 4);
        //    else
        //    {
        //        bullet.GetComponent<BulletController>().speedy = (bullet.GetComponent<BulletController>().speedx);
        //        bullet.GetComponent<BulletController>().speedx = 0;
        //    }
        //}


    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.GetComponent<Coin>())
        {
            Player.CollectCoin(1);
            Destroy(other.gameObject);
            SoundManager.sndMan.PlayCoinSound();
        }
    }

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
    }

}
