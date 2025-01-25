using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class DialogueTrigger : MonoBehaviour
{
    public Message[] messages;
    public Actor[] actors;

    private DialogueManager dialogue;

    public void StartDialogue() {

        // dialogue = FindObjectOfType<DialogueManager>();
        
        // if(isActive == true){
        //     dialogue.Active();
        // }      

        dialogue = gameObject.GetComponent<DialogueManager>();   
                
        dialogue.Active(messages,actors);

    }
}

[System.Serializable]
public class Message {
    public int actorId;
    public string message;
    public string soundeffect;
}

[System.Serializable]
public class Actor {
    public string name;
    public Sprite sprite;
}

