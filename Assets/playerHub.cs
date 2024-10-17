using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHub : MonoBehaviour
{
    public List<plate> plates;
    public void canMove(bool i)
    {
        gameObject.GetComponent<playerMovement>().canMove = i;
    }
    public void setPlates(List<GameObject> platesGO)
    {
        plates.Clear();
        for (int i = 0; i < platesGO.Count; i++)
        {
            plates.Add(platesGO[i].GetComponent<plate>());
        }
    }
}
