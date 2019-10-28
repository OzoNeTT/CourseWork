using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Enemy1 : MonoBehaviour
{

    // Use this for initialization
    private PlayerControl Player;

    private Transform target;
    public float radius;
    float rad2;
    public int damage = 3;
    bool attackanime;
    public float MoveSpeed;
    float fx, fy, fz;
    float dtime;
    bool attack = false;
    bool stay = false;
    float moveSorig;
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
        Player = FindObjectOfType<PlayerControl>();
        target = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        dtime = Time.time;
        rad2 = 8;
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        _anim.SetBool("attack", false);
        _anim.SetBool("walking", false);
        //GetComponent<Animator>().Play("Enemy1_idle");
        moveSorig = MoveSpeed;
        

    }

    // Update is called once per frame
    void Update()
    {
        
        if (attack)
        {
            _anim.SetBool("attack", true);
            
            //_anim.SetBool("walking", false);

            //GetComponent<Animator>().Play("Enemy1_Attack");
            //attack = false;
        }
        if (stay)
        {
            _anim.SetBool("walking", false);
            //_anim.SetBool("attack", false);

            //GetComponent<Animator>().Play("Enemy1_idle");
            //stay = false;
        }

        

        if (Vector2.Distance(transform.position, target.position) > radius)
        {
            //GetComponent<Animator>().Play("Enemy1_idle");
        }
        if (Vector2.Distance(transform.position, target.position) < radius && MoveSpeed != 0 && stay == false)
        {

            //transform.position = new Vector3(Vector3.MoveTowards(transform.position, Player.transform.position, MoveSpeed * Time.deltaTime).x, transform.position.y);
            transform.position = Vector2.MoveTowards(transform.position, target.position, MoveSpeed * Time.deltaTime);
            
            _anim.SetBool("attack", false);
            _anim.SetBool("walking", true);
            //GetComponent<Animator>().Play("Enemy1_walk");
            Flip();
        }

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            MoveSpeed = 0f;
            //GetComponent<Animator>().Play("Enemy1_Attack");
            if (Time.time >= dtime)
            {
                //GetComponent<Animator>().Play("Enemy1_Attack");
                attack = true;
                dtime = Time.time + 1f;

                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.GetComponent<AudioSource>().Play();
                //Player.KnockBackCount = 0.2f;
                //if (collision.transform.position.x < transform.position.x)
                //    Player.KnockFromRight = true;
                //else
                //    Player.KnockFromRight = false;
            }
        }


    }
    private void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
        {
            MoveSpeed = 0f;
            //GetComponent<Animator>().Play("Enemy1_Attack");
            if (Time.time >= dtime)
            {
                //GetComponent<Animator>().Play("Enemy1_Attack");
                //attack = true;
                SoundManager.sndMan.PlaySword();
                dtime = Time.time + 1f;

                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.GetComponent<AudioSource>().Play();
                //Player.KnockBackCount = 0.2f;
                //if (collision.transform.position.x < transform.position.x)
                //    Player.KnockFromRight = true;
                //else
                //    Player.KnockFromRight = false;
            }
        }
        if(collision.tag == "Borders")
        {      
            stay = true;
            if(transform.position.x > collision.transform.position.x && target.position.x > collision.transform.position.x)
            {
                stay = false;
            }
            else if (transform.position.x < collision.transform.position.x && target.position.x < collision.transform.position.x)
            {
                stay = false;
            }
        }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.GetComponent<PlaterStats>().get_health() == 6)
            {
                //_anim.SetBool("attack", true);
                //GetComponent<Animator>().Play("Enemy1_Attack");
            }
            StartCoroutine("wait");

        }
      
    }
    public void Flip()
    {

        if (Player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        else if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(fx, fy, fz);
    }
    private IEnumerator wait()
    {
        MoveSpeed = moveSorig;
        yield return new WaitForSeconds(2.0f);
    }
}
