using UnityEngine;
using System.Collections;

public class BulletController : MonoBehaviour {
    public float speedx,speedy;
    private PlayerControl player;
    public float damage=10;

    public AudioSource HitSound;
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<PlayerControl>();
        if(player.transform.localScale.x<0)
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
        
        if (other.tag == "Enemy")
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
        if (other.tag == "Borders" || other.tag == "Button" || other.tag == "Door")
        {
            SoundManager.sndMan.PlayHitSound();
            Destroy(this.gameObject);
            
        }

    }
}
