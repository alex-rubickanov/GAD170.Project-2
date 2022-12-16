using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] float speed = 30f;
    [HideInInspector] Transform target;
    [SerializeField] GameObject enemy;
    [SerializeField] public EnemySpawner enemySpawner;

    

    private void Start()
    {
        target = GameObject.Find("Player").transform;
        
    }

    private void Update()
    {
        ChasePlayer();
        transform.LookAt(target);
    }

    void ChasePlayer() //this function makes enemies follow our player
    {
        Vector3 destination = (target.position - transform.position).normalized; //takes where enemy should go
        transform.position += destination * speed * Time.deltaTime; //moves enemy
    }

   /* void KillEnemy()
    {
        Destroy(enemy);
    } */

    
}
