using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class customer : MonoBehaviour
{
    [SerializeField] public string customerID;
    bool canInteract;
    playerHub hub;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (canInteract && Input.GetKeyUp(KeyCode.E))
        {
            checkForPlate();
        }
    }
    void checkForPlate()
    {
        if(hub != null)
        {
            for(int i = 0;i < hub.plates.Count;i++)
            {
                Debug.Log(hub.plates[i].cusID + ' '+ customerID);
                if(hub.plates[i].cusID == customerID)
                {
                    hub.plates[i].delivered();
                    hub.plates.RemoveAt(i);
                    StartCoroutine(deliver());
                }
            }
        }
    }
    IEnumerator deliver()
    {
        yield return null;
    }
    void setupCustomer(string id)
    {
        customerID = id;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            hub = collision.gameObject.GetComponent<playerHub>();
            canInteract = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            canInteract = false;
        }
    }
}
