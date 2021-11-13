using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dialogues : MonoBehaviour
{
    public string[] lines = {"Scarbro : No need to panic, you are in safe hands.","People : Phew... We thought this was it. Go save others."}; 
    public float textSpeed; //Dialogue Speed

    private int index; // Index for the dialogue lines
    public bool dialogueEnded = false;
    public bool dialogueStarted = false;

    private void Start() {
        GetComponent<TMPro.TextMeshProUGUI>().text = string.Empty;
    }

    private void Update() {
        // Skips the dialogue time when pressed 'E'
        if(Input.anyKeyDown && dialogueStarted){
            if(GetComponent<TMPro.TextMeshProUGUI>().text == lines[index]){
                //Skips to the next sentence
                NextLine();
            } else {
                // Completes the current sentece
                StopAllCoroutines();
                GetComponent<TMPro.TextMeshProUGUI>().text = lines[index];
            }
        }
    }

    public void startDialouge(){
        index = 0;
        dialogueStarted = true;
        StartCoroutine(TypeLine());
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
            gameObject.SetActive(false);
            dialogueEnded = true;
        }
    }
}
