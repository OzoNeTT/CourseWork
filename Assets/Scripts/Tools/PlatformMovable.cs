using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformMovable : MonoBehaviour
{
    public float speed;
    public Transform left;
    public Transform right;
    public bool isHorizontal;
    float MoveSpeed;
    void Start()
    {
        //speed = 7f;
        MoveSpeed = speed;
       
    }

    void Update()
    {
        if (isHorizontal)
        {
            transform.Translate(MoveSpeed * Time.deltaTime, 0, 0);
            if (transform.position.x > right.position.x)
            {
                MoveSpeed = -speed;

            }
            else if (transform.position.x < left.position.x)
            {
                MoveSpeed = speed;

            }
        }
        else if (!isHorizontal)
        {
            transform.Translate(0, MoveSpeed * Time.deltaTime, 0);
            if (transform.position.y > right.position.y)
            {
                MoveSpeed = -speed;

            }
            else if (transform.position.y < left.position.y)
            {
                MoveSpeed = speed;

            }
        }
    }

    public float get_Global_speed()
    {
        return MoveSpeed;
    }
}
