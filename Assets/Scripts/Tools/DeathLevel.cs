using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathLevel : MonoBehaviour
{
    [SerializeField] Transform spawnPoint;
    private PlayerControl Player;
    public AudioSource DeathSound;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Player"))
        {
            DeathSound.pitch = Random.Range(0.9f, 1.1f);
            DeathSound.Play();
            FindObjectOfType<PlaterStats>().TakeDamage(6);
            collision.transform.position = spawnPoint.position;
        }

        if (collision.transform.CompareTag("Enemy"))
        {
            Destroy(collision.gameObject);
        }
    }
}
