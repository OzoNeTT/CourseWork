using UnityEngine;
using System.Collections;

public class GameOverMenu : MonoBehaviour {

	// Use this for initialization
    public static int levelnumber;
    public static GameOverMenu instance = null;
	void Start () {
	
	}
    void Awake()
    {
        if (instance == null)
            instance = this;
        else if (instance != this)
            Destroy(gameObject);

        DontDestroyOnLoad(gameObject);

    }
	// Update is called once per frame
	void Update () {
	
	}
    public void StartGame()
    {
        Application.LoadLevel(levelnumber);
    }
    public void ExitGame()
    {
        Application.Quit();
    }
}
