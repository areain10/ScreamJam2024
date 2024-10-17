using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class plateSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]GameObject platePrefab;
    playerHub hub;
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
        
        for (int i = 0 ; i < data.Count; i++)
        {
            string[] dia = { "", "" };
            for (int d = 7; d < data.Count; d++)
            {
                dia[d - 7] = data[i][d];
            }
            var go = Instantiate(platePrefab, new Vector2(gameObject.transform.position.x + Random.Range(-2f, 2f), gameObject.transform.position.y - 3), gameObject.transform.rotation);
            go.GetComponent<plate>().setupPlate(data[i][1], data[i][2], data[i][3], data[i][4], data[i][5], dia);
            hub.plates.Add(go.GetComponent<plate>());
            yield return new WaitForSeconds(1f);
        }
        
        yield return null;
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
