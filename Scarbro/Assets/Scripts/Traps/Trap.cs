using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag(k.Tags.PLAYER))
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
    }
}
