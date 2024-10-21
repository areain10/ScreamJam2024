using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum playerAnimStates { idle,walking,trayidle,traywalking}
public class playerAnimation : MonoBehaviour
{
    public Sprite[] walkingSprite;
    public Sprite[] traySprites;
    float elapsedTime;
    public float delay;
    Rigidbody2D rb;
    public playerAnimStates state;
    Sprite sprite;
    int currentSprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>().sprite;
        rb = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody2D>();
        elapsedTime = 0f;
        currentSprite = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        switch (state)
        {
            case playerAnimStates.idle:
                GetComponent<SpriteRenderer>().sprite = walkingSprite[1];
                elapsedTime = 0f; break;
            case playerAnimStates.walking:
                if(elapsedTime > delay)
                {
                    GetComponent<SpriteRenderer>().sprite = walkingSprite[currentSprite];
                    currentSprite = (currentSprite + 1) % 3;
                    elapsedTime = 0;
                    
                }

                break;
            case playerAnimStates.trayidle:
                GetComponent<SpriteRenderer>().sprite = traySprites[1];
                elapsedTime = 0f; break;
            case playerAnimStates.traywalking:
                if (elapsedTime > delay)
                {
                    GetComponent<SpriteRenderer>().sprite = traySprites[currentSprite];
                    currentSprite = (currentSprite + 1) % 3;
                    elapsedTime = 0;

                }
                break;
        }
        elapsedTime += Time.deltaTime;
    }
    public void changeState(playerAnimStates stat)
    {
        state = stat;
    }
}
