using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deleter : MonoBehaviour
{
    [SerializeField] AudioClip[] platefallingaudio;
    [SerializeField] CameraShake camShake;
    AudioSource audio;
    // Start is called before the first frame update
    gameManager manager;
    void Start()
    {
        audio = GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Gamemanager").GetComponent<gameManager>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Plate")
        {
            audio.clip = platefallingaudio[Random.Range(0, platefallingaudio.Length)];
            
            StartCoroutine(camShake.Shake(0.3f,1));
            audio.Play(0);
            manager.loseLife();
            StartCoroutine(collision.gameObject.GetComponent<plate>().plateFallen());
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
