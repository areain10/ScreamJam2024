using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum gameStates {Playing, ChangingCourses, Win, Lose}
public enum courses {appetizer, main, dessert, afterHourse }
public class gameManager : MonoBehaviour
{
    [SerializeField] int maxRoundLife;
    [SerializeField] TextMeshProUGUI livesCounter;
    [SerializeField] AudioClip[] soundtrack;
    AudioSource[] audios;
    List<float> volumes = new List<float>();
    cutscene cut;
    int currentCourse;
    int roundLife;
    public bool changingCourse;
    gameStates currentState;
    playerHub playerHub;
    // Start is called before the first frame update
    void Start()
    {
        playerHub = GameObject.FindGameObjectWithTag("Player").GetComponent<playerHub>();
        changingCourse = false;
        currentCourse = 1;
        cut = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<cutscene>();
        audios = GetComponents<AudioSource>();
        audios[0].clip = soundtrack[currentCourse - 1];
        audios[0].Play(0);
        resetlife();
        updateLiveCounter();
        for (int i = 0; i < audios.Length; i++)
        {
            volumes.Add(audios[i].volume);
            
        }
        StartCoroutine(changeCourse(currentCourse, 10f));
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loseLife()
    {
        if(playerHub.lostLifeThisRound == false)
        {
            roundLife -= 1;
            updateLiveCounter();
            if (roundLife <= 0)
            {
                currentState = gameStates.Lose;
                StartCoroutine(gameOver());
            }
            playerHub.lostLifeThisRound = true;
        }
        
    }

    public void resetlife()
    {
        
        roundLife = maxRoundLife;
        updateLiveCounter();
    }

    IEnumerator gameOver()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
        //ChangeScene
    }
    public IEnumerator gameCompleted()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(3);
    }

    public IEnumerator changeCourse(int nextCourse, float duration)
    {
        playerHub.canMove(false);
        changingCourse = true;
        currentCourse = nextCourse;
        StartCoroutine(cut.changeArt(nextCourse-1,duration));

        audios[0].clip = soundtrack[currentCourse - 1];
        audios[0].Play(0);

        yield return new WaitForSeconds(duration);
        changingCourse = false;
        playerHub.canMove(true);

    }
    public IEnumerator courseSetup(int course)
    {
        yield return new WaitForSeconds(1.5f);
        switch (course)
        {
            case 2:

                setupMain();

                break;
            case 3:
                
                setupDessert();
                break;
            case 4:
                setupAfterHours();
                break;
        }
        yield return null;
    }
    public void setupMain()
    {
        GetComponentInChildren<batspawner>().spawnBats(6);
    }
    public void setupDessert()
    {
        GetComponentInChildren<batspawner>().killBats();
        GetComponentInChildren<GhoulSpawner>().spawnGhoul(2);
    }
    public void setupAfterHours()
    {
        GetComponentInChildren<batspawner>().killBats();
        GetComponentInChildren<GhoulSpawner>().killGhouls();
        GetComponentInChildren<batspawner>().spawnBats(4);
        GetComponentInChildren<GhoulSpawner>().spawnGhoul(4);
    }
    void updateLiveCounter()
    {
        livesCounter.text = "";
        for (int i = 0; i < roundLife; i++)
        {
            livesCounter.text += " |";
        }
        
    }
    public IEnumerator quiet(float duration, float multiplier)
    {
        
        for (int i = 0; i < audios.Length; i++)
        {
            volumes.Add(audios[i].volume);
            audios[i].volume *= multiplier;
        }
        yield return new WaitForSeconds(duration);
        for (int i = 0; i < audios.Length; i++)
        {
            
            audios[i].volume = volumes[i];
        }
    }
}
