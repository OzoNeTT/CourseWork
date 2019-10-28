using UnityEngine;
using System.Collections;

public class BossBulletController : MonoBehaviour {

	// Use this for initialization
    private PlayerControl Player;
    public float MoveSpeed = 50;
    public int damage = 5;
    public Vector3 oldplayerpos;
    private Rigidbody2D rb;
	void Start () {
        Player = FindObjectOfType<PlayerControl>();
        //oldplayerpos = Player.transform.position;
        oldplayerpos.x = Player.transform.position.x;
        oldplayerpos.z = Player.transform.position.z;
        rb = GetComponent<Rigidbody2D>();
        //Тут костыль чтоб к центру тела летело, а не к ногам
        oldplayerpos.y = Player.transform.position.y + 4;
        //
        
	}

    // Update is called once per frame
    void Update () {
        this.transform.position = Vector3.MoveTowards(this.transform.position, oldplayerpos, MoveSpeed * Time.deltaTime);
        if (this.transform.position == oldplayerpos)
            Destroy(this.gameObject);
        //this.transform.position = 

        //TODO : ПОЛЕТ НЕ ДО ЦЕЛИ А ПОД УГЛОМ ДО ПРЕПЯТСТВИЯ 

        // ХЗ ЧЕ ЭТО, НО НА САЙТЕ ЭТО ВРАЩЕНИЕ
        //this.transform.rotation=Quaternion.RotateTowards(this.transform.rotation,Quaternion.FromToRotation(Vector3.forward,Vector3.right),60*Time.deltaTime);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {

            Destroy(this.gameObject);
            FindObjectOfType<PlaterStats>().TakeDamage(damage);
            Player.KnockBackCount = 0.2f;
            if (other.transform.position.x < this.transform.position.x)
                Player.KnockFromRight = true;
        }
        if (other.tag == "Borders")
        {
            Destroy(this.gameObject);
        }
    }
}
