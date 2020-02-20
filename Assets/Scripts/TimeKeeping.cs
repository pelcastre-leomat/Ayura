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
using TMPro;

public class TimeKeeping : MonoBehaviour
{
    //Used for keeping track of time since level started and the time left respectively 
    public static float passedTime;
    public static float timeLeft;

    //Used for displaying the current time left
    public TMP_Text timeText;
    //Flag used for signaling when the player has died so as to stop counting time left and total
    public static bool hasDied;
    //Flag used for updating as unfrequently as possible
    private bool hasChanged;

    private static int giveTime, timesBirbHit;

    void Start()
    {
        //Reset values
        hasChanged = true;
        hasDied = false;
        timesBirbHit = 0;
        giveTime = 0;
    }
    
    void Update()
    {
        //Update screen if values have changed; flag set within the third if
        if (hasChanged)
        {
            //If time is less than 0.001 signal that the player has died and stop updating the time
            if (timeLeft < 0.001)
            {
                GameOver.isDead = true;
                hasChanged = false;
            }
            else
            {
                //If the player hasn't died update both the elapsed time and the time left
                if (!hasDied)
                {
                    timeLeft -= Time.deltaTime;
                    passedTime = timeLeft;
                    passedTime = Time.timeSinceLevelLoad;
                    timeText.text = "Time: " + (timeLeft).ToString("0");
                    hasChanged = true;
                }
            }
        }
    }

    //Method used for increasing time by the float value 'time' for every 3th apple picked
    public static void moreTime(float time)
    {
        if (giveTime == 3)
        {
            timeLeft += time;
            giveTime = 0;
        }
        giveTime++;
    }

    //Controls how many hits to Birb the player need to make for a time bonus, based on difficulty
    public static void birbHitTime()
    {
        if (timesBirbHit == 2)
        {
            if (Difficulty.mode == "Hard") {
                timeLeft += 5;
            }
            else
            {
                timeLeft += 10;
            }
            timesBirbHit = 0;
        }
        timesBirbHit++;
    }
}
