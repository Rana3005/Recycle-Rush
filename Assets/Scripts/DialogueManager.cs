using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    //class can be accessed from anywhere
    public static DialogueManager Instance;

    public Image characterIcon;
    public TextMeshProUGUI nameCharacter;
    public TextMeshProUGUI dialogueArea;

    public bool isDialogueActive = false;
    public float typingSpeed = 0.1f;
    public Animator animator;

    //queue for lines
    private Queue<DialogueLine> lines;

    private void Start(){
        if(Instance == null){
            Instance = this;
        }

        lines = new Queue<DialogueLine>();
    }

    public void StartDialogue(Dialouge dialogue){
        isDialogueActive = true;
        animator.Play("show");
        lines.Clear();
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        foreach (DialogueLine line in dialogue.dialogueLines)
        {
            lines.Enqueue(line);
        }

        DisplayNextDialogueLine();
    }

    public void DisplayNextDialogueLine(){
        if(lines.Count == 0){
            //check if queue is empty
            EndDialogue();
            return;
        }

        DialogueLine currentLine = lines.Dequeue();

        characterIcon.sprite = currentLine.character.icon;
        nameCharacter.text = currentLine.character.name;

        StopAllCoroutines();

        StartCoroutine(SentenceType(currentLine));
    }

    IEnumerator SentenceType(DialogueLine dialogueLine){
        dialogueArea.text = "";
        foreach(char letter in dialogueLine.line.ToCharArray()){
            dialogueArea.text += letter;
            yield return new WaitForSeconds(typingSpeed);
        }
    }

    void EndDialogue(){
        isDialogueActive = false;
        animator.Play("hide");
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
    
}
