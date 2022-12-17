using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    Text scoreUI;
    EnemySpawner enemySpawner;
    private void Start()
    {
        enemySpawner = FindObjectOfType<EnemySpawner>();
        scoreUI = GetComponent<Text>();
       
    }

    private void Update()
    {
        scoreUI.text = "Score: " + enemySpawner.score;
    }
}
