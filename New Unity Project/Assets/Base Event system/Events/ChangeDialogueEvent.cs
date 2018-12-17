using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialogueEvent : Event {

    public GameObject targetGO;
    public DialogueSO[] dialogueSOs;
    void Start()
    {
        DialogueEngine.instance.StartDisplayTextInTime(dialogueSOs, gameObject);
        
    }
}
