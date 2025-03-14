using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class DialogueCharacter {
    //groups name and icon into one class
    public string name;
    public Sprite icon;
}

[System.Serializable]
public class DialogueLine {
    //uses dialogue character for asset
    public DialogueCharacter character;
    //allows large amounts of text to be inputted
    [TextArea(3, 10)]
    public string line;
}

[System.Serializable]
public class Dialouge {
    public List<DialogueLine> dialogueLines = new List<DialogueLine>();
}

public class DialogueTrigger : MonoBehaviour
{
    public Dialouge dialouge;

    public void TriggerDialouge(){
        DialogueManager.Instance.StartDialogue(dialouge);
    }

    private void OnTriggerEnter(Collider collision){
        if(collision.CompareTag("Player")){
            Debug.Log("Player Dialogue trigger");
            TriggerDialouge();
        }
    }
}
