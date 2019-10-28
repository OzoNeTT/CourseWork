﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossScript : MonoBehaviour
{
    //points for moving
    public Transform[] points;
    float Speed;
    private PlayerControl Player;
    public Transform firepoint;
    public GameObject Bullet;
    public GameObject Enemies;
    public GameObject Enemies2;
    float fx, fy, fz;
    public int health;
    bool enter = true;
    public Slider healthUI;

    void Start()
    {
        GetComponent<Animator>().Play("Boss_Appear");
        //InvokeRepeating("SpawnEnemies", 1f, 8);
        Player = FindObjectOfType<PlayerControl>();
        health = 100;
       
        StartCoroutine("boss");
        fx = transform.localScale.x;
        fy = transform.localScale.y;
        fz = transform.localScale.z;
        Speed = 1f;
    }
   // public void takedamage(float damage)
   // {
   //     health -= damage;
   //     if(health < 50)
   //     {
   //         Speed = 3f;
   //     }
   //     if (health < 0)
   //     {
   //         GetComponent<Animator>().Play("Boss_Dies");
   //         Die();
   //     }
   //         
   // }
    public void SpawnEnemies()
    {
        SoundManager.sndMan.PlayScreamBoss();
        GetComponent<Animator>().Play("Boss_Skill");

        if (health <= 50)
        {
            points[0].GetComponent<Animator>().Play("PointSpawn");
            points[1].GetComponent<Animator>().Play("PointSpawn");
           
           
            Instantiate(Enemies2, points[0].position, points[0].rotation);
            Instantiate(Enemies2, points[1].position, points[1].rotation);

            if (points[3].GetComponent<PositionCheck>().GetPosition() == false)
            {
                points[3].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies, points[3].position, points[3].rotation);
            }
            if (points[4].GetComponent<PositionCheck>().GetPosition() == false)
            {
                points[4].GetComponent<Animator>().Play("PointSpawn");
                Instantiate(Enemies, points[4].position, points[4].rotation);
            }
        }
        else
        {
            points[0].GetComponent<Animator>().Play("PointSpawn");
            points[1].GetComponent<Animator>().Play("PointSpawn");
            Instantiate(Enemies2, points[0].position, points[0].rotation);
            Instantiate(Enemies2, points[1].position, points[1].rotation);
        }
    }
    
    void Die()
    {
        Destroy(this.gameObject, 1f);
    }
    void Update()
    {
        //healthUI.value = health;
        healthUI.value = health;
        if (GameObject.Find("Player 1") != null)
        {
            Flip();
        }
        if (health < 50)
        {
            Speed = 3f;
            //if(enter == true)
            //{
            //    //CancelInvoke();
            //    //InvokeRepeating("SpawnEnemies", 5f, 18);
            //    enter = false;
            //}
            
            //if (GameObject.Find("BossBullet") != null)
            if (FindObjectOfType<BossBulletController>() != null) 
            {
                FindObjectOfType<BossBulletController>().MoveSpeed = 40;
            }
        }
        if (health < 0)
        {
            GetComponent<Animator>().Play("Boss_Dies");
            health = 100;
            Die();
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {


            FindObjectOfType<PlaterStats>().TakeDamage(2);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
        }
        if (other.tag == "PlayerBullet")
        {
            Destroy(other.gameObject);
            health -= 4;
            
        }

    }

    IEnumerator boss()
    {
        //First
        while (true)
        {

            while (this.transform.position.x != points[0].position.x)
            {

                this.transform.position = Vector2.MoveTowards(this.transform.position, new Vector2(points[0].position.x, this.transform.position.y), Speed);

                yield return null;
            }
            transform.localScale = new Vector3(-1 * fx, fy, fz);
            int i = 0;
            yield return new WaitForSeconds(1);
            while (i < 10)
            {
                
                Instantiate(Bullet, firepoint.position, firepoint.rotation);
                i++;
                yield return new WaitForSeconds(.5f);
            }
            //second
            while (this.transform.position != points[2].position)
            {

                this.transform.position = Vector2.MoveTowards(this.transform.position, points[2].position, Speed);
                
                yield return null;
            }
            i = 0;

            
            SpawnEnemies();

            yield return new WaitForSeconds(1);
            while (i < 10)
            {
                
                Instantiate(Bullet, firepoint.position, firepoint.rotation);
                i++;
                yield return new WaitForSeconds(.5f);
            }
            //third
            while (this.transform.position.x != points[1].position.x)
            {

                this.transform.position = Vector2.MoveTowards(this.transform.position, points[1].position, Speed);

                yield return null;
            }
            transform.localScale = new Vector3(fx, fy, fz);
            i = 0;
            yield return new WaitForSeconds(1);
            while (i < 10)
            {
               
                Instantiate(Bullet, firepoint.position, firepoint.rotation);
                i++;
                yield return new WaitForSeconds(.5f);
            }
            //4


        }
    }
    public void Flip()
    {

        if (Player.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-1 * fx, fy, fz);
        else if (Player.transform.position.x < transform.position.x)
            transform.localScale = new Vector3(fx, fy, fz);
    }

}
