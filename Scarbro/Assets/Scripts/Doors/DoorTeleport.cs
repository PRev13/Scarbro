using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{

    public Transform doorTarget;
    public Transform cameraTarget;
    GameObject thePlayer;
    

    [Header("Target Door Parameters")]
    public bool spawnToLeftOfTargetDoor; //will the player spawn to the left of the target door?
    public float spawnOffset; //x position offset of player spawn from a door

    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag(k.Tags.PLAYER);
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        SoundManager.PlaySound("Teleport");//Play Teleport Sound when passing through doors.
        if (spawnToLeftOfTargetDoor) //makes player spawn left of door
        {
            thePlayer.transform.position = doorTarget.transform.position - new Vector3(spawnOffset, 0f, 0);
            //reset spawn now for if player dies later
            thePlayer.GetComponent<Player>().spawnLocation = thePlayer.transform.position;
        }
        else //makes player spawn right of door
        {
            thePlayer.transform.position = doorTarget.transform.position + new Vector3(spawnOffset, 0f, 0);
            //reset spawn now for if player dies later
            thePlayer.GetComponent<Player>().spawnLocation = thePlayer.transform.position;
        }
        Vector3 pos = cameraTarget.position;
        pos.z = -10f;
        Camera.main.transform.position = pos;
        
    }
}
