using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batspawner : MonoBehaviour
{
    public GameObject bat;
    List<GameObject> bats;
    private void Start()
    {
        bats = new List<GameObject>();
    }
    public void spawnBats(int num)
    {
        for (int i = 0; i <num; i++)
        {
            var go = Instantiate(bat);
            bats.Add(go);
        }
    }

    public void killBats()
    {
        for (int i = 0;i < bats.Count;i++)
        {
            Destroy(bats[i]);
        }
        bats = new List<GameObject> ();
    }
}
