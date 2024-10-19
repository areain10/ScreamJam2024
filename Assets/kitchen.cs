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
    gameManager manager;

    // Start is called before the first frame update
    void Start()
    {
        round = -1;
        menu = new List<List<List<String>>>();
        readMenu();
        interactable = false;
        hub = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHub>();
        convWindow = GameObject.FindGameObjectWithTag("ConversationWindow");
        manager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<gameManager>();
        convWindow.SetActive(false);
        
    }
    void Update()
    {
        if (interactable)
        {
            checkForInteract();
        }
    }
    IEnumerator spawnPlate()
    {
        manager.resetlife();
        round++;
        /*switch (round)
        {
            case 4:
                StartCoroutine(manager.changeCourse(2));
                yield return new WaitForSeconds(5);
                break;
            case 10:
                StartCoroutine(manager.changeCourse(3));
                yield return new WaitForSeconds(5);
                break;
            case 15:
                StartCoroutine(manager.changeCourse(4));
                yield return new WaitForSeconds(5);
                break;
            case 20:
                //end game
                break;
            

        }*/
        switch (round)
        {
            case 1:
                StartCoroutine(manager.changeCourse(2,10));
                yield return new WaitForSeconds(5);
                break;
            case 5:
                StartCoroutine(manager.changeCourse(3,5));
                yield return new WaitForSeconds(5);
                break;
            case 8:
                StartCoroutine(manager.changeCourse(4, 5));
                yield return new WaitForSeconds(5);
                break;
            case 20:
                //end game
                break;


        }
        Debug.Log("Interact");
        hub.canMove(true);
        convWindow.SetActive(true);
        plateSpawner.spawnPlate(menu[round], round);
        //hub.setPlates(menu[round]);
        interactable = false;
        yield return null;
    }
    void checkForInteract()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            if(hub.plates.Count == 0)
            {
               StartCoroutine(spawnPlate());
            }
            else 
            {
                List<List<string>> tmp = new List<List<string>>();
                bool active = true;
                for (int i = 0; i < hub.plates.Count; i++)
                {
                    
                    if (hub.plates[i].state == plateState.fallen)
                    {
                        
                        for (int k = 0; k < menu[round].Count; k++)
                        {
                            //Debug.Log(menu[round][k][1] + " " + hub.plates[i].id);
                            if (menu[round][k][1] == hub.plates[i].id)
                            {
                                tmp.Add(menu[round][k]);
                            }
                        }
                        
                        
                    }
                }
                convWindow.SetActive(true);
                Debug.Log("Spawning " + tmp.Count + " plates");
                plateSpawner.spawnPlate(tmp, round);
            }
            

        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == "Player" )
        {
            hub = collision.gameObject.GetComponent<playerHub>();
            interactable = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            if(convWindow != null)
            {
                convWindow.SetActive(false);
            }
            
            //hub = null;
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
            string strTmp = "";
            for (int j = 0; j < tmp.Count; j++)
            {
                strTmp += tmp[j].ToString() + " __ ";
            }
            Debug.Log(strTmp);
            menu[roundCounter].Add(tmp);
            //go.SetActive(false);



        }
        // Update is called once per frame
    }
}
