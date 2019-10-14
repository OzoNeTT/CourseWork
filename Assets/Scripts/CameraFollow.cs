using UnityEngine;
using System.Collections;

public class CameraFollow : MonoBehaviour {

	// Use this for initialization
    //private Vector2 velocity;
    //public float smoothTimeY;
    //public float smoothTimeX;
    //public GameObject player;
    // Use this for initialization
    public Transform target;
    public float smothing;
    private Vector3 offset;

    //public bool bounds;
    //public Vector3 minCamerapos;
    //public Vector3 maxCamerapos;
    void Start()
    {
        offset = transform.position - target.position;
        //player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {

        Vector3 targetcampos = target.position + offset;
        transform.position = Vector3.Lerp(transform.position, targetcampos, smothing * Time.deltaTime);
    }
    //void FixedUpdate()
    //{
    //    float posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, smoothTimeX);
    //    float posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, smoothTimeY);
    //    transform.position = new Vector3(posX, posY, transform.position.z);
    //    if (bounds)
    //    {
    //        transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamerapos.x, maxCamerapos.x), Mathf.Clamp(transform.position.y, minCamerapos.y, maxCamerapos.y), Mathf.Clamp(transform.position.z, minCamerapos.z, maxCamerapos.z));
    //    }
    //}
}
