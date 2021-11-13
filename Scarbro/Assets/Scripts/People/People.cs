using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    public Dialogues dialogues;
    public Sprite[] sprites;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];

    }

    private void Update() {
        if( dialogues.GetComponent<Dialogues>().dialogueEnded == true){
            Destroy(this.gameObject); // People disappears when dialogues are finished.
        }
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Executed when Player reaaches the people and executes only once.
       if(other.gameObject.tag == "Player" && dialogues.GetComponent<Dialogues>().dialogueStarted == false){
            dialogues.GetComponent<Dialogues>().startDialouge();
       }
   }
}
