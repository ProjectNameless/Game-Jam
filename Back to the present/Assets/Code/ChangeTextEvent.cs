using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ChangeTextEvent : Event
{
    public Text textToChange;
    public string newText;
    public override void Call()
    {
        base.Call();
        textToChange.text = newText;
    }
}
