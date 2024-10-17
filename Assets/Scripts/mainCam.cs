using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mainCam : MonoBehaviour
{
    GameObject playerGO;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.visible = false;
        playerGO = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.position = playerGO.transform.position + new Vector3(0,0,-10.5f);
    }
}
