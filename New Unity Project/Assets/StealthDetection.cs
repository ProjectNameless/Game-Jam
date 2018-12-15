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
        Debug.Log(other.name);
        if (other.tag.Equals("Player"))
            other.GetComponent<PlayerController>().changeHealth(-100);
    }

}
