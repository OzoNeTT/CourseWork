using UnityEngine;
using System.Collections;

public class SpikesController : MonoBehaviour {

    public int damage = 6;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            //FindObjectOfType<LevelManger>().Respawnplayer();
            //FindObjectOfType<PlaterStats>().TakeDamage(damage);
            other.GetComponent<PlayerControl>().KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
               other.GetComponent<PlayerControl>().KnockFromRight = true;
        
        }
    }
}
