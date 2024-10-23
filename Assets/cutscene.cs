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
        GetComponentInChildren<Image>().color = new Color(255, 255, 255, 0);
    }
    public IEnumerator changeArt(int artId,float duration)
    {
        Debug.Log(artId);
        try { im.sprite = art[artId]; }
        catch { GetComponentInChildren<Image>().sprite = art[artId]; }
        StartCoroutine(SpriteFade(GetComponentInChildren<Image>(), 255,duration/4));
        

        yield return new WaitForSeconds(duration/2);
        StartCoroutine(SpriteFade(GetComponentInChildren<Image>(), 0, duration / 4));

        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public IEnumerator SpriteFade(
Image sr,
float endValue,
float duration)
    {
        
        
        if (endValue > 0) {
            sr.color = new Color(255, 255, 255, 0);
            for (float i = 0; i <= 1; i += Time.deltaTime)
            {
                // set color with i as alpha
                sr.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        else {
            sr.color = new Color(255, 255, 255, 255);
            for (float i = 1; i >= 0; i -= Time.deltaTime)
            {
                // set color with i as alpha
                sr.color = new Color(1, 1, 1, i);
                yield return null;
            }
        }
        sr.color = new Color(255,255,255,endValue);
    }
}
