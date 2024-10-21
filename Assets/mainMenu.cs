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
    List<float> volumes;
    options option;
    private void Awake()
    {
        options.SetActive(false);
        buttons.SetActive(true);
        DontDestroyOnLoad(this);
        SceneManager.sceneLoaded += OnSceneLoaded;


    }
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);
        float i = options.GetComponentInChildren<Slider>().value;
        volumes = new List<float>();
        for (int j = 0; j < audioSources.Length; j++)
        {
            volumes.Add(audioSources[j].volume);
            audioSources[j].volume = volumes[j]*i ;
        }
    }
    public void Options()
    {
        options.SetActive(true);
        try
        {
            buttons.SetActive(false);
        }
        catch
        {

        }
    }
    public void exitOptions()
    {
        options.SetActive(false);
        try
        {
            buttons.SetActive(true);
        }
        catch
        {

        }
        
    }
    private void Start()
    {
        option = FindFirstObjectByType<options>();
        
        audioSources = GameObject.FindObjectsByType<AudioSource>(FindObjectsSortMode.None);

    }
    public void setVolume()
    {
        float i = options.GetComponentInChildren<Slider>().value;
        for (int j=0; j < audioSources.Length; j++)
        {
            audioSources[j].volume = volumes[j] * i;
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

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) 
        {
            options.SetActive(true);
        }
    }

}
