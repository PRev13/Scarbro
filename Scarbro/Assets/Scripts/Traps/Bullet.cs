using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag(k.Tags.PLAYER))
        {
            collision.gameObject.GetComponent<Player>().Die();
        }
        Destroy(gameObject);
    }
}
