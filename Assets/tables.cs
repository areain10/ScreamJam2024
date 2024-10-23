using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class tables : MonoBehaviour
{
    int tabNum;
    int numOfCus;
    List<string> customerIDs;
    [SerializeField] GameObject customerPrefab;
    private List<Sprite>[] interactableSprites;
    public Sprite[] phSprites;
    List<Sprite> tmp;
    public List<Sprite> portraits;
    // Start is called before the first frame update
    void Start()
    {
        interactableSprites = GameObject.FindGameObjectWithTag("TableManager").GetComponent<tableManager>().getSprite();
        //for(int i = 0; i < interactableSprites.Length; i++) { Debug.Log(interactableSprites[i].Count); }

    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setUpTable(int tableNum, int numOfCustomer,List<string> customerID)
    {
        tmp = new List<Sprite>();
        tabNum = tableNum;
        numOfCus = numOfCustomer;
        customerIDs = customerID;
        interactableSprites = GameObject.FindGameObjectWithTag("TableManager").GetComponent<tableManager>().getSprite();
        portraits = GameObject.FindGameObjectWithTag("TableManager").GetComponent<tableManager>().getPort();
        //Debug.Log("num of portraits" + portraits.Count);
        //Debug.Log("Set up table num" + tableNum + numOfCus + customerIDs.ToString());

        List<GameObject> spawnLocations = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.GetChild(0).transform.childCount; i++)
        {
            spawnLocations.Add(gameObject.transform.GetChild(0).transform.GetChild(i).gameObject);
            var go = Instantiate(customerPrefab, spawnLocations[i].transform.position, spawnLocations[i].transform.rotation);
           
            
            //if(go != null && customerIDs[i] != null) { go.GetComponent<customer>().customerID = customerIDs[i]; } else { go.GetComponent<customer>().customerID = "000"; }
            try
            {
                
                go.GetComponent<customer>().customerID = customerIDs[i];
                tmp.Clear();
                
                tmp.Add(phSprites[i]);
                go.GetComponent<customer>().setSprite(tmp);
                
                

            }
            catch
            {

                go.GetComponent<customer>().customerID = "000";
            }
            bool found = false;
            for (int j = 0; j < interactableSprites.Length; j++)
            {
                try
                {
                    if (interactableSprites[j][0].name.Substring(0, 3) == customerIDs[0])
                    {

                        go.GetComponent<customer>().sprites = interactableSprites[j];
                        found = true;
                    }
                }
                catch
                {
                    Debug.Log("Interactable "+ interactableSprites[0].Count);
                }
                
            }
            for (int j = 0; j < portraits.Count; j++)
            {
                //Debug.Log(portraits.Count + " " + customerIDs.Count);
                try
                {
                    if (portraits[j].name == customerIDs[i])
                    {
                        Debug.Log(portraits[j].name + " " + customerIDs[i]);
                        go.GetComponent<customer>().setPortrait(portraits[j]);
                        go.GetComponent<customer>().demonSprite = portraits[j];


                    }
                }
                catch
                {

                }
                
            }
            //Debug.Log(i + " " + phSprites[i].name + " " + go.GetComponent<customer>().sprites[0].name);
            if (!found)
            {
                
            }

        }
    }
}
