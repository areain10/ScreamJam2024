using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tables : MonoBehaviour
{
    int tabNum;
    int numOfCus;
    List<string> customerIDs;
    [SerializeField] GameObject customerPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void setUpTable(int tableNum, int numOfCustomer,List<string> customerID)
    {
        tabNum = tableNum;
        numOfCus = numOfCustomer;
        customerIDs = customerID;
        Debug.Log("Set up table num" + tableNum + numOfCus + customerIDs.ToString());

        List<GameObject> spawnLocations = new List<GameObject>();
        for(int i = 0; i < gameObject.transform.GetChild(0).transform.childCount; i++)
        {
            spawnLocations.Add(gameObject.transform.GetChild(0).transform.GetChild(i).gameObject);
            var go = Instantiate(customerPrefab, spawnLocations[i].transform.position, spawnLocations[i].transform.rotation);
            //if(go != null && customerIDs[i] != null) { go.GetComponent<customer>().customerID = customerIDs[i]; } else { go.GetComponent<customer>().customerID = "000"; }
            try
            {
                go.GetComponent<customer>().customerID = customerIDs[i];
            }
            catch
            {
                go.GetComponent<customer>().customerID = "000";
            }
            
        }
    }
}
