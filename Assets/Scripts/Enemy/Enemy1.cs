using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy1 : MonoBehaviour {

	// Use this for initialization
    private PlayerControl Player;

    private Transform target;
    public float radius;
    public int damage = 3;
    bool attackanime;
    public float MoveSpeed;
    float fx, fy, fz;

	void Start () {
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Vector2.Distance(transform.position, target.position) < radius)
        {
            //transform.position = new Vector3(Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime).x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            Flip();
        }
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            GetComponent<Animator>().Play("Enemy1_Attack");
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            other.GetComponent<AudioSource>().Play();
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
