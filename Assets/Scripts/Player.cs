using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [Header("STATS")]
    public int health = 100;

    [Header("Movement")] 
    [SerializeField] float speed = 10f;
    CharacterController contoller;

    [Header("Bullets")]
    [SerializeField] GameObject bulletSpawner;
    [SerializeField] GameObject bullet;

    [Header("Death Screen")]
    [SerializeField] GameObject deathText;
    [SerializeField] GameObject restartText;
    [HideInInspector] public bool playerAlive = true;
    private void Start()
    {
        contoller = GetComponent<CharacterController>();
        Cursor.lockState = CursorLockMode.Locked; //deletes cursor
    }
    private void Update()
    { 
        if (health > 0) //let player move while he is alive, when we put FALSE in variable playerAlive we "freeze" him
        {
            Movement();
        } else
        {
            Debug.Log(health);
            Debug.Log("Played is dead!");
            DeathScreen();
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

    void DeathScreen()
    {
        restartText.SetActive(true);
        deathText.SetActive(true);
    }
}
