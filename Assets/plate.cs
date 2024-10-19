using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneTemplate;
using UnityEngine;

public enum plateState { active, fallen, delivered}
public class plate : MonoBehaviour
{
    playerHub hub;
    public string id;
    public string name;
    public string table;
    public string customer;
    public string cusID;
    public string[] diag;
    public plateState state;
    // Start is called before the first frame update
    void Start()
    {
        hub = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHub>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public GameObject setupPlate(string i, string nam, string tabl, string custome, string cusI, string[] dialoague)
    {
        id = i; name = nam; table = tabl; customer = custome; cusID = cusI; diag = dialoague;
        state = plateState.active;
        return gameObject;
    }

    public void delivered()
    {
        state = plateState.delivered;
        Destroy(gameObject);
    }
    public IEnumerator plateFallen()
    {
        state = plateState.fallen;
        hub.canMove(false);
        yield return new WaitForSeconds(2f);
        hub.canMove(true);
        yield return null;
    }
}
