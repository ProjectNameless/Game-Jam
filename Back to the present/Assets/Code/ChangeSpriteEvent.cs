using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class ChangeSpriteEvent : Event
{
    public Image imageToChange;
    public Sprite newSprite;
    public override void Call()
    {
        base.Call();
        imageToChange.sprite = newSprite;
    }
}
