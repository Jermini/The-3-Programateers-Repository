using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // General Variables
    private float playerSpeed;
    private float horizontalInput;
    private float verticalInput;
    
    private float horizontalScreenLimit = 9.5f;
    private float barrierTop = -2.0f;
    private float barrierBottom = -3.0f;

    public GameObject bulletPrefab;

    void Start(){

        playerSpeed = 6f;

        // This moves the character to starting position
        transform.Translate(new Vector3(0, -3, 0));
    }

    void Movement()
    {
        // Read the Input from the Player
        horizontalInput = Input.GetAxis("Horizontal"); 
        verticalInput = Input.GetAxis("Vertical");

        // Move the player
        transform.Translate(new Vector3(horizontalInput, verticalInput, 0) * Time.deltaTime * playerSpeed);

        // Screen Limit
        if(transform.position.x > horizontalScreenLimit || transform.position.x <= -horizontalScreenLimit) 
        {
            transform.position = new Vector3(transform.position.x * -1, transform.position.y, 0);
        }

        // Barrier Limit - the player can only move between these barriers
        if(transform.position.y > barrierTop) 
        {
            transform.position = new Vector3(transform.position.x, barrierTop, 0);
        }
        else if(transform.position.y <= barrierBottom) 
        {
            transform.position = new Vector3(transform.position.x, barrierBottom, 0);
        }
    }

    void Update()
    {
        // This function is called every frame: 60 frames/second
        Movement();
        Shooting();
    }

    void Shooting()
    {
        // if the player presses the SPACE key, create * shoot a projectile
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity); 
        }
    }
}
