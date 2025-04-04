using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Variables
    private int lives;
    private float speed;
    private GameManager gameManager;
    private float horizontalInput;
    private float verticalInput;
    public GameObject bulletPrefab;
    public GameObject explosionPrefab;

    // Start is called before the first frame update
    void Start()
    {
        // Makes gameManager a reference to the GameManager object in the scene
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        
        // Sets speed & lives for the player
        lives = 3;
        speed = 5f;
        gameManager.ChangeLivesText(lives);
    }

    // Update is called once per frame
    void Update()
    {
        Movement();
        Shooting();
    }

    // Player Movement
    void Movement()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * speed * Time.deltaTime);

        // Screen Limit for the player, references gameManager for screen size
        float horizontalScreenSize = gameManager.horizontalScreenSize;
        float verticalScreenSize = gameManager.verticalScreenSize;

        if (transform.position.x <= -horizontalScreenSize || transform.position.x > horizontalScreenSize)
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        if (transform.position.y <= -verticalScreenSize || transform.position.y > verticalScreenSize)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y * -1, 0);
        }
    }   

    // Player Shooting
    void Shooting()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
        }
    } 

    // Makes Player lose a life if they collide with an enemy
    public void LoseALife()
    {
        lives--;
        gameManager.ChangeLivesText(lives);

        if (lives == 0)
        {
            Destroy(this.gameObject);
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);
        }
    }

    // Makes Player gain a life if they collide with a health power-up WITHOUT exceeding 3 lives
    public void GainALife()
    {
        lives++;
        gameManager.ChangeLivesText(lives);

        if (lives >= 3)
        {
            lives = 3;
            gameManager.ChangeLivesText(lives);
        }
    }
}
