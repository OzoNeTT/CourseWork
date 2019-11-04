using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class EnemyGhostGFX : MonoBehaviour
{
    Path path;

    public Transform target;
    public float speed = 400f;
    public float nextWaypointDistance = 3f;
    int currentWaypoint = 0;
    bool reachedEndOfPath = false;
    public Animator _anim;
    Seeker seeker;
    Rigidbody2D rb;
    float dtime;
    float deltatime;
    private AudioSource snd;
    private AudioClip GhostSound;
    bool isAlive = true;
    int damage = 2;
    bool soundPaused = false;
    public float PlayerRadius = 50;
    void Start()
    {
        seeker = GetComponent<Seeker>();
        rb = GetComponent<Rigidbody2D>();
        //_anim = GetComponent<Animator>();
        InvokeRepeating("UpdatePath", 0f, .5f);
        _anim.SetBool("isPlayer", false);
        seeker.StartPath(rb.position, target.position, OnPathComplete);
        snd = GetComponent<AudioSource>();
        GhostSound = Resources.Load<AudioClip>("ghost");
        dtime = Time.time;
        deltatime = Time.time;

    }
    void UpdatePath()
    {
        if (seeker.IsDone())
        {
            seeker.StartPath(rb.position, target.position, OnPathComplete);
        }
    }
    void OnPathComplete(Path p)
    {
        if (!p.error)
        {
            path = p;
            currentWaypoint = 0;
        }
    }
    void FixedUpdate()
    {
        if (Vector2.Distance(transform.position, target.position) <= PlayerRadius && isAlive)
        {

            if (Time.time >= dtime)
            {
                dtime = Time.time + 23f;
                snd.PlayOneShot(GhostSound);
            }
            if (soundPaused)
            {
                snd.UnPause();
            }
            _anim.SetBool("isPlayer", true);
            if (path == null)
                return;

            if (currentWaypoint >= path.vectorPath.Count)
            {
                reachedEndOfPath = true;
                return;
            }
            else
            {
                reachedEndOfPath = false;
            }

            Vector2 direction = ((Vector2)path.vectorPath[currentWaypoint] - rb.position).normalized;
            Vector2 force = direction * speed * Time.deltaTime;

            rb.AddForce(force);

            float distance = Vector2.Distance(rb.position, path.vectorPath[currentWaypoint]);

            if (distance < nextWaypointDistance)
            {
                currentWaypoint++;
            }

            if (rb.velocity.x >= 0.01f)
            {
                transform.localScale = new Vector3(-1f, 1f, 1f);
            }
            else if (rb.velocity.x <= -0.01f)
            {
                transform.localScale = new Vector3(1f, 1f, 1f);
            }
        }
        else
        {
            snd.Pause();
            soundPaused = true;
            _anim.SetBool("isPlayer", false);
        }
    }
    public void Dies()
    {
        isAlive = false;
        rb.velocity = new Vector3(0, 0, 0);

        StartCoroutine("waitAttack");

    }
    private IEnumerator waitAttack()
    {
        _anim.SetBool("isPlayer", false);
        _anim.SetBool("Dies", true);
        yield return new WaitForSeconds(1f);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Player")
        {
            if (Time.time >= deltatime && isAlive)
            {
                deltatime = Time.time + 1f;
                FindObjectOfType<PlaterStats>().TakeDamage(damage);
                collision.transform.GetComponent<AudioSource>().Play();
                FindObjectOfType<PlayerControl>().KnockBackCount = 0.1f;
                if (collision.transform.position.x < transform.position.x)
                    FindObjectOfType<PlayerControl>().KnockFromRight = true;
                else
                    FindObjectOfType<PlayerControl>().KnockFromRight = false;
            }
        }
    }


}
