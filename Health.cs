using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    private GameManager gameManager;
    private float delayTime = 6.5f;

    public GameObject healthSound;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        transform.localScale = transform.localScale * 0.135f;
    }

    // Update is called once per frame
    void Update()
    {
        DestroyHealthPowerUp();
    }

    // Destroy health powerup after a couple of seconds on screen
    void DestroyHealthPowerUp()
    {
        Destroy(this.gameObject, delayTime);
    }

    // If the health powerup collides with player, destroy the powerup and add a life to the player, as well as play a health power-up sound
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            healthSound = GameObject.Find("HealthAudio").GetComponent<AudioSource>().gameObject;
            healthSound.GetComponent<AudioSource>().Play();
            collision.GetComponent<PlayerController>().GainALife();
            Destroy(this.gameObject);
        }
    }
}
