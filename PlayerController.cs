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
    public GameObject thrusterPrefab;
    public GameObject shieldPrefab;
    private int weaponType;
    private bool hasShield;

    // Start is called before the first frame update
    void Start()
    {
        weaponType = 1;

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
            switch (weaponType)
            {
                case 1:
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    break;
                case 2:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.7f, 1, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.7f, 1, 0), Quaternion.identity);
                    break;
                case 3:
                    Instantiate(bulletPrefab, transform.position + new Vector3(-0.7f, 1, 0), Quaternion.Euler(0, 0, 45));
                    Instantiate(bulletPrefab, transform.position + new Vector3(0, 1, 0), Quaternion.identity);
                    Instantiate(bulletPrefab, transform.position + new Vector3(0.7f, 1, 0), Quaternion.Euler(0, 0, -45));
                    break;
            }
        }
    } 

    // Makes Player lose a life if they collide with an enemy
    public void LoseALife()
    {
        if (hasShield == false)
        {
            lives--;
            gameManager.ChangeLivesText(lives);
        }
        else if (hasShield == true)
        {
            shieldPrefab.SetActive(false);
            gameManager.ManagePowerUpText(0);
            gameManager.PlaySound(2);
            hasShield = false;
        }

        if (lives == 0)
        {
            gameManager.GameOverScreen();
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

    IEnumerator SpeedPowerDown()
    {
        yield return new WaitForSeconds(5f);
        speed = 5f;
        thrusterPrefab.SetActive(false);
        gameManager.PlaySound(2);

        if (!hasShield)
        {
            gameManager.ManagePowerUpText(0);
        }
        else if (hasShield)
        {
            gameManager.ManagePowerUpText(4);
        }
    }

    IEnumerator WeaponPowerDown()
    {
        yield return new WaitForSeconds(5f);
        weaponType = 1;
        gameManager.PlaySound(2);

        if (!hasShield)
        {
            gameManager.ManagePowerUpText(0);
        }
        else if (hasShield)
        {
            gameManager.ManagePowerUpText(4);
        }
    }

    // Makes the player gain a powerup if they collide with one
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Powerup")
        {
            int whichPowerup = Random.Range(1, 5);
            Destroy(collision.gameObject);

            switch (whichPowerup)
            {
                // Speed
                case 1:
                    gameManager.ManagePowerUpText(1);
                    gameManager.PlaySound(1);
                    speed = 10f;
                    StartCoroutine(SpeedPowerDown());
                    thrusterPrefab.SetActive(true);
                    break; 
                // Double Shot Weapon
                case 2:
                    gameManager.ManagePowerUpText(2);
                    gameManager.PlaySound(1);
                    weaponType = 2;
                    StartCoroutine(WeaponPowerDown());
                    break;
                // Triple Shot Weapon
                case 3:
                    gameManager.ManagePowerUpText(3);
                    gameManager.PlaySound(1);
                    weaponType = 3;
                    StartCoroutine(WeaponPowerDown());
                    break;
                // Shield
                case 4:
                    gameManager.ManagePowerUpText(4);
                    shieldPrefab.SetActive(true);
                    hasShield = true;
                    gameManager.PlaySound(1);
                    break;
            }
        }
    }
}
