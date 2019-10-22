using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TeleporterScript : MonoBehaviour
{
    private PlaterStats Player;
    string lname;
    // Start is called before the first frame update
    void Start()
    {
        Player = FindObjectOfType<PlaterStats>();
        GetComponent<Animator>().Play("Teleporter_Idle");
        lname = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        

        if (collision.tag == "Player")
        {
            FadeInOut.sceneEnd = true;
            GetComponent<Animator>().Play("Teleporter_Boom");
            Invoke("loadLevel", 1f);

        }
    }
    private void loadLevel()
    {
        
        if(lname == "Level0")
        {
            SceneManager.LoadScene("Level1");
        }
        else if(lname == "Level1")
        {
            SceneManager.LoadScene("Level2");
        }
        else if(lname == "Level2")
        {
            SceneManager.LoadScene("Level3");
        }
        else if (lname == "Level3")
        {
            SceneManager.LoadScene("Boss");
        }
    }
}
