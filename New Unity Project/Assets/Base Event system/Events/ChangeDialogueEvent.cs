using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeDialogueEvent : Event {

    public GameObject targetGO;
    public DialogueSO[] dialogueSOs;
    public override void Call()
    {
        DialogueEngine.instance.StartDisplayTextInTime(dialogueSOs, gameObject);
        if (next != null)
        {
            DialogueEngine.instance.next = next;
        }
    }
}
