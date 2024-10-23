using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;

public class kitchen : MonoBehaviour
{
    bool interactable;  
    playerHub hub;
    GameObject convWindow;
    int round;
    int course;
    List<List<List<string>>> menu;
    [SerializeField] public GameObject[] platePrefab;
    [SerializeField] plateSpawner plateSpawner;
    gameManager manager;
    float courseChangeDuration;

    // Start is called before the first frame update
    void Start()
    {
        courseChangeDuration = 10f;
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
       
        switch (round)
        {
            case 5:
                course = 2;
                yield return StartCoroutine(manager.changeCourse(course, courseChangeDuration));
                break;
            case 10:
                course = 3;
                yield return StartCoroutine(manager.changeCourse(course, courseChangeDuration));
                break;
            case 15:
                course = 4;
                //yield return StartCoroutine(manager.changeCourse(course, courseChangeDuration));
                StartCoroutine(manager.gameCompleted());
                break;
            case 20:
                StartCoroutine(manager.gameCompleted());
                break;



        }
        hub.lostLifeThisRound = false;
        Debug.Log("Interact");
        hub.canMove(true);
        convWindow.SetActive(true);
        plateSpawner.spawnPlate(menu[round], round);
        //hub.setPlates(menu[round]);
        interactable = true;
        yield return new WaitForSeconds(courseChangeDuration/5);
        StartCoroutine(manager.courseSetup(course));
    }
    void checkForInteract()
    {
        if (Input.GetKeyUp(KeyCode.E))
        {
            GetComponent<AudioSource>().Play();
            if(hub.plates.Count == 0)
            {
               StartCoroutine(spawnPlate());
            }
            else 
            {
                hub.lostLifeThisRound = true;
                
                
                hub.resetPos(false);
                List<List<string>> tmp = new List<List<string>>();
                //List<string> tmp2 = new List<string>();
                for (int i = 0; i < hub.plates.Count; i++)
                {
                    List<string> tmp2 = new List<string>();
                    if (hub.plates[i].state == plateState.fallen)
                    {
                        
                        for (int k = 0; k < menu[round].Count; k++)
                        {
                            //Debug.Log(menu[round][k][1] + " " + hub.plates[i].id);
                            if (menu[round][k][1] == hub.plates[i].id && !tmp2.Contains(menu[round][k][1]))
                            {
                                //Debug.Log("Adding this plate: " + menu[round][k][1] + " " + tmp.Count);
                                tmp.Add(menu[round][k]);
                                tmp2.Add(menu[round][k][1]);
                            }
                        }
                        
                        
                    }
                }
                convWindow.SetActive(true);
                //Debug.Log("Spawning " + tmp.Count + " plates, Hub has " + hub.plates.Count + " plates, Round "+round + ", With "+ menu[round].Count);
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
            if (int.Parse(data[0]) != roundCounter)
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
