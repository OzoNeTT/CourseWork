﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaterStats : MonoBehaviour
{
    // Use this for initialization
   // public int health = 6;
   // public int lives = 3;
    public float flickerDuration = 0.1f;
    private float flickerTime = 0f;
    private SpriteRenderer spriteRenderer;
    public bool isImmune = false;
    public float immunityDuraction = 1.5f;
    private float immunityTime = 0f;
   // public int collectedCoins = 0;
    //public Text scoreUI;
    //public Text LiveUI;
    //public Slider healthUI;
    // Use this for initialization
    void Start()
    {
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
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
}
