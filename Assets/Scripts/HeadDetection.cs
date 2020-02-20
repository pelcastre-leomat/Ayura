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

public class HeadDetection : MonoBehaviour
{
    //Pally animator
    public Animator animator;
    //Pally box collider
    public BoxCollider2D pallyHitBox;
    //Pally Rigidbody
    public Rigidbody2D pally;
    //Audio effects to play when hit by an apple, when catching Cutter, and when a drop hits head
    public AudioSource hitFX, caughtCutter, dropOnHead;
    //Flag for signaling if head has been hit
    private bool isHit;
    //Flag for signaling if player has died
    public static bool isDead;

    void Start()
    {
        //Reset all variables
        isHit = false;
        isDead = false;
    }

    void Update()
    {
        //If player is dead, play the stunned animation indefenitely, else just play once for 0.5s
        if (isDead)
        {
            hitAnimation(false);
        }else if (isHit)
        {
            hitAnimation(true);
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        //If player isn't hit or dead, if hit by apple play sound, signal hit, play animation, deal damage
        //If hit by Cutter, play sound effect, signal that Cutter was caught
        //If hit by Pickup, call on applesPickedUp method to update time, health and points
        //If hit by drop, deal damage, deal time penalty, signal a hit, play a sound, and destroy drop item
        if (!isHit && !isDead)
        {
            if (col.gameObject.tag == "Apple")
            {
                hitFX.Play();
                isHit = true;
                animator.SetBool("isHit", true);
                if (Difficulty.mode == "Hard")
                {
                    HealthScript.updateHP(-15);
                }
                else
                {
                    HealthScript.updateHP(-10);
                }
            }
            else if (col.gameObject.tag == "Cutter")
            {
                caughtCutter.Play();
                PlayerController.caught = true;
            }else if(col.gameObject.tag == "Pickup")
            {
                PlayerController.applesPickedUp(col);
            }else if(col.gameObject.tag == "Drop")
            {
                HealthScript.updateHP(-50);
                TimeKeeping.timeLeft -= 5;
                isHit = true;
                dropOnHead.Play();
                Destroy(col.gameObject);
            }
        }
    }

    //If head stays in collision with pickup, call on applesPickedUp to update health,time,score
    void OnCollisionStay2D(Collision2D col)
    {
        if (col.gameObject.tag == "Pickup")
        {
            //applePickFX.Play
            PlayerController.applesPickedUp(col);
        }
    }

    //Reset hit flag and play hit animation while coroutine executes
    void hitAnimation(bool stop)
    {

        isHittable(false);
        animator.SetBool("isHit", true);
        if (stop)
        {
            StartCoroutine(stunnedAnimation(0.5f));
        } 
    }

    //Method for creating the duration of the stunned animation
    IEnumerator stunnedAnimation(float s)
    {
        yield return new WaitForSecondsRealtime(s);
        animator.SetBool("isHit", false);
        isHittable(true);
        isHit = false;
    }

    //Used for making player stunning for the duration of the stunned animation
    private void isHittable(bool isHittable)
    {
        PlayerController.isHittable = isHittable;
    }
}
