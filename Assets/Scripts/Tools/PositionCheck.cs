using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionCheck : MonoBehaviour
{
    bool IsPlaced;

    void Start()
    {
        IsPlaced = false;
    }
    void Update()
    {
        
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "Enemy2")
        {
            IsPlaced = true;
        }
        else
        {
            IsPlaced = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Enemy2")
        {
            IsPlaced = false;
        }
    }
    
    public bool GetPosition()
    {
        return IsPlaced;
    }
}
