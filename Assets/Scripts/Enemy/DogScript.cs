﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogScript : MonoBehaviour
{
    public float moveSpeed;
    public bool moveRight;
    int health;


    //wall
    public Transform wallCheck;
    public float wallCheckRadius;
    public LayerMask whatIsWall;
    public bool walled;

    //edge check
    private bool notAtEdge;
    public Transform egdeCheck;
    float dtime;

    //player
    public float Radius;
    private PlayerControl Player;
    private Animator _anim;
    public Transform playerCheck;
    public float playerCheckRadius;
    public LayerMask whatIsPlayer;
    public bool playered;
    public static bool isAttacking = false;
    float fx, fy, fz;

    float OrigMoveSpeed;

    bool canEnter = true;
    void Start()
    {
        health = 6;
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Player = FindObjectOfType<PlayerControl>();
        _anim = GetComponent<Animator>();
        OrigMoveSpeed = moveSpeed;
        dtime = Time.time;
    }
    void Die()
    {
        SoundManager.sndMan.PlayDogDeath();
        Destroy(this.gameObject);
    }
    public void takeDamage(int a)
    {
        health -= a;
    }
    void Update()
    {
        walled = Physics2D.OverlapCircle(wallCheck.position, wallCheckRadius, whatIsWall);

        notAtEdge = Physics2D.OverlapCircle(egdeCheck.position, wallCheckRadius, whatIsWall);

        playered = Physics2D.OverlapCircle(playerCheck.position, playerCheckRadius, whatIsPlayer);
        if (Vector2.Distance(transform.position, Player.transform.position) > Radius)
        {
            moveSpeed = OrigMoveSpeed;
            canEnter = true;
            _anim.SetBool("IsRun", false);
            if (walled)
            {
                Flip2();
            }
            if (moveRight)
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                //transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }    
            // Патруль
        }
        else
        {
            // Атака игрока 
            if (canEnter)
            {
                moveSpeed = OrigMoveSpeed + 10;
                canEnter = false;
            }

            Flip();
            _anim.SetBool("IsRun", true);
            
            if (moveRight)
            {
                //transform.localScale = new Vector3(-1f, 1f, 1f);
               
                GetComponent<Rigidbody2D>().velocity = new Vector2(moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            else
            {
                //transform.localScale = new Vector3(1f, 1f, 1f);
                GetComponent<Rigidbody2D>().velocity = new Vector2(-moveSpeed, GetComponent<Rigidbody2D>().velocity.y);
            }
            if (playered)
            {
                _anim.SetBool("IsAttacking", true);
            }
            else
            {
                _anim.SetBool("IsAttacking", false);
            }
        }
        
        if (health < 0)
        {
            health = 6;
            Die();
        }
    }
    public void Flip()
    {

        if (Player.transform.position.x > transform.position.x)
        {
            moveRight = true;
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        }
        else if (Player.transform.position.x < transform.position.x)
        {
            moveRight = false;
            transform.localScale = new Vector3(fx, fy, fz);
        }
    }
    public void Flip2()
    {

        if (wallCheck.position.x < transform.position.x)
        {
            moveRight = true;
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        }
        else if (wallCheck.position.x > transform.position.x)
        {
            moveRight = false;
            transform.localScale = new Vector3(fx, fy, fz);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            moveSpeed = 0f;
            if (Time.time >= dtime)
            {
                dtime = Time.time + .75f;
                SoundManager.sndMan.PlayBark();
                FindObjectOfType<PlaterStats>().TakeDamage(2);
            }
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            moveSpeed = 0f;
            if (Time.time >= dtime)
            {
                dtime = Time.time + .75f;
                SoundManager.sndMan.PlayBark();
                FindObjectOfType<PlaterStats>().TakeDamage(2);
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            moveSpeed = OrigMoveSpeed;
            canEnter = true;
        }
    }
}

