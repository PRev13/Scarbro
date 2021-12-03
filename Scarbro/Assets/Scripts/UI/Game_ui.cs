using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Game_ui : MonoBehaviour
{
    [SerializeField] Image[] liveImgs;
    [SerializeField] TextMeshProUGUI peopleSavedTxt;

    public void LivesUpdate(int _lives)
    {
        //Turn off all images
        for (int i=0; i<liveImgs.Length; i++)
        {
            liveImgs[i].enabled = false;
        }

        //Turn on the current lives
        for (int i=0; i< _lives; i++)
        {
            liveImgs[i].enabled = true;
        }
    }

    public void PeopleSavedUpdate(int _n)
    {
        peopleSavedTxt.SetText(_n + "/6");
    }



}
