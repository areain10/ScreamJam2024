using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhoulSpawner : MonoBehaviour
{
    public GameObject ghoul;
    List<GameObject> ghouls;
    Transform playerTrans;
    private void Start()
    {
        playerTrans = GameObject.FindGameObjectWithTag("Player").transform;
        ghouls = new List<GameObject>();
    }
    public void spawnGhoul(int num)
    {
        for (int i = 0; i < num; i++)
        {
            var go = Instantiate(ghoul,new Vector2(playerTrans.position.x + Random.Range(-4,4), playerTrans.position.y + Random.Range(-5,5)), new Quaternion(0,0,0,0));
            if(Random.Range(0, 1) == 0)
            {
                go.GetComponent<ghoul>().state = ghoulState.normal;
            }
            else
            {
                go.GetComponent<ghoul>().state = ghoulState.invisible;
            }
            ghouls.Add(go);
        }
    }

    public void killGhouls()
    {
        for (int i = 0; i < ghouls.Count; i++)
        {
            Destroy(ghouls[i]);
        }
        ghouls = new List<GameObject>();
    }
}
