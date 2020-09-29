﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileLauncher : MonoBehaviour
{
    public GameObject myPlayer;
    public GameObject bulletPrefab;
    public float bulletSpeed = 10.0f;
    public float bulletDestroyTime = 3.0f;
    
    void Update()
    {
        FollowPlayer(gameObject);
        FollowMouse(gameObject);
        if (Input.GetMouseButtonDown(0))
        {
            Shoot();
        }
    }

    //let arm follow the mouse and flip
    void FollowMouse(GameObject input)
    {
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();
        float z = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        input.transform.rotation = Quaternion.Euler(0f, 0f, z); //arm follows mouse if face to the right
        myPlayer.transform.rotation = Quaternion.Euler(0, 0, 0); //player faces right
        //Debug.Log(z);
        if(z < -90 || z > 90)
        {
            if (myPlayer.transform.eulerAngles.y == 0 || myPlayer.transform.eulerAngles.y == 180)
            {
                input.transform.rotation = Quaternion.Euler(180, 0, -z); //arm follows mouse if face to the left
                myPlayer.transform.rotation = Quaternion.Euler(0, 180, 0); //player faces left
            }
        }
    }

    //let the arm and gun follow the player
    void FollowPlayer(GameObject input)
    {
        input.transform.position = myPlayer.transform.position + new Vector3(0f, -0.2f, -1f); //minus arm's coordinate by player's position
    }

    //fire the bullets
    void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab) as GameObject;
        FollowPlayer(bullet);
        FollowMouse(bullet);

        Vector3 difference = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z)) - transform.position;
        float distance = difference.magnitude;
        Vector2 direction = difference / distance;
        direction.Normalize();
        
        bullet.transform.position += new Vector3(direction.x , direction.y, 0f);
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        Destroy(bullet, bulletDestroyTime);
    }
}
