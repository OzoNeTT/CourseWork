using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserScript : MonoBehaviour
{
    float dtime = 0;
    private Transform target;
    public float radius;
    
    void Start()
    {
        GetComponent<Animator>().Play("LaserIdle");
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

   
    void Update()
    {
        if (Vector2.Distance(transform.position, target.position) < radius)
        {
            dtime += Time.deltaTime;

            if (dtime >= 3.5)
            {
                SoundManager.sndMan.PlayLaser();
                dtime = 0;
            }
            if (dtime >= 3)
            {
                GetComponent<Animator>().Play("laserActive");

            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {
            
            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {
            
            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && dtime >= 3.4)
        {
            
            FindObjectOfType<PlaterStats>().TakeDamage(4);
            collision.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (collision.transform.position.x < this.transform.position.x)
                collision.GetComponent<PlayerControl>().KnockFromRight = true;

        }
    }

}
