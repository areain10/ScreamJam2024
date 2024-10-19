using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class cutscene : MonoBehaviour
{
    [SerializeField] Sprite[] art;
    Image im;
    // Start is called before the first frame update
    void Start()
    {
        im = GetComponentInChildren<Image>();
        im.color = new Color(0, 0, 0, 0);
    }
    public IEnumerator changeArt(int artId)
    {
        im.sprite = art[artId];
        im.color = new Color(255, 255, 255, 255);

        yield return new WaitForSeconds(5f);
        im.color = new Color(0, 0, 0, 0);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
