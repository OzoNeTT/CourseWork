using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{
    public float moveSpeed;
   // public bool canDoubleJump;
    public KeyCode R;
    public KeyCode L;
   // public KeyCode spacebar;
   // public float jumpheight;
    public bool isFacingRight;
   // public Transform groundCheck;
   // public float groundCheckRadius;
    //public LayerMask whatIsGround;
   // private bool grounded;
    //public int collectedCoins = 0;
    float fx, fy, fz;
    public float KnockBackDist;
    public float KnockBackCount;
    public bool KnockFromRight;
    private float moveVelocity;
    public KeyCode Return;
    //public Transform firePoint;
   // public GameObject Bullet;
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

        GetComponent<Animator>().SetFloat("Velocity", Mathf.Abs(GetComponent<Rigidbody2D>().velocity.x));
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
    
}
