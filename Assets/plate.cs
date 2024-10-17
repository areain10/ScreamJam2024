using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plate : MonoBehaviour
{
    public string id;
    public string name;
    public string table;
    public string customer;
    public string cusID;
    public string[] diag;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject setupPlate(string i, string nam, string tabl, string custome, string cusI, string[] dialoague)
    {
        id = i; name = nam; table = tabl; customer = custome; cusID = cusI; diag = dialoague;
        return gameObject;
    }

    public void delivered()
    {
        Destroy(gameObject);
    }
}
