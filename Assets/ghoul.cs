using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Unity.Burst.Intrinsics.X86.Avx;

public enum ghoulState { spawning,despawning,normal,invisible}
public class ghoul : MonoBehaviour
{
    public ghoulState state;
    GameObject player;
    [SerializeField] float speed;
    [SerializeField] float timeInvis;
    [SerializeField] float timeNormal;
    Transform playerTrans;
    float elapsedTime;
    // Start is called before the first frame update
    void Start()
    {
        elapsedTime = Random.Range(0f,timeNormal);
        player = GameObject.FindGameObjectWithTag("Player");
        playerTrans = player.transform;
        //state = ghoulState.normal;
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
            int num;
            num = Random.Range(0, 2);
            if (num == 0)
            {
                num = -1;
            }
            gameObject.transform.position = new Vector2(playerTrans.position.x + (Random.Range(2f, 4f) * num), playerTrans.position.y + (Random.Range(2.5f, 5f) * num));
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
