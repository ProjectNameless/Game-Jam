using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "Dialogue without audio", menuName = "Dialogue/without audio", order = 0)]
public class DialogueSO : ScriptableObject {

    public string text;
    public float time;
    public string nextEvent;
}
