using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerSound : MonoBehaviour
{
    AudioSource source;
    [Header("Movement")]
    [SerializeField] AudioClip[] walking;
    // Start is called before the first frame update
    void Start()
    {
        source = GetComponent<AudioSource>();   
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void playOnLoop(AudioClip[] clips) 
    {
        GetComponent<AudioSource>().clip = clips[Random.Range(0, clips.Length)];
        GetComponent<AudioSource>().loop = true;
        GetComponent<AudioSource>().Play(0);
    }
    public void stopSound()
    {

    }
}
