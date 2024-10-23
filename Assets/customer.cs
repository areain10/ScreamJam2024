using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using static Unity.Burst.Intrinsics.X86.Avx;

public class customer : MonoBehaviour
{
    [SerializeField] public string customerID;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI dialogue;
    [SerializeField] float idleanimDelay;
    int currentSprite;
    float elapsedTime;
    public List<Sprite> sprites;
    Canvas customerDia;
    public string customerName;
    bool canInteract;
    playerHub hub;
    public Sprite demonSprite;
    public UnityEngine.UI.Image demonImage;

    // Start is called before the first frame update
    void Start()
    {
        //demonSprite = null;
        currentSprite = 0;
        elapsedTime = 0f;
        nameText.text = customerID;
        customerDia = GameObject.FindGameObjectWithTag("CustomerDialogue").GetComponent<Canvas>();
        dialogue = customerDia.GetComponentInChildren<TextMeshProUGUI>();
        customerDia.sortingOrder = -2;
        //devilSprite = customerDia.GetComponentInChildren<devilSprite>().gameObject.GetComponent<Image>();
        //devilSprite.gameObject.SetActive(false);
        demonImage = GameObject.FindGameObjectWithTag("DemonSprite").GetComponent<UnityEngine.UI.Image>();


    }
    public void setSprite(List<Sprite> sprite)
    {
        
        sprites = sprite;
        GetComponentInChildren<SpriteRenderer>().sprite = sprites[currentSprite];
    }
    public void setPortrait(Sprite por)
    {
        demonSprite = por;
        //Debug.Log(por.name + " " + demonImage.sprite.name);
    }
    // Update is called once per frame
    void Update()
    {
        if(sprites.Count > 1) 
        {
            animationIdle();
        }
        
        if (canInteract && Input.GetKeyUp(KeyCode.E))
        {
            checkForPlate();
        }
    }
    void animationIdle()
    {
        if(elapsedTime > idleanimDelay)
        {
            currentSprite = (currentSprite + 1) % sprites.Count;
            GetComponentInChildren<SpriteRenderer>().sprite = sprites[currentSprite];
            elapsedTime = 0f;
        }
        elapsedTime += Time.deltaTime;
    }
    bool checkForPlate()
    {
        if(hub != null)
        {
            for(int i = 0;i < hub.plates.Count;i++)
            {
                Debug.Log(hub.plates[i].cusID + ' '+ customerID);
                if(hub.plates[i].cusID == customerID && hub.plates[i].state != plateState.fallen)
                {
                    Debug.Log(hub.plates[i].name + ' '+ hub.plates[i].cusID+' '+ hub.plates[i].diag[0]);
                    
                    StartCoroutine(Deliver(hub.plates[i]));
                    hub.plates[i].delivered();
                    hub.plates.RemoveAt(i);
                    return true;
                    
                }
            }
        }
        return false;
    }
   
    IEnumerator Deliver(plate i)
    {
        demonImage = GameObject.FindGameObjectWithTag("DemonSprite").GetComponent<UnityEngine.UI.Image>();
        demonImage.overrideSprite = demonSprite;
        
        customerDia.sortingOrder = 1;
        dialogue.color = Color.red;
        hub.resetPos(false);
        hub.trayDissapear(false);
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
        hub.trayDissapear(true);
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
