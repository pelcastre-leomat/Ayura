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
using System;

public class BirbMovement : MonoBehaviour{

    //Movement vector
    public static Vector2 movement;
    //Birb RigidBody
    private Rigidbody2D birb;
    //Animatior of Birb
    public Animator animator;
    //Flag for signaling if Birb has been hit
    public static bool isHit;
    //Hit birb sound effect
    public AudioSource hitBirb;
    //Spawn new object sound effect
    public AudioSource spawnItemFx;
    //Array containing all the spawnable objects
    public GameObject[] prefabArr;
    //Variables for movement speed multiplier, Birb "sight" range for detecting the player for attack and 
    //time to wait before an object may be spawned
    private static float speedMult,range,timeBeforeSpawn;
    //Counter for limiting the amount of objects spawned in one moment
    private static int spawnedObjects;
    //Used for flipping Birb in the corresponding direction
    private float characterScaleX;
    private Vector2 characterScale;
    //Flags for signaling Birb needs to flip
    //wasHit signals that, when going offscreen, Birb needs to wait for 3 seconds before re-entering sight
    public static bool setFlip, wasHit;
    //Values used for updating Birb's position when new coordinates are created when colliding with either wall
    public static float xVal, yVal;

    void Start()
    {
        //Reset all variables
        setFlip = false;
        birb = GetComponent<Rigidbody2D>();
        isHit = false;
        spawnedObjects = 0;

        //Used for flipping Birb
        characterScale = transform.localScale;
        characterScaleX = characterScale.x;
    }

    void Update()
    {
        //Allow Birb to spawn an object at player's position if there are 0 spawned objects, Birb hasn't been hit, and player isn't dead
        if (spawnedObjects != 1 && !isHit && !GameOver.isDead)
        {
            if ((birb.position.x >= PlayerController.pos - range) && (birb.position.x <= PlayerController.pos + range))
            {
                spawnedObjects = 1;
                spawnItemFx.Play();
                Instantiate(prefabArr[UnityEngine.Random.Range(0, 5)], birb.position, Quaternion.identity);
                StartCoroutine(spawnRandObj());
            }
        }

        //If setFlip flag is set, change Birb's position to new one, flip Birb in corresponding direction, and reset flag
        if (setFlip)
        {
            birb.position = new Vector2(xVal, yVal);
            flip();
            setFlip = false;
        }
    }

    void FixedUpdate()
    {
        //Move Birb
        birb.MovePosition(birb.position + movement* Time.fixedDeltaTime);    
    }

    //If birb is hit by Cutter, play the stunned animation as well as update the score and the time
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Cutter")
        {
            wasHit = true;
            hitBirb.Play();
            hitAnimation();
            ScoreScript.increaseScore(100);
            TimeKeeping.birbHitTime();
        }
    }

    //Method for changing the animation of Birb to the stunned animation for the duration of the coroutine
    void hitAnimation()
    {
        if(movement.x * speedMult <= 40 && Difficulty.mode == "Hard")
        {
            movement *= speedMult;
        }
        else if(movement.x * speedMult <= 15 && Difficulty.mode == "Easy")
        {
            movement *= speedMult;
        }
        isHit = true;
        animator.SetBool("isHit", true);
        StartCoroutine(stunnedAnimation());
    }

    //Coroutine used for creating the duration of the stunned animation
    IEnumerator stunnedAnimation()
    {
        yield return new WaitForSecondsRealtime(1f);
        animator.SetBool("isHit", false);
        isHit = false;
    }

    //Used to set movement speeds as well as object spawn time and throw speed from outside of the class
    public static void setSpeeds(float speed,float mult, float setRange,float tbs, float throwSpeed)
    {
        movement.x = speed;
        speedMult = mult;
        range = setRange;
        timeBeforeSpawn = tbs;
        SpawnedObject.speedMult = throwSpeed;
    }

    //Wait before allowing Birb to spawn an object
    IEnumerator spawnRandObj()
    {
        yield return new WaitForSecondsRealtime(timeBeforeSpawn);
        spawnedObjects = 0;
    }

    //Depending on the generated coordinates from the WallSpawner class, flip birb in the corresponding direction
    private void flip()
    {
        if (xVal < 0)
        {
            movement.x = Math.Abs(movement.x);
            characterScale.x = Math.Abs(characterScaleX);
        }
        else if (xVal > 0)
        {
            movement.x = Math.Abs(movement.x) *-1;
            characterScale.x = Math.Abs(characterScaleX) * -1;
        }
        transform.localScale = characterScale;
    }
}
