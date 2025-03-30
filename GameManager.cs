using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyOnePrefab;
    public GameObject enemyTwoPrefab;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("CreateEnemyOne", 1, 2);
        InvokeRepeating("CreateEnemyTwo", 1, 3);
    
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // This function is called to create enemies of type 1
    void CreateEnemyOne()
    {
        Instantiate(enemyOnePrefab, new Vector3(Random.Range(-9f, 9f), 6.5f, 0), Quaternion.identity);
    }

    // This function is called to create enemies of type 2
    void CreateEnemyTwo()
    {
        Instantiate(enemyTwoPrefab, new Vector3(-9f, Random.Range(-4f, 4f), 0), Quaternion.identity);
    }
}

