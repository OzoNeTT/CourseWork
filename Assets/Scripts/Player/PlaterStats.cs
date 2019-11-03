using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlaterStats : MonoBehaviour
{
    // Use this for initialization
    public static int  health = 6;
    public static int lives = 5;
    public float flickerDuration = 0.1f;
    private float flickerTime = 0f;
    private SpriteRenderer spriteRenderer;
    public bool isImmune = false;
    public float immunityDuraction = 1f;
    private float immunityTime = 0f;
    public static  int collectedCoins = 0;
    public Text scoreUI;
    public Text LiveUI;
    public Slider healthUI;
   [SerializeField] Transform spawnPoint;

    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        scoreUI.text = "" + PlaterStats.collectedCoins.ToString();
        LiveUI.text = "" + PlaterStats.lives.ToString();
        healthUI.value = health;
        if (this.isImmune == true)
        {
            SpriteFlicker();
            immunityTime = immunityTime + Time.deltaTime;
            if (immunityTime >= immunityDuraction)
            {
                this.isImmune = false;
                this.spriteRenderer.enabled = true;
            }
        }
    }
    void SpriteFlicker()
    {
        if (this.flickerTime < this.flickerDuration)
        {
            this.flickerTime = this.flickerTime + Time.deltaTime;
        }
        else if (this.flickerTime >= this.flickerDuration)
        {
            spriteRenderer.enabled = !(spriteRenderer.enabled);
            this.flickerTime = 0;
        }
    }
    public void TakeDamage(int damage)
    {
        if (this.isImmune == false)
        {
            GetComponent<Animator>().Play("TakeDamage");
            PlaterStats.health = PlaterStats.health - damage;
            if (PlaterStats.health < 0f)
                PlaterStats.health = 0;
            if (PlaterStats.lives > 0f && PlaterStats.health == 0f)
            {
                // FindObjectOfType<LevelManger>().Respawnplayer();
                SoundManager.sndMan.PlayDeathSound();
                PlaterStats.health = 6;
                PlaterStats.lives--;
                transform.position = spawnPoint.position;
            }
            else if (PlaterStats.lives == 0 && PlaterStats.health == 0)
            {
                Debug.Log("GAMEOVER!!");
                PlaterStats.collectedCoins = 0;
                PlaterStats.lives = 5;
                PlaterStats.health = 6;
                GameOverMenu.levelnumber = Application.loadedLevel;
                Destroy(this.gameObject);
                Application.LoadLevel("GameOverScene");
            }
            PlayHitReaction();
        }


    }
    void PlayHitReaction()
    {
        this.isImmune = true;
        this.immunityTime = 0f;
    }
    public void CollectCoin(int coinValue)
    {
        PlaterStats.collectedCoins = PlaterStats.collectedCoins + coinValue;
    }
    public int get_health()
    {
        return health;
    }
}
