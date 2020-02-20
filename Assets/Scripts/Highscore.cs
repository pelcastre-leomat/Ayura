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

public class Highscore : MonoBehaviour
{
    //Used to display the highscores when the score is bigger than 0
    public GameObject easyHighscoreContainer, hardHighscoreContainer;
    //Array of the text elements in each of the highscore containers
    public TMP_Text[] easyElems, hardElems;
    //Used to update the tables, socreReset is used to reset the scores and hide the tables
    private bool hasChanged, scoresReset;

    void Start()
    {
        //Reset the variable
        hasChanged = true;
    }

    void Update()
    {
        //If the scoresReset flag is set, reset scores and hide the score tables
        if (scoresReset){
            if (scoresReset)
            {
                resetScores();
                easyHighscoreContainer.SetActive(false);
                hardHighscoreContainer.SetActive(false);
                scoresReset = false;
            }
        }

        //Enter if flag is set
        if (hasChanged)
        {
            //Reset flag in order to only update when actually needed
            hasChanged = false;
            //If the key easy or hard keys don't exist or they exist and are of inappropriate length, create/reset the values to fit the set format
            if (!PlayerPrefs.HasKey("easyHighscore") || PlayerPrefs.GetString("easyHighscore").Length < 3)
            {
                string score = "AAAA" + " " + 0 + " " + 0 + " " + "Easy";
                PlayerPrefs.SetString("easyHighscore", score);
            }else if (!PlayerPrefs.HasKey("hardHighscore") || PlayerPrefs.GetString("easyHighscore").Length < 3)
            {
                string score = "AAAA" + " " + 0 + " " + 0 + " " + "Hard";
                PlayerPrefs.SetString("hardHighscore", score);
            }

            //String arrays for easy access of the score elements for the easy and hard scores
            string[] easyHighScore = PlayerPrefs.GetString("easyHighscore").Split(char.Parse(" "));
            string[] hardHighScore = PlayerPrefs.GetString("hardHighscore").Split(char.Parse(" "));

            //If the scores are bigger than 0, display them by updating the text elements and setting the container to be visible
            if (int.Parse(easyHighScore[1]) != 0)
            {
                easyHighscoreContainer.SetActive(true);
                for (int i = 0; i < easyElems.Length; i++)
                {
                    easyElems[i].text = easyHighScore[i];
                }
            }
            if (int.Parse(hardHighScore[1]) != 0)
            {
                hardHighscoreContainer.SetActive(true);
                for (int i = 0; i < hardElems.Length; i++)
                {
                    hardElems[i].text = hardHighScore[i];
                }
            }
        }
    }

    //Reset the scores
    public void resetScores()
    {
        string scores = "AAAA" + " " + 0 + " " + 0 + " " + "Hard";
        PlayerPrefs.SetString("hardHighscore", scores);
        string scoreS = "AAAA" + " " + 0 + " " + 0 + " " + "Easy";
        PlayerPrefs.SetString("easyHighscore", scoreS);
        scoresReset = true;
    }
}
