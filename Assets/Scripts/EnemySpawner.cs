using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] GameObject[] EnemiesPrefabs;
    [SerializeField] int maxEnemy = 10;
    private int enemyCount = 0;
    public int score;

    public delegate void OnEnemyDeathEvent(int integer);
    public OnEnemyDeathEvent OnEnemyDeath;

    void Start()
    {
        score = 0;
        print("??");
        LoopingThroughItems();
        OnEnemyDeath += RandomInstantiate;
        OnEnemyDeath += Score;

    }

    void Update()
    {
        if (enemyCount < maxEnemy) // we have 10 enemies we dont instantiate more enemies
        {
            RandomInstantiate(0);
            enemyCount += 1;
        }
        
    }

    public void RandomInstantiate(int integer) //this function spawn random enemy prefab from array on random position on the map
    {
        float xPos = Random.Range(-49, 49);
        float zPos = Random.Range(-50, 50);
        Instantiate(EnemiesPrefabs[Random.Range(0, 2)], new Vector3(xPos, 1, zPos), Quaternion.identity);
    }

    private void LoopingThroughItems()                                              // This function takes each prefab in our array and send them in function ItJustWorks to make a string variable to print
    {
        for (int i = 0; i < 2; i++)
        {
            Debug.Log(ItJustWorks(EnemiesPrefabs[i],i));
        }                                                                                   // I'm ashamed for these 2 functions. 
    }                                                                                       // They were made only to demonstrate my ability to making looping through items in collections  
                                                                                            // and making Return functions
    private string ItJustWorks(GameObject obj, int index)                             // Function ItJustWorks returns STRING and need 2 parameters.         
    {                                                                                 
        string toPrint = "Enemy " + (index+1) + " is " + obj;
        return toPrint;
    }

    public void Score(int points)
    {
        score += points;
        Debug.Log("Score: " + score);
    }
}
