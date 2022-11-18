using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;

public class Gun : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 10f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawner;
    [SerializeField] Player player;

   
    void Update()
    {
        if (player.playerAlive == true)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0)) //if player press left mouse button we instantiate a bullet prefab and destroy it in 6 second to avoid millions of them
            {
                GameObject bullet = Instantiate(bulletPrefab, bulletSpawner.position, bulletSpawner.rotation);
                bullet.GetComponent<Rigidbody>().velocity = bulletSpawner.forward * bulletSpeed * Time.deltaTime;
                Destroy(bullet, 6f);
            }
        }
        
        
    }
    
}
