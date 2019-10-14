using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public bool canDoubleJump;
    public KeyCode R;
    public KeyCode L;
    public KeyCode spacebar;
    public float jumpheight;
    public bool isFacingRight;
    public Transform groundCheck;
    public float groundCheckRadius;
    public LayerMask whatIsGround;
    private bool grounded;
    //public int collectedCoins = 0;
    float fx, fy, fz;
    public float KnockBackDist;
    public float KnockBackCount;
    public bool KnockFromRight;
    private float moveVelocity;
    public KeyCode Return;
    public Transform firePoint;
    public GameObject Bullet;
    public double nextFire = -1.0f;

    // Start is called before the first frame update
    void Start()
    {
        nextFire = Time.time;
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
    }

    // Update is called once per frame
    void Update()
    {
        moveVelocity = 0;
        if (Input.GetKey(R))
            moveVelocity = moveSpeed;
        else if (Input.GetKey(L))
            moveVelocity = -moveSpeed;


        //knockback
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
            }
            else if (canDoubleJump)
            {
                GetComponent<Animator>().Play("Jump");
                GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
                GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpheight));
                canDoubleJump = false;
            }

           

        }

        //shoot
        if (Input.GetKeyDown(Return))
        {
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 0.5;
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
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
        if (Input.GetKey(KeyCode.UpArrow))
        {

            if (Input.GetKey(L) || Input.GetKey(R))
                bullet.GetComponent<BulletController>().speedy = (bullet.GetComponent<BulletController>().speedx / 4);
            else
            {
                bullet.GetComponent<BulletController>().speedy = (bullet.GetComponent<BulletController>().speedx);
                bullet.GetComponent<BulletController>().speedx = 0;
            }
        }


    }

}
