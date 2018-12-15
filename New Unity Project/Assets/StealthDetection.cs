using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealthDetection : MonoBehaviour
{
    private AIController controller;
    // Start is called before the first frame update
    void Start()
    {
        controller = gameObject.GetComponentInParent<AIController>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag.Equals("Player"))
        {
            Debug.Log("spotted player");
            controller.spottedPlayer();
        }
    }

}
