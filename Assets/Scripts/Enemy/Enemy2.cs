using UnityEngine;
using System.Collections;

public class Enemy2 : MonoBehaviour {

	// Use this for initialization
    public Transform firePoint;
    private PlayerControl Player;
    public float RepeatRate; 
    public GameObject Bullet;
    public float radius;
    private Transform target;
    float fx, fy, fz;
    public double nextFire = -1.0f;
    public AudioSource ShootSound;

    void Start () {
        //  InvokeRepeating("Shoot", 0f, RepeatRate);
        //  InvokeRepeating("Move", 0f, 0.1F);
        nextFire = Time.time;
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        GetComponent<Animator>().Play("Enemy2_Idle");
    }
	
	// Update is called once per frame
	void Update () {
        if (Vector2.Distance(transform.position, target.position) > radius)
        {
            GetComponent<Animator>().Play("Enemy2_Idle");
        }
        if (Vector2.Distance(transform.position, target.position) < radius)
        {

            //transform.position = new Vector3(Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime).x, transform.position.y);
            Flip();
            if (Time.time >= nextFire)
            {
                nextFire = Time.time + 1;
                Shoot();
            }

        }
    }
    public void Shoot()
    {
        ShootSound.pitch = 2f;
        ShootSound.Play();
        GameObject bullet = (GameObject)Instantiate(Bullet, firePoint.position, firePoint.rotation);
        GetComponent<Animator>().Play("Enemy2_Shoot");
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
        if (other.tag == "Player")
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

}
