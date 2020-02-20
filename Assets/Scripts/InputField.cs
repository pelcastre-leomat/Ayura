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

public class InputField : MonoBehaviour
{
    //Input field object
    public TMP_InputField tmpInputField;

    void Start()
    {
        //When button is pressed Listener getText will be updated
        tmpInputField.onEndEdit.AddListener(GameOver.getText);
    }
}
