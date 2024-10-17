using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class kitchen : MonoBehaviour
{
    bool interactable;  
    playerHub hub;
    GameObject convWindow;
    int round;
    List<List<List<string>>> menu;
    [SerializeField] GameObject platePrefab;
    [SerializeField] plateSpawner plateSpawner;
    // Start is called before the first frame update
    void Start()
    {
        round = -1;
        menu = new List<List<List<String>>>();
        readMenu();
        interactable = false;
        hub = null;
        convWindow = GameObject.FindGameObjectWithTag("ConversationWindow");
        convWindow.SetActive(false);
        
    }
    void Update()
    {
        if (interactable)
        {
            checkForInteract();
        }
    }
    void checkForInteract()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(hub.plates.Count == 0)
            {
                round++;
                Debug.Log("Interact");
                hub.canMove(true);
                convWindow.SetActive(true);
                plateSpawner.spawnPlate(menu[round],round);
                //hub.setPlates(menu[round]);
                interactable = false;
            }
            

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" && hub == null)
        {
            hub = collision.gameObject.GetComponent<playerHub>();
            interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            convWindow.SetActive(false);
            hub = null;
            interactable = false;
        }
    }
    void readMenu()
    {
        var dataset = Resources.Load<TextAsset>("menu");
        string[] dataLine = dataset.text.Split('\n');
        List<string> dataLines = dataLine.ToList<string>();
        dataLines.RemoveAt(0);
        menu.Clear();
        List<string> tmp = new List<string>();
        int roundCounter = 0;
        for (int i = 0; i < dataLines.Count; i++)
        {
            var data = dataLines[i].Split(',');
            Debug.Log(data.Length);
            string[] dia= { "", "" };
            tmp = data.ToList<string>();
            if (Convert.ToInt32(data[0]) != roundCounter)
            {
                roundCounter = Convert.ToInt32(data[0])-1;
                menu.Add(new List<List<string>>());
            }

            
            //var go = Instantiate(platePrefab, gameObject.transform.position, gameObject.transform.rotation);
            //go.GetComponent<plate>().setupPlate(data[1], data[2], data[3], data[4], data[5], dia);
            //platePrefab.GetComponent<plate>().setupPlate(data[1], data[2], data[3], data[4], data[5], dia);

            menu[roundCounter].Add(tmp);
            //go.SetActive(false);



        }
        // Update is called once per frame
    }
}
