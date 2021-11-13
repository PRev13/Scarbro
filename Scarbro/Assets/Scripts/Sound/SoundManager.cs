using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static AudioClip jumpSound, inverseGravitySound, teleportSound; //Audio Clips for character
    static AudioSource audioSrc;

    private void Start() {
        //Allocation of audioclips located in Resources folder
        jumpSound = Resources.Load<AudioClip> ("Jump");
        inverseGravitySound = Resources.Load<AudioClip> ("Inverse");
        teleportSound = Resources.Load<AudioClip> ("Teleport");
        audioSrc = GetComponent<AudioSource> ();
    }

    public static void PlaySound(string clip){
        
        switch(clip) {
            case "Jump":
                audioSrc.PlayOneShot (jumpSound);
                break;
            case "Inverse":
                audioSrc.PlayOneShot (inverseGravitySound);
                break;
            case "Teleport":
                audioSrc.PlayOneShot (teleportSound);
                break;
        }
    }
}
