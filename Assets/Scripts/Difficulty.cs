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

public class Difficulty : MonoBehaviour
{
    //Used for setting difficulty outside of class
    public static string mode;
    void Start()
    {
        //Set difficulty at start
        setDifficulty();   
    }

    //The "easy" and "hard" methods set the Birb nad player movement, initial length of game, and multiplier speed for velocity of spawned objects
    private static void easy()
    {
        BirbMovement.setSpeeds(1f, 1.2f, 0.5f,2,1);
        PlayerController.setSpeeds(15,2);
        TimeKeeping.timeLeft = 60;
        SpawnedObject.speedMult = 1.2f;

    }
    private static void hard()
    {
        BirbMovement.setSpeeds(2f,1.3f, 0.5f,0.7f,1.3f);
        PlayerController.setSpeeds(19,1f);
        TimeKeeping.timeLeft = 40;
        SpawnedObject.speedMult = 1.4f;
    }

    //Checks the stored difficulty set in the options menu in the main menu and sets the difficulty accordingly
    public static void setDifficulty()
    {
        bool isHard = Options.toBool(PlayerPrefs.GetInt("isHard"));
        if (isHard)
        {
            hard();
            mode = "Hard";
        }
        else
        {
            easy();
            mode = "Easy";
        }
    }
}
