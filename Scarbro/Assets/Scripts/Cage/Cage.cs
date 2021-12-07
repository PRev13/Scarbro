using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[SelectionBase]
public class Cage : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D other) {
        if(other.gameObject.tag == "Player"){
            ClearChildren();
            Destroy(this.gameObject);
         }
    }

    //Destroys all the child of Cage GameObject
    public void ClearChildren() {

    float i = 0;
    foreach (Transform child in transform) {
        i += 1;
        Destroy(child.gameObject);
    }
    //Debug.Log(transform.childCount);
}
}
