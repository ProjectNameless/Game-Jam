using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueEngine : MonoBehaviour {
    public Text Captions;
    private AudioSource lastCall;
    #region singleton
    public static DialogueEngine instance;
    private void Awake()
    {
        instance = this;
    }
    #endregion
    // Use this for initialization
    void Start () {
	}
    /// <summary>
    /// displays whole string within time specified. letters per second = text.length/time
    /// </summary>
    /// <param name="text"></param>
    /// <param name="time"></param>
	public void StartDisplayTextInTime(DialogueSO[] dialogues, GameObject caller)
    {
        if (lastCall != null)
        {
            lastCall.Pause();
            lastCall = null;
        }
        StopAllCoroutines();
        StartCoroutine(DisplayTextInTime(dialogues, caller));
    }
    /// <summary>
    /// the coroutine that runs along side the update function to allow pausing and checking things mid frame.
    /// </summary>
    /// <param name="dialogues"></param>
    /// <param name="caller"></param>
    /// <returns></returns>
    private IEnumerator DisplayTextInTime(DialogueSO[] dialogues, GameObject caller)
    {
        foreach (DialogueSO currentDialogue in dialogues)
        {
            if (currentDialogue is DialogueWithAudioSO)
            {
                AudioSource audioSource;
                DialogueWithAudioSO dialogueWithAudio = ((DialogueWithAudioSO)currentDialogue);
                audioSource = caller.GetComponent<AudioSource>();
                AudioClip soundClip = dialogueWithAudio.voice;
                audioSource.clip = soundClip;
                audioSource.Play();
                lastCall = audioSource;
                float lettersPerSecond = (currentDialogue.text.Length - 1) / dialogueWithAudio.time;
                int letterIndex = 0;
                Captions.text = "";
                while ((Captions.text.Length < currentDialogue.text.Length) && (!Input.GetKeyDown(KeyCode.Return)))
                {
                    Captions.text += currentDialogue.text[letterIndex];
                    letterIndex++;
                    yield return new WaitForSeconds(1 / lettersPerSecond);
                }
                Captions.text = currentDialogue.text;
            }
            else
            {
                float lettersPerSecond = (currentDialogue.text.Length - 1) / currentDialogue.time;
                int letterIndex = 0;
                Captions.text = "";
                while ((Captions.text.Length - 1 < currentDialogue.text.Length - 1) && (!Input.GetKeyDown(KeyCode.Return)))
                {
                    Captions.text += currentDialogue.text[letterIndex];
                    letterIndex++;
                    yield return new WaitForSeconds(1 / lettersPerSecond);
                }
                Captions.text = currentDialogue.text;
                float timeTillExpire = currentDialogue.time;
                yield return null;
                while (timeTillExpire > 0 && !Input.GetKeyDown(KeyCode.Return))
                {
                    yield return null;
                    timeTillExpire -= Time.deltaTime;
                }
            }
            Captions.text = "";
        }
    }
}
