using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Giant : MonoBehaviour
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
    [SerializeField] float hitsToKill = 4;


    private Vector3 distanceToPlayer;
    private bool isAlive = true;
    private Collider col;

    [Header("AUDIO SFX")]
    [SerializeField] AudioSource audioSourcce;
    //[SerializeField] AudioClip walkSound;
    [SerializeField] AudioClip attackSound;
    [SerializeField] AudioClip dyingSound;

    //FIX ROTATION
    private float fixX;
    private float fixZ;
    private void Start()
    { 
        animator = this.gameObject.GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        col = this.gameObject.GetComponent<Collider>();
        enemySpawner = FindObjectOfType<EnemySpawner>();
        playerScript = FindObjectOfType<Player>();                              //EVERYTHING THE SAME AS IN ZOMBIE SCRIPT
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
                if (onetime2)
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
        col.isTrigger = true;
        audioSourcce.PlayOneShot(dyingSound);
        Destroy(this.gameObject, 10f);
    }

    void Chasing()
    {
        animator.SetBool("Attack", false);
        animator.SetBool("Walking", true);
        animator.speed = speed;
        //audioSourcce.PlayOneShot(walkSound);
    }

    void Attack()
    {
        audioSourcce.PlayOneShot(attackSound);
        animator.speed = attackSpeed;
        animator.SetBool("Attack", true);
        playerScript.health -= 20;
        Debug.Log(playerScript.health);
    }

    bool PlayerClose()
    {
        distanceToPlayer = this.transform.position - player.transform.position;
        if (distanceToPlayer.magnitude < attackRange)
        {
            return true;
        }
        else return false;
    }

    void FixPosition()
    {
        this.transform.position = new Vector3(this.transform.position.x, 0.3f, this.transform.position.z);

        //FIX ROTATION
        fixX = this.transform.rotation.eulerAngles.x;
        fixX = Mathf.Clamp(fixX, -10f, 10f);
        fixZ = this.transform.rotation.eulerAngles.z;
        fixZ = Mathf.Clamp(fixZ, -10f, 10f);
        transform.rotation = Quaternion.Euler(fixX, this.transform.rotation.eulerAngles.y, fixZ);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Bullet")
        {
            if (hitsToKill <= 0)
            {
                isAlive = false;
            }
            else
            {
                hitsToKill -= 1;
            }
        }
    }
}
