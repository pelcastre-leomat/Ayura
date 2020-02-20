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
using UnityEngine.UI;
using System;

public class PlayerController : MonoBehaviour
{
    //Rigidbody of Pally
    public Rigidbody2D body;
    //Movement speed for Pally walk
    public static float speed;
    //Animator for changing animations, i.e. idle to running
    public Animator animator;
    //Flags for acting upon player being hit or dying
    public static bool isHittable, isDead;
    //Sound effects for player picking up apple or sword
    public AudioSource applePickFX, cutterPickFX;
    //Movement vector for moving Pally
    private Vector2 movement;
    //Variable for storing the current value of the movement keyboard keys
    private float moveH;
    //Vector and float value for flipping the player to the corresponding button press direction
    private Vector2 characterScale;
    private float characterScaleX;
    //Position of Pally for Birb attack
    public static Vector2 local;
    //Used to update Birb on the player posisition
    public static float pos;

    //Sword gameobject for interacting with sword and its properties/components
    public GameObject swordObject;
    //Sword throw multiplier speed
    public static float swordSpeedMult = 1;
    //Flag for signaling if sword has been caught by head of Pally
    public static bool caught;
    //Flag for signaling if sword is currently wielded by Pally
    private static bool swordWielded;
    //Sword speed
    private float swordSpeed;

    void Start()
    {
        //Reset values for all variables
        isHittable = true;
        isDead = false;
        caught = false;
        swordWielded = false;
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;

        //Assign rigidbody component to boyd variable
        body = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //If isDead flag is not set, continue
        if (!isDead)
        {
            //Used to move the player as well as changing animation and setting a direction, i.e. flipping
            moveH = Input.GetAxis("Horizontal");
            movement = new Vector2(moveH, 0.0f);
            animator.SetFloat("speed", Math.Abs(body.velocity.x));
            flip();

            //Cutter is caught by head, wield sword
            if (caught)
            {
                caught = false;
                wieldSword();
            }
        }

        //If the sword is wielded, allow the player to throw it
        if (swordWielded)
        {
            swordObject.transform.position = new Vector2(body.position.x, body.position.y + 0.5f);
            if (Input.GetKeyDown(KeyCode.Space))
            {
                throwSword();
            }
        }
    }

    void FixedUpdate()
    {
        //While the player flag isDead is not set, allow it to move Pally as well as update the position Birb uses for attacks
        if (!isDead)
        {
            body.velocity += movement * speed * Time.fixedDeltaTime;
            pos = body.position.x;
        }
    }

    //Player is hittable and not dead:
    //If collision is detected with a pickup item, play a sound effect and call applePickedUp method
    //if collision is detected with Cutter, play a sound and call on wieldSword method
    void OnCollisionEnter2D(Collision2D col)
    {
        if (isHittable && !isDead)
        {
            if (col.gameObject.tag == "Pickup")
            {
                applePickFX.Play();
                applesPickedUp(col);
            }
            else if (col.gameObject.tag == "Cutter")
            {
                //Removed this and the made the variable public
                //swordObject = col.gameObject;
                cutterPickFX.Play();
                wieldSword();
            }
        }
    }

    //Player is hittable and not dead:
    //If continued collision is detected with a pickup item, play a sound effect and call applePickedUp method
    //if continued collision is detected with Cutter, play a sound and call on wieldSword method
    void OnCollisionStay2D(Collision2D col)
    {
        if (!isDead)
        {
            if (col.gameObject.tag == "Pickup")
            {
                applePickFX.Play();
                applesPickedUp(col);
            }
            else if (col.gameObject.tag == "Cutter")
            {
                cutterPickFX.Play();
                wieldSword();
            }
        }
    }

    //Mirror the Pally sprite depending on the movement direction the player has, also set the speed of the sword throw
    private void flip()
    {
        if (moveH < 0)
        {
            swordSpeed = Math.Abs(body.velocity.x) *-1;
            characterScale.x = -characterScaleX;
        }
        else if (moveH > 0)
        {
            swordSpeed = Math.Abs(body.velocity.x);
            characterScale.x = characterScaleX;
        }
        transform.localScale = characterScale;
    }

    //When called, sword rotation is set to 0, the sword's collider is disabled, and its rigidbody is set to static
    public void wieldSword()
    {
        swordObject.transform.SetPositionAndRotation(new Vector3(0, 0, 0), Quaternion.Euler(0, 0, 0f));
        swordObject.GetComponent<CapsuleCollider2D>().enabled = false;
        swordObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        swordWielded = true;
    }

    //When called reactivates the collider of the sword as well as sets the rigidbody to dynamic again.
    //A velocity depending on the player's movement is also applied
    private void throwSword()
    {
        swordWielded = false;
        Rigidbody2D swordBody = swordObject.GetComponent<Rigidbody2D>();
        swordObject.GetComponent<CapsuleCollider2D>().enabled = true;
        swordBody.bodyType = RigidbodyType2D.Dynamic;
        swordBody.velocity = new Vector2(swordSpeed * swordSpeedMult, Math.Abs(swordSpeed)*swordSpeedMult);
    }

    //Used to set the speed of the player movement as well as the speed of the sword, used from outside of class
    public static void setSpeeds(float setSpeed,float setSwordSpeed)
    {
        speed = setSpeed;
        swordSpeedMult = setSwordSpeed;
    }

    //Destroy the pickup object, update the time, health, as well as points, finally update amount of apples to be spawned
    public static void applesPickedUp(Collision2D col)
    {
        Destroy(col.gameObject);
        TimeKeeping.moreTime(5f);
        ScoreScript.increaseScore(50);
        if (Difficulty.mode == "Hard")
        {
            HealthScript.updateHP(5);
        }
        else
        {
            HealthScript.updateHP(10);
        }
        AppleSpawner.applesSpawned -= 1;
    }
}



