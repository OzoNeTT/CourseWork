using UnityEngine;
using System.Collections;

public class Enemy2BulletController : MonoBehaviour {

    public float speedx, speedy = 0;
    public int damage;
    private PlayerControl Player;
    float fx, fy, fz;

    // Use this for initialization
    void Start()
    {
        Player = FindObjectOfType<PlayerControl>();
        //if (transform.position.x > 0)
        //{
        //    speed = -speed;
        //}
        //if (transform.position.x < 0)
        //{
        //    speed = -speed;
        //}
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Flip();
        if (Player.transform.localScale.x < transform.localScale.x)
        {
            speedx = 20;
        }
        if (Player.transform.localScale.x > transform.localScale.x)
        {
            speedx = -20;
        }

    }

    // Update is called once per frame
    void Update()
    {

        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            SoundManager.sndMan.PlayEnemy2hitSound();
            Destroy(this.gameObject);
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
        }
        if (other.tag == "Borders")
        {
            SoundManager.sndMan.PlayEnemy2hitSound();
            Destroy(this.gameObject);
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
