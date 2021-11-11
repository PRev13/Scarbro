using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PlatformFalls : MonoBehaviour
{
    [SerializeField]
    private float fallTime_t, GravityDirction;

    // called when the character touches the fallable platform
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            Invoke(nameof(PlatformsFallsWithTime), fallTime_t);
        }
    }

    void PlatformsFallsWithTime ()
    {
        transform.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        transform.GetComponent<Rigidbody2D>().gravityScale = GravityDirction;
        transform.DOScaleY(0, 0.8f);
    }
}
