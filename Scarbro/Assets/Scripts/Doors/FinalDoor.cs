using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalDoor : MonoBehaviour
{

    GameObject thePlayer;
    public Transform cameraTarget;

    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag(k.Tags.PLAYER);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        SoundManager.PlaySound("Teleport");//Play Teleport Sound when passing through doors.
        if (thePlayer.GetComponent<Player>().GetPeopleRescue() == 6) //makes player spawn left of door
        {
            thePlayer.transform.position = new Vector3(0,-11,0); //transport to a completed "screen"
                                                               //if we had multiple levels we'd transition to next scene

            Vector3 pos = cameraTarget.position;
            pos.z = -10f;
            Camera.main.transform.position = pos;
        }

    }
}
