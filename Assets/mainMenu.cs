using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class mainMenu : MonoBehaviour
{
    [SerializeField] GameObject buttons;
    [SerializeField] GameObject options;
    AudioSource[] audioSources;

    private void Awake()
    {
        options.SetActive(false);
        buttons.SetActive(true);
        DontDestroyOnLoad(this);
        

    }
    public void Options()
    {
        options.SetActive(true);
        buttons.SetActive(false);
    }
    public void exitOptions()
    {
        options.SetActive(false);
        buttons.SetActive(true);
    }
    private void Start()
    {
        
        audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
    }
    public void setVolume()
    {
        float i = options.GetComponentInChildren<Slider>().value;
        for (int j=0; j < audioSources.Length; j++)
        {
            audioSources[j].volume = i;
        }
    }
    public void clickedPlay()
    {
        SceneManager.LoadScene(1);
    }
    public void clickedQuit()
    {
        Application.Quit();
    }
    
}
