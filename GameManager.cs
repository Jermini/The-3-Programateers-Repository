using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject healthPrefab;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI powerupText;
    public GameObject gameOverText;
    public GameObject restartText;
    public GameObject powerUpPrefab;
    public GameObject audioPlayer;
    public AudioClip powerupSound;
    public AudioClip powerdownSound;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public int score; 
    private bool gameOver; 
    public int cloudMove;

    // Start is called before the first frame update
    void Start()
    {
        gameOver = false;
        gameOverText.SetActive(false);
        restartText.SetActive(false);
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateHealthPowerUp", 1, 10.5f);
        score = 0;
        cloudMove = 1;
        AddScore(0);
        StartCoroutine(SpawnPowerUp());
        powerupText.text = "No PowerUps Active Yet!";
    }

    // Update is called once per frame
    void Update()
    {
        // If the game is over and player presses the R key, restart the scene/game
        if (gameOver)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }
    }

    // Spawn a powerup every few seconds and repeat
    IEnumerator SpawnPowerUp()
    {
        float spawnTime = Random.Range(8, 15);
        yield return new WaitForSeconds(spawnTime);
        CreatePowerUp();
        StartCoroutine(SpawnPowerUp()); 
    }

    // Change powerup text depending on powerup
    public void ManagePowerUpText(int powerupType)
    {
        switch (powerupType)
        {
            case 1:
                powerupText.text = "PowerUp: Speed Boost!";
                break;
            case 2:
                powerupText.text = "PowerUp: Double Damage!";
                break;
            case 3:
                powerupText.text = "PowerUp: Triple Damage!";
                break;
            case 4:
                powerupText.text = "PowerUp: Shield!";
                break;
            default:
                powerupText.text = "No PowerUps Active Yet!";
                break;
        }
    }


    public void PlaySound(int whichSound)
    {
        switch (whichSound)
        {
            case 1:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerupSound);
                break;
            case 2:
                audioPlayer.GetComponent<AudioSource>().PlayOneShot(powerdownSound);
                break;
        }
    }

    // Create a new enemy at a random position
    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.85f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    // Create a power up at a random position
    void CreatePowerUp()
    {
        Instantiate(powerUpPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.75f, Random.Range(-verticalScreenSize, verticalScreenSize) * 0.75f, 0), Quaternion.identity);
    }

     // Create a health power-up at a random position
    void CreateHealthPowerUp()
    {
        Instantiate(healthPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.75f, Random.Range(-verticalScreenSize, verticalScreenSize) * 0.75f, 0), Quaternion.identity);
    }


    // Create a new cloud/sky at a random position
    void CreateSky()
    {
        for(int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }

    // Add score to the current score
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        scoreText.text = "Score: " + score;
    }

    // Change the lives text
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    // Show the Game Over Screen
    public void GameOverScreen()
    {
        gameOverText.SetActive(true);
        restartText.SetActive(true);
        gameOver = true;
        CancelInvoke();
        cloudMove = 0;
    }
}
