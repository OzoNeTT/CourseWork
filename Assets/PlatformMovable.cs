using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovable : MonoBehaviour
{
    public float speed;
    public Transform left;
    public Transform right;
    float MoveSpeed;
    void Start()
    {
        //speed = 7f;
        MoveSpeed = speed;
    }

    void Update()
    {
        transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
        if(transform.position.x > right.position.x)
        {
            MoveSpeed = -speed;
        } else if (transform.position.x < left.position.x)
        {
            MoveSpeed = speed;
        }
    }
}
