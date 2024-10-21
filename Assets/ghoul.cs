using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public enum ghoulState { spawning,despawning,normal,invisible}
public class ghoul : MonoBehaviour
{
    ghoulState state;
    GameObject player;
    [SerializeField] float speed;
    [SerializeField] float timeInvis;
    [SerializeField] float timeNormal;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = 0f;
        player = GameObject.FindGameObjectWithTag("Player");
        state = ghoulState.normal;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(state.ToString());
        switch (state)
        {
            case ghoulState.spawning:
                elapsedTime = 0;
                StartCoroutine(spawnDespawn(true));break;
            case ghoulState.despawning:
                StartCoroutine(spawnDespawn(false));
                elapsedTime = 0;
                break; 
            case ghoulState.normal:
                transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime); 
                if(elapsedTime > timeNormal)
                {
                    
                    state = ghoulState.despawning;
                    
                    elapsedTime = 0;
                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                break; 
            
            case ghoulState.invisible:
                if (elapsedTime > timeInvis)
                {
                    StartCoroutine(spawnDespawn(true));
                    state = ghoulState.spawning;
                    
                    elapsedTime = 0;

                }
                else
                {
                    elapsedTime += Time.deltaTime;
                }
                break;
        }
    }   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(state == ghoulState.normal && collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<playerHub>().reverseInput(3f);
            state = ghoulState.despawning;
            elapsedTime = 0;
            
        }
        
    }
    
    IEnumerator spawnDespawn(bool spawn)
    {
        Debug.Log("SpawnDespawnCalled");
        if (!spawn)
        {
            float a = GetComponent<SpriteRenderer>().color.a;
            Color tmp = GetComponent<SpriteRenderer>().color;
            while (GetComponent<SpriteRenderer>().color.a > 0)
            {
                Debug.Log(GetComponent<SpriteRenderer>().color.a);
                a -= 0.05f;
                tmp.a = a;
                GetComponent<SpriteRenderer>().color = tmp;
                yield return new WaitForSeconds(0.05f);
            }
            state = ghoulState.invisible;
        }
        else
        {
            float a = GetComponent<SpriteRenderer>().color.a;
            Color tmp = GetComponent<SpriteRenderer>().color;
            while (GetComponent<SpriteRenderer>().color.a < 1)
            {
                Debug.Log(GetComponent<SpriteRenderer>().color.a);                                                                                                                                  
                a += 0.05f;
                tmp.a = a;
                GetComponent<SpriteRenderer>().color = tmp;
                yield return new WaitForSeconds(0.05f);
            }
            state = ghoulState.normal;
        }
        
    }
}
