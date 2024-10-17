using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInteraction : MonoBehaviour
{
    GameObject currentInteractable;
    // Start is called before the first frame update
    void Start()
    {
        currentInteractable = null;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            currentInteractable = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (currentInteractable != null)
        {
            currentInteractable = null;
        }
    }
}
