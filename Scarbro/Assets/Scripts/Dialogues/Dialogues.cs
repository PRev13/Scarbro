using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string[] lines = {"Scarbro : No need to panic, you are in safe hands.","People : Phew... We thought this was it. Go save others."}; //these lines arent the ones used in game rn
    public float textSpeed; //Dialogue Speed

    private int index; // Index for the dialogue lines
    public bool dialogueInProgress = false;
    public bool dialogueStarted = false;

    private void Start() {
        GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
    }

    private void Update() {        
            if(GetComponent<TMPro.TextMeshProUGUI>().text == lines[index]){
                //Skips to the next sentence
                NextLine();
            }
        
    }

    public void startDialouge(){
        if(dialogueInProgress == false)
        {
            index = 0;
            dialogueStarted = true;
            dialogueInProgress = true;
            StartCoroutine(TypeLine());
        }
    }

    IEnumerator TypeLine(){
        // Displays the sentence charcter by character with wait time
        foreach (char c in lines[index].ToCharArray())
        {
            GetComponent<TMPro.TextMeshProUGUI>().text += c;
            yield return new WaitForSeconds(textSpeed);
        }
    }


    void NextLine(){

        if(index < lines.Length -1)
        {
            //Next Sentence of dialogue
            index++;
            GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
            StartCoroutine(TypeLine());
        } else {
            //End of Conversation
            //gameObject.SetActive(false);
            dialogueInProgress = false;
        }
    }
}
