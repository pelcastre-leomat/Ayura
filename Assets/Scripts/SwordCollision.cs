//#########################################
//#   Game: Ayura                         #
//#   Author: Leonardo Matias Pelcastre   #
//#   Email: lp222nf@student.lnu.se       #
//#   ID: lp222nf                         #
//#   Year: 2019                          #
//#########################################


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    //Rigidbody component of sword
    private Rigidbody2D sword;

    //Multiple audio effects
    public AudioSource cutApple,swordFall;

    void Start()
    {
        //Get the sword's Rigidbody2D component
        sword = GetComponent<Rigidbody2D>();  
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //Initialize item variable from collision
        GameObject item = col.gameObject;

        //If item is apple or Birb play sound effect and update isHit variable in AppleFalling and BirbMovement classes respectively
        //if item is either left or right wall, play a sound effect
        if(item.tag == "Apple")
        {
            cutApple.Play();
        }else if(item.tag == "LeftWall" || item.tag == "RightWall")
        {
            swordFall.Play();
        }

        //Reflects away from the object sword collided with. Used for avoiding errounous collision detection with objects
        Vector2 dir = col.contacts[0].point - sword.position;
        dir = -dir.normalized;
        sword.AddForce(dir*30);
    }
}
