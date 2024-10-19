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
    cutscene cut;
    int currentCourse;
    int roundLife;
    gameStates currentState;
    // Start is called before the first frame update
    void Start()
    {
        cut = GameObject.FindGameObjectWithTag("Cutscene").GetComponent<cutscene>();
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
        SceneManager.LoadScene(1);
        //ChangeScene
    }

    public IEnumerator changeCourse(int nextCourse)
    {
        currentCourse = nextCourse;
        StartCoroutine(cut.changeArt(currentCourse-1));
        yield return new WaitForSeconds(1f);
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
