using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class keyToGate : MonoBehaviour
{
    public GameObject gate;
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
            SoundManager.sndMan.PlayKeyWav();
            Destroy(this.gameObject);
            Destroy(gate);
        }
    }
}
