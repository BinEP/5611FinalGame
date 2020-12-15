using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject YouLoseMenuiUI;
    public GameObject YouWinMenuUI;
    public string nextLevelName;

    void Start() {
        Time.timeScale = 1f;
	}

    public void YouLose()
    {
        if (!gameIsPaused)
        {
            Pause();
        }
    }

    public void YouWin()
    {
        if (!gameIsPaused)
        {
            Win();
        }
    }

    void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;

        YouLoseMenuiUI.SetActive(true);
    }

    void Win()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;

        YouWinMenuUI.SetActive(true);
    }

    public void Restart()
    {
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }


    void toInvoke() {
        Debug.Log("restart");
        GameObject button = GameObject.FindGameObjectsWithTag("DrawButton")[0];
        Debug.Log("restart 2");
        Debug.Log("restart 3");
    }

    public void Next()
    {
        gameIsPaused = false;
        //GameObject button = GameObject.FindGameObjectsWithTag("DrawButton")[0];
        //button.GetComponent<drawCards>().Setup();
        SceneManager.LoadScene(nextLevelName);
        Time.timeScale = 1f;
    }
}
