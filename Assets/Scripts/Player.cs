using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 10f;
    CharacterController contoller;

    [SerializeField] GameObject bulletSpawner;
    [SerializeField] GameObject bullet;

    public bool playerAlive = true;
    private void Start()
    {
        contoller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //deletes cursor
    }
    private void Update()
    { 
        if(playerAlive == true) //let player move while he is alive, when we put FALSE in variable playerAlive we "freeze" him
        {
            Movement();
        }

        if (Input.GetKeyDown(KeyCode.Space)) //lets player restart the game
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        
    }

    void Movement() //tried to use Charachter contoller to move player. Much better then using transform!
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        contoller.Move(move * speed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other) //if player collides with enemy we put FALSE in boolean variable playerAlive
    {
        if (other.tag == "Enemy") //all enemies prefabs have tag ENEMY
        {
            Debug.Log("Player is dead!");
            playerAlive = false;
        }
        
    }
}
