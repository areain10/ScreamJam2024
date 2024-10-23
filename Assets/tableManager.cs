using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class tableManager : MonoBehaviour
{

    public Sprite[] sprites;
    public List<Sprite> portraits;
    

    // Start is called before the first frame update
    void Start()
    {
        fillUpTable();
    }
    public List<Sprite> getPort() { return portraits; }
    public List<Sprite>[] getSprite()
    {
        List<Sprite>[] mtList = new List<Sprite>[8];
        List<Sprite> tmp;
        for (int i = 0; i < 8;i++)
        {
            tmp = new List<Sprite>();
            for (int j = 0; j < sprites.Length; j++)
            {
                //Debug.Log(sprites[j].name[0] + " " +((char)(i + 1)));
                if (sprites[j].name[0].ToString() == (i + 1).ToString())
                {
                    tmp.Add(sprites[j]);
                }
            }
            mtList[i] = tmp;
        }
        return mtList;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void fillUpTable()
    {
        var dataset = Resources.Load<TextAsset>("tables");
        string[] dataLine = dataset.text.Split('\n');
        var tablestmp = new List<GameObject>();
        List<string> dataLines = dataLine.ToList<string>();
        dataLines.RemoveAt(0);
        Debug.Log(dataLines);
        for (int i = 0; i < dataLines.Count; i++)
        {
            
            tablestmp.Add(gameObject.transform.GetChild(i).gameObject);
            var data = dataLines[i].Split(',');
            tables currentTables = tablestmp[i].GetComponent<tables>();
            List<string> ids = new List<string>();
            for (int d = 2; d < data.Length; d++)
            {
                if (data[d] != "-")
                {
                    ids.Add(data[d]);
                }
            }
            Debug.Log(data[0] + data[1]);
            currentTables.setUpTable(Convert.ToInt32(data[0]), Convert.ToInt32(data[1]), ids); 

        }


    }
}
