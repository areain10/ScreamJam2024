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
    AudioSource audios;
    cutscene cut;
    int currentCourse;
    int roundLife;
    gameStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        currentCourse = 1;
        cut = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<cutscene>();
        audios = GetComponent<AudioSource>();
        audios.clip = soundtrack[currentCourse - 1];
        audios.Play(0);
        resetlife();
        updateLiveCounter();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void loseLife()
    {
        roundLife -= 1;
        updateLiveCounter();
        if(roundLife <= 0 )
        {
            currentState = gameStates.Lose;
            StartCoroutine(gameOver());
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
        currentCourse = nextCourse;
        StartCoroutine(cut.changeArt(currentCourse-1,duration));
        switch (nextCourse)
        {
            case 2:
                setupMain();
                
                break;
            case 3:
                GetComponentInChildren<batspawner>().killBats();
                setupDessert();
                break;
            case 4:
                setupAfterHours();
                break;

        }
        audios.clip = soundtrack[currentCourse - 1];
        audios.Play(0);
        
        yield return new WaitForSeconds(5f);
        
    }
    public void setupMain()
    {
        GetComponentInChildren<batspawner>().spawnBats(6);
    }
    public void setupDessert()
    {
        GetComponentInChildren<batspawner>().killBats();
        GetComponentInChildren<batspawner>().spawnBats(1);
    }
    public void setupAfterHours()
    {
        GetComponentInChildren<batspawner>().killBats();
        GetComponentInChildren<batspawner>().spawnBats(4);
    }
    void updateLiveCounter()
    {
        livesCounter.text = "";
        for (int i = 0; i < roundLife; i++)
        {
            livesCounter.text += " |";
        }
        
    }
}
