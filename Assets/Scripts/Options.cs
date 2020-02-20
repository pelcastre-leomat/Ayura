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

public class Options : MonoBehaviour
{
    //Music audio source
    public AudioSource audioPlayer;
    //Toggles for music, difficulty and fx;
    public Toggle musicToggle, diffToggle, fxToggle;
    //Sliders for music volume and fx volume
    public Slider volumeSlider,fxVolumeSlider;
    //Flag for signaling changed has been made
    private bool hasChanged = true;

    private void Update()
    {
        //Used to maintain the same settings throughout the game and after exiting
        //Applies the saved values to the sliders, toggles, and music audio source
        if (hasChanged)
        {
            audioPlayer.volume = PlayerPrefs.GetFloat("storedVolume");
            musicToggle.isOn = toBool(PlayerPrefs.GetInt("withMusic"));
            volumeSlider.value = PlayerPrefs.GetFloat("storedVolume")/0.2f;
            fxVolumeSlider.value = PlayerPrefs.GetFloat("storedFxVolume");
            fxToggle.isOn = toBool(PlayerPrefs.GetInt("withFx"));
            diffToggle.isOn = toBool(PlayerPrefs.GetInt("isHard"));
            hasChanged = false;
        }
    }

    //Store the value of the slider, apply the value to the music audio source, signal change
    public void volumeSettings(float value)
    {
        PlayerPrefs.SetFloat("storedVolume", value*0.2f);
        audioPlayer.volume = PlayerPrefs.GetFloat("storedVolume");
        hasChanged = true;
    }

    //Store the value of the fx slider, signal change
    public void fxSettings(float value)
    {
        PlayerPrefs.SetFloat("storedFxVolume", value);
        hasChanged = true;
    }

    //Store fx value of toggle, signal change
    public void fx(bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt("withFx", 1);
        }
        else
        {
            PlayerPrefs.SetInt("withFx", 0);
        }
        hasChanged = true;
    }

    //Store music value of toggle, signal update
    public void music(bool state)
    {
        if (state)
        {
            PlayerPrefs.SetInt("withMusic", 1);
            audioPlayer.Play();
        }
        else
        {
            PlayerPrefs.SetInt("withMusic", 0);
            audioPlayer.Pause();
        }
        hasChanged = true;
    }

    //Store value of difficulty toggle, signal update
    public void  isHard(bool hard)
    {
        if (hard)
        {
            PlayerPrefs.SetInt("isHard", 1);
        }
        else
        {
            PlayerPrefs.SetInt("isHard", 0);
        }
        hasChanged = true;
    }

    //Translate an int with value 0 or 1 into a bool
    public static bool toBool(int b)
    {
        bool f = false;
        if (b == 1)
        {
            f = true;
        }
        return f;
    }
}
