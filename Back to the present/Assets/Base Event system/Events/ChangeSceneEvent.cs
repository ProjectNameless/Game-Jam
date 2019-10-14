using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeSceneEvent : Event
{
    public int index;
    public override void Call()
    {
        base.Call();
        SceneManager.LoadScene(index);
    }
}
