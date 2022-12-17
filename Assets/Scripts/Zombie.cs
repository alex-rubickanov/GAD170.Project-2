using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    private Animator animator;
    private GameObject player;
    EnemySpawner enemySpawner;
    bool onetime = true;
    bool onetime2 = true;
    Player playerScript;

    [Header("STATS")]
    [SerializeField] float attackRange;
    [SerializeField] float speed = 1;
    [SerializeField] float attackSpeed = 1;
    [SerializeField] float hitsToKill = 2;

    [Header("AUDIO SFX")]
    [SerializeField] AudioSource audioSourcce;
    //[SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip dyingSound;

    private Vector3 distanceToPlayer;
    private bool isAlive = true;
    private Collider col;

    //FIX ROTATION
    float fixX;
    float fixZ;
    private void Start()
    {
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.gameObject.GetComponent<Collider>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        playerScript = FindObjectOfType<Player>();

    }

    private void Update()
    {
        //Actions
        if (isAlive)
        {
            this.transform.LookAt(player.transform);
            FixPosition();
            if (PlayerClose())
            {
                if (onetime2)                               // i hope there will be better option that make a ONETIME bool variable but i got so many problems as memory leak without it
                {
                    Attack();
                    onetime2 = false;
                }

            }
            else
            {
                Chasing();
                onetime2 = true;
            }
        }
        else
        {
            if (onetime)
            {
                Death();
                enemySpawner.OnEnemyDeath(3);
                onetime = false;
            }

        }

    }


    void Death()
    {
        animator.speed = 1;
        animator.SetBool("Dying", true);
        col.isTrigger = true;                           //play animation, sound, make trigger collider to be able go through and destroy in 10 seconds
        audioSourcce.PlayOneShot(dyingSound);
        Destroy(this.gameObject, 10f);
    }

    void Chasing()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walking",true);
        animator.speed = speed;
       // audioSourcce.PlayOneShot(walkSound);
    }

    void Attack()
    {
        animator.speed = attackSpeed;       // we play animation, play attack sound, decrease player's health
        animator.SetBool("Attack", true);
        audioSourcce.PlayOneShot(attackSound);
        playerScript.health -= 15;
        Debug.Log(playerScript.health);
    }
    
    bool PlayerClose() //check player
    {
        distanceToPlayer = this.transform.position - player.transform.position;
        if (distanceToPlayer.magnitude < attackRange)
        {
            return true;
        }
        else return false;
    }

    void FixPosition() //fix model's poistion. Try to comment this function in update to see what could be without it
    {
        this.transform.position = new Vector3(this.transform.position.x, 0, this.transform.position.z);
        
        //FIX ROTATION
        fixX = this.transform.rotation.eulerAngles.x;
        fixX = Mathf.Clamp(fixX, -10f, 10f);
        fixZ = this.transform.rotation.eulerAngles.z;
        fixZ = Mathf.Clamp(fixZ, -10f, 10f);
        transform.rotation = Quaternion.Euler(fixX, this.transform.rotation.eulerAngles.y, fixZ);
    }

    private void OnTriggerEnter(Collider other) // if object collides with bullet HITSTOKILL times it dies
    {
        if (other.tag == "Bullet")
        {
            if (hitsToKill <= 0)
            {
                isAlive = false;
            } else
            {
                hitsToKill -= 1;
            }
        }
    }
}
