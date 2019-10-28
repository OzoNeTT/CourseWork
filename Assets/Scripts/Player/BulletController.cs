using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public float speedx,speedy;
    private PlayerControl player;
    public float damage=10;
    float fx, fy, fz;
    public AudioSource HitSound;

    // Use this for initialization
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
	void Update () {
        //GetComponent<Rigidbody2D>().velocity = new Vector2(speed, GetComponent<Rigidbody2D>().velocity.y);
        GetComponent<Rigidbody2D>().velocity = new Vector2(speedx, speedy);
	}
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
            Destroy(other.gameObject);
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
            Destroy(other.gameObject, 0.25f);
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
    public void flip()
    {

        if (player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3( fx, fy, fz);
        else if (player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
    }
}
