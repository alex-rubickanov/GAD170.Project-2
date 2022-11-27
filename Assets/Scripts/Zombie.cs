using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator animator;
    private void Start()
    {
       animator = this.gameObject.GetComponent<Animator>();
    }

    private void Update()
    {
        animator.SetBool("Walking", false);
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("Walking", true);
        }
        
    }
}
