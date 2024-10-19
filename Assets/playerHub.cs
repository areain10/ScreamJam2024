using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHub : MonoBehaviour
{
    balancingInput balIn;
    public List<plate> plates;
    private void Start()
    {
        balIn = GameObject.FindGameObjectWithTag("Tray").GetComponent<balancingInput>();
    }
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
    public void resetPos(bool move)
    {
        if(balIn != null)
        {
            balIn.resetRot(move);
        }
        
    }
}
