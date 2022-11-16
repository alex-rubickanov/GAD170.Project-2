using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour                          // This code is very difficult for newbies
{                                                               // This script allow our player rotate PlayerObject using mouse
    float mouseX;                                               // I spent a lot of time trying to get the same result but using only knowledge from my head
    float mouseY;                                               // So this script was STOLEN from Brackeys
                                                                // Shame on me that I didn't come to Mustafa on consultation and ask him to help me
    public float mouseSensitivity = 100f;                       // I'll do it next time 

    public Transform player;

    float xRotation;
    public Player playerScript;

    
    void Update()
    {
        if (playerScript.playerAlive == true) 
        {
            mouseX = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
            mouseY = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseX;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f); // We Limit how far player can rotate to avoid rollover

            transform.localRotation = Quaternion.Euler(xRotation, 0f, -0f);
            player.Rotate(Vector3.up * mouseY);
        }
    }
}