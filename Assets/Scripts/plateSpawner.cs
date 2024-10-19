using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


public class plateSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject platePrefab;
    playerHub hub;
    string kitchDia;
    [SerializeField] TextMeshProUGUI textMeshProUGUI;
    void Start()
    {
        hub = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHub>();
    }

    public void spawnPlate(List<List<string>> plates, int round)
    {
        StartCoroutine(spawnPlates(plates,round));
    }
    IEnumerator spawnPlates(List<List<string>> data,int round)
    {
        hub.canMove(false);
        hub.plates.Clear();
        
        for (int i = 0 ; i < data.Count; i++)
        {
            string[] dia = { "", "" };
            
            dia[0] = data[i][6];
            dia[1] = data[i][7];
            Debug.Log(i + ' ' + dia[0] + ' ' + dia[1]);
            var go = Instantiate(platePrefab, new Vector2(gameObject.transform.position.x + Random.Range(-2f, 2f), gameObject.transform.position.y - 3), gameObject.transform.rotation);
            go.GetComponent<plate>().setupPlate(data[i][1], data[i][2], data[i][3], data[i][4], data[i][5], dia);
            hub.plates.Add(go.GetComponent<plate>());
            kitchDia = "";
            kitchDia = "This is a " + data[i][2] + " for " + data[i][5] + " at Table " + data[i][3]; 
            StartCoroutine(Typewriter(kitchDia, textMeshProUGUI));
            yield return new WaitForSeconds((kitchDia.Length * 0.03f) + 1f);
        }
        hub.canMove(true); 
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
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
}
