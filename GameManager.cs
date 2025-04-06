using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyOnePrefab;
    public GameObject cloudPrefab;
    public GameObject coinPrefab;
    public TextMeshProUGUI livesText;
    public TextMeshProUGUI scoreText;
    public float horizontalScreenSize;
    public float verticalScreenSize;
    public int score;

    // Start is called before the first frame update
    void Start()
    {
        horizontalScreenSize = 10f;
        verticalScreenSize = 6.5f;
        Instantiate(playerPrefab, transform.position, Quaternion.identity);
        CreateSky();
        InvokeRepeating("CreateEnemy", 1, 3);
        InvokeRepeating("CreateCoinPowerUp", 1, 10.5f);
        score = 0;
        ChangeScoreText(score);
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Create a new enemy at a random position
    void CreateEnemy()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.9f, verticalScreenSize, 0), Quaternion.Euler(180, 0, 0));
    }

    // Create a new cloud/sky at a random position
    void CreateSky()
    {
        for (int i = 0; i < 30; i++)
        {
            Instantiate(cloudPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize), Random.Range(-verticalScreenSize, verticalScreenSize), 0), Quaternion.identity);
        }
    }

    // This function is called to add score and update the score text
    public void AddScore(int earnedScore)
    {
        score = score + earnedScore;
        ChangeScoreText(score);
    }

    // This function is called to update the lives text
    public void ChangeLivesText(int currentLives)
    {
        livesText.text = "Lives: " + currentLives;
    }

    // This function is called to update the score text
    public void ChangeScoreText(int currentScore)
    {
        scoreText.text = "Score: " + currentScore;
    }

    // Create a coin powerup at a random position
    void CreateCoinPowerUp()
    {
        Instantiate(coinPrefab, new Vector3(Random.Range(-horizontalScreenSize, horizontalScreenSize) * 0.65f, Random.Range(-verticalScreenSize, verticalScreenSize) * 0.65f, 0), Quaternion.identity);
    }
}