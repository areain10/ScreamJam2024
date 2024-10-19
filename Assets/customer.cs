using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static Unity.Burst.Intrinsics.X86.Avx;

public class customer : MonoBehaviour
{
    [SerializeField] public string customerID;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogue;
    Image devilSprite;
    Canvas customerDia;
    public string customerName;
    bool canInteract;
    playerHub hub;

    // Start is called before the first frame update
    void Start()
    {
        nameText.text = customerID;
        customerDia = GameObject.FindGameObjectWithTag("CustomerDialogue").GetComponent<Canvas>();
        dialogue = customerDia.GetComponentInChildren<TextMeshProUGUI>();
        customerDia.sortingOrder = -2;
        //devilSprite = customerDia.GetComponentInChildren<devilSprite>().gameObject.GetComponent<Image>();
        //devilSprite.gameObject.SetActive(false);


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
                    Debug.Log(hub.plates[i].diag[0]);
                    plate currentplate = hub.plates[i];
                    hub.plates[i].delivered();
                    hub.plates.RemoveAt(i);
                    StartCoroutine(Deliver(currentplate));
                }
            }
        }
    }
   
    IEnumerator Deliver(plate i)
    {
        
        customerDia.sortingOrder = 1;
        dialogue.color = Color.red;
        hub.resetPos(false);
        //devilSprite.gameObject.SetActive(true);
        hub.canMove(false);
        
        dialogue.text = "";
        foreach (char c in i.diag[0])
        {
            dialogue.text += c;
            yield return new WaitForSeconds(.03f);
        }
        yield return new WaitForSeconds(2f);
        dialogue.text = "";
        dialogue.color = Color.white;
        dialogue.text = "";
        foreach (char c in i.diag[1])
        {
            dialogue.text += c;
            yield return new WaitForSeconds(.03f);
        }
        yield return new WaitForSeconds(2f);
        hub.canMove(true);
        customerDia.sortingOrder = -2;
        //devilSprite.gameObject.SetActive(false);
        hub.resetPos(true);
        yield return null;
    }
    IEnumerator Typewriter(string text, TextMeshProUGUI tmp)
    {
        tmp.text = "";
        foreach (char c in text)
        {
            tmp.text = tmp.text + c;
            yield return new WaitForSeconds(.03f);
        }
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
