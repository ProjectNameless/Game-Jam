using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Dialogue with audio", menuName = "Dialogue/with audio", order = 1)]
public class DialogueWithAudioSO : DialogueSO {

    public AudioClip voice;
    public bool interuptable;
}
