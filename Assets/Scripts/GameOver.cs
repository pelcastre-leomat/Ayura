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
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GameOver : MonoBehaviour
{
    //Objects for displaying the different screen
    public GameObject GameOverScreen, inputScreen,onlyGameOver;
    //Flags to signal that player died and that user has input
    public static bool isDead,hasInput;
    //Default name for highscore when user has not input anything
    public static string initials;
    
    private void Start()
    {
        //Reset all variables
        isDead = false;
        hasInput = false;
        initials = "PLYR";
    }

    void Update()
    {
        //If player has died, enter
        if (isDead)
        {
            //Signal the other classes that the player is dead
            PlayerController.isDead = true;
            HeadDetection.isDead = true;
            TimeKeeping.hasDied = true; 

            //Display the Game Over screen
            bringGOS();

            //Retrieves both the easy and the hard score
            int savedEasyScore = int.Parse(PlayerPrefs.GetString("easyHighscore").Split(char.Parse(" "))[1]);
            int savedHardScore = int.Parse(PlayerPrefs.GetString("hardHighscore").Split(char.Parse(" "))[1]);
            
            //If the score is 0 or the current score isn't higher than the current highscore, don't ask the player to input a name
            if (ScoreScript.applesPicked == 0 || savedEasyScore > ScoreScript.applesPicked)
            {
                noInputScreen();
            } else if (ScoreScript.applesPicked == 0 || savedHardScore > ScoreScript.applesPicked) {
                noInputScreen();
            }
        }

        //If the player has input a name, save the score onto the apropriate scoreboard
        if (hasInput)
        {
            string score = initials + " " + ScoreScript.applesPicked + " " + (Math.Round(TimeKeeping.passedTime, 1) + " " + Difficulty.mode);
                
            if (Difficulty.mode == "Easy")
            {
                PlayerPrefs.SetString("easyHighscore", score);
            }
            else
            {
                PlayerPrefs.SetString("hardHighscore", score);
            }
            hasInput = false;
        }
    }

    //Activate the game over screen and signal change
    public void bringGOS()
    {
        GameOverScreen.SetActive(true);
        hasInput = true;
    }

    //Don't display the input screen, just the game over menu
    private void noInputScreen()
    {
        inputScreen.SetActive(false);
        onlyGameOver.SetActive(true);
    }

    //Reload the main level scene
    public void retry()
    {
        SceneManager.LoadScene(1);
    }

    //Destroy the music element so as to avoid overlapping music sources playing and go to the Main menu
    public void gotoMenu()
    {
        GameObject.Destroy(GameObject.Find("Music"));
        SceneManager.LoadScene(0);
    }

    //Listener for the input field, signals update
    public static void getText(string text)
    {
        if(text.Length > 0){
            initials = text;
        }
        hasInput = true;
    }
}
