using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    Dialogues dialogues;
    public Sprite[] sprites;
    GameObject thePlayer;

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        dialogues = GameManager.Instance.dialogues;
        _spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        thePlayer = GameObject.FindGameObjectWithTag(k.Tags.PLAYER);

    }

    private void Update() {

        //comment this out as a work around for now
        //if( dialogues.GetComponent<Dialogues>().dialogueEnded == true){
            //Destroy(this.gameObject); // People disappears when dialogues are finished.
        //}
    }

    private void OnCollisionEnter2D(Collision2D other) {
        // Executed when Player reaaches the people and executes only once. && dialogues.GetComponent<Dialogues>().dialogueStarted == false
       if(other.gameObject.tag == "Player" ){
            dialogues.GetComponent<Dialogues>().startDialouge();
            Destroy(this.gameObject); // People disappear right away however only 1 person does
            thePlayer.GetComponent<Player>().UpdatePeopleRescueAdd1();
        }
   }
}
