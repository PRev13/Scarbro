using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTeleport : MonoBehaviour
{

    public Transform doorTarget;
    public BoxCamera boxCamera;
    public BoxCamera.FACES face;
    GameObject thePlayer;
    

    [Header("Target Door Parameters")]
    public bool spawnToLeftOfTargetDoor; //will the player spawn to the left of the target door?
    public float spawnOffset; //x position offset of player spawn from a door

    private void Start()
    {
        thePlayer = GameObject.FindGameObjectWithTag(k.Tags.PLAYER);
        if(boxCamera == null)
        {
            Debug.LogWarning("This door is missing camera", gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
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

        boxCamera.MoveCamera(doorTarget.GetComponent<DoorTeleport>().face);
    }

    private void OnDrawGizmosSelected()
    {
        if (doorTarget != null)
        {
            //We draw just a line to see more easy with witch door is conected
            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, doorTarget.position);
            DoorTeleport otherDoor = doorTarget.GetComponent<DoorTeleport>();
            //We draw a circle where the player is going to spawn
            Vector3 offset = Vector3.zero;
            offset.x = otherDoor.spawnToLeftOfTargetDoor ? otherDoor.spawnOffset : -otherDoor.spawnOffset;
            Gizmos.DrawWireSphere(doorTarget.position + offset, 0.3f);
            //We draw where is going to spawn in this door
            Gizmos.color = Color.green;
            offset.x = spawnToLeftOfTargetDoor ? spawnOffset : -spawnOffset;
            Gizmos.DrawWireSphere(transform.position + offset, 0.3f);
        }
    }
}
