using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            PlaterStats.health += 2;
            SoundManager.sndMan.PlayDrinkHP();
            Destroy(this.gameObject);
        }
    }
}
