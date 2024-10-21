using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerHub : MonoBehaviour
{
    balancingInput balIn;
    playerAnimation playerAnim;
    public List<plate> plates;
    Rigidbody2D rb;
    [SerializeField] AudioClip[] walking;
    [SerializeField] AudioClip[] balance;
    AudioSource source;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        playerAnim = GetComponentInChildren<playerAnimation>();
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
    public void shakeTray(float power,float duration, float delay)
    {
        StartCoroutine(balIn.shake(power,duration,delay));
    }
    public void handleAnim()
    {
        if(rb.velocity.magnitude > 0)
        {
            if(GetComponents<AudioSource>()[0].isPlaying == false)
            {
                playOnLoop(walking);
            }
            
            if (plates.Count < 1 || howManyFallen() == plates.Count)
            {
                playerAnim.changeState(playerAnimStates.walking);
                
            }
            else
            {
                if (GetComponents<AudioSource>()[1].isPlaying == false)
                {
                    playOnLoopPlates(balance);
                }
                playerAnim.changeState(playerAnimStates.traywalking);
            }
            
        }
        else
        {
            if (GetComponents<AudioSource>()[0].isPlaying == true)
            {
                GetComponents<AudioSource>()[0].Stop();
            }
            if (GetComponents<AudioSource>()[1].isPlaying == true)
            {
                GetComponents<AudioSource>()[1].Stop();
            }

            if (plates.Count < 1 || howManyFallen() == plates.Count)
            {
                playerAnim.changeState(playerAnimStates.idle);
                
            }
            else
            {
                playerAnim.changeState(playerAnimStates.trayidle);
            }
        }
        //Debug.Log(playerAnim.state);
    }
    public int howManyFallen()
    {
        int count = 0;
        for (int i = 0; i < plates.Count; i++)
        {
            if (plates[i].state == plateState.fallen)
            {
                count++;
            }
        }
        return count;
    }
    public void playOnLoop(AudioClip[] clips)
    {
        GetComponents<AudioSource>()[0].clip = clips[Random.Range(0, clips.Length)];
        GetComponents<AudioSource>()[0].loop = true;
        GetComponents<AudioSource>()[0].Play(0);
    }
    public void playOnLoopPlates(AudioClip[] clips)
    {
        GetComponents<AudioSource>()[1].clip = clips[Random.Range(0, clips.Length)];
        GetComponents<AudioSource>()[1].loop = true;
        GetComponents<AudioSource>()[1].Play(0);
    }

    public void reverseInput(float duration)
    {
        StartCoroutine(balIn.reverseIn(duration));
    }

}
