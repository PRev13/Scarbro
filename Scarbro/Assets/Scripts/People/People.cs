using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class People : MonoBehaviour
{
    Dialogues dialogues;
    public Sprite[] sprites;
    public Sprite[] spritesRescued;
    GameObject thePlayer;
    private int spriteIndex; 

    private SpriteRenderer _spriteRenderer;

    private void Awake() {
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Start() {
        dialogues = GameManager.Instance.dialogues;
        spriteIndex = Random.Range(0, sprites.Length);
        _spriteRenderer.sprite = sprites[spriteIndex];
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
            GetComponent<BoxCollider2D> ().enabled = false; // Disables box collider so it won't trigger the dialogues a second time.
            dialogues.GetComponent<Dialogues>().startDialouge();
            _spriteRenderer.sprite = spritesRescued[spriteIndex]; //Changes sprite of the people when rescued
            Destroy(this.gameObject, 4); // People disappear after 4 seconds however only 1 person does
            thePlayer.GetComponent<Player>().UpdatePeopleRescueAdd1();
        }
   }
}
