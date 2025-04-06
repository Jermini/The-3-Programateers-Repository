using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private GameManager gameManager;
    private float delayTime = 7.5f;
    public GameObject coinSound;
   
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.localScale = transform.localScale * 0.135f;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyCoinPowerUp();
    }

    // Destroy coin powerup after a couple of seconds on screen
    void DestroyCoinPowerUp()
    {
        Destroy(this.gameObject, delayTime);
    }

    // If the coin powerup collides with player, destroy the powerup and add 1 to score
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player") 
        {
            coinSound = GameObject.Find("CoinAudio").GetComponent<AudioSource>().gameObject;
            coinSound.GetComponent<AudioSource>().Play();
            gameManager.AddScore(1);
            Destroy(this.gameObject);
        }
    }
}
