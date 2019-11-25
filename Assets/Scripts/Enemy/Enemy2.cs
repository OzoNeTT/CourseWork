using UnityEngine;
using System.Collections;
using System;
public class Enemy2 : MonoBehaviour {

    // Use this for initialization
    public GameObject hill;
    public Transform firePoint;
    private PlayerControl Player;
    public float RepeatRate; 
    public GameObject Bullet;
    public float radius;
    private Transform target;
    float fx, fy, fz;
    public double nextFire = -1.0f;
    public AudioSource ShootSound;
    Animator _anim;
    bool dies = false;
    void Start () {
        //  InvokeRepeating("Shoot", 0f, RepeatRate);
        //  InvokeRepeating("Move", 0f, 0.1F);
        _anim = GetComponent<Animator>();
        nextFire = Time.time;
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        GetComponent<Animator>().Play("Enemy2_Idle");
    }
    public void Dies()
    {
        dies = true;
        StartCoroutine("waitDies");
        System.Random rnd = new System.Random();
        float a = rnd.Next(0, 2);
        if (a <= 0.25)
        {
            Instantiate(hill, transform.position, transform.rotation);
        }

    }
    // Update is called once per frame
    void Update () {
        if (GameObject.Find("Player 1") != null)
        {
            if (Vector2.Distance(transform.position, target.position) > radius )
            {
                //GetComponent<Animator>().Play("Enemy2_Idle");
                //_anim.SetBool("shooting", false);
            }
            if (Vector2.Distance(transform.position, target.position) < radius && !dies)
            {
                
                //transform.position = new Vector3(Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime).x, transform.position.y);
                Flip();
                if (Time.time >= nextFire && !_anim.GetBool("dies"))
                {
                    nextFire = Time.time + 2;
                    
                    Shoot();
                }

            }
        }
    }
    public void Shoot()
    {
        //GetComponent<Animator>().Play("Enemy2_Shoot");
        ShootSound.pitch = 2f;
        ShootSound.Play();
        GetComponent<Animator>().Play("Enemy2_Shoot");
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
        
        //if (this.transform.rotation.y == 0)
        //    bullet.GetComponent<Enemy2BulletController>().speed = 20;
        //else
        //    bullet.GetComponent<Enemy2BulletController>().speed = -20;

    }
    //public void Move()
    //{
    //    this.transform.position = new Vector3(Vector3.MoveTowards(this.transform.position, Player.transform.position, 10 * Time.deltaTime).x, this.transform.position.y);
    //}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player" && !dies)
        {
            
            FindObjectOfType<PlaterStats>().TakeDamage(10);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < transform.position.x)
                Player.KnockFromRight = true;
            else
                Player.KnockFromRight = false;
        }
    }
    public void Flip()
    {

        if (Player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        else if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(fx, fy, fz);
    }
    private IEnumerator waitDies()
    {
        _anim.SetBool("dies", true);
        
        yield return new WaitForSeconds(.3f);
        Destroy(this.gameObject);
    }

}
