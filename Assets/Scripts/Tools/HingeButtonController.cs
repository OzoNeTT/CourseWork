using UnityEngine;
using System.Collections;

public class HingeButtonController : MonoBehaviour {

    // Use this for initialization
    public GameObject hinge;
    float x = -5f;
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "PlayerBullet")
        {
            SoundManager.sndMan.PlayDoorOpening();
            hinge.GetComponent<Rigidbody2D>().isKinematic = false;
            transform.Translate(x, transform.position.y, transform.position.z);
        }
    }
}
