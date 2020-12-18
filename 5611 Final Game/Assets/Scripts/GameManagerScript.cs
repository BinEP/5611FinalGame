using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManagerScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject EndGameMenuUI;
    public string nextLevelName;

    void Start()
    {
        GlobalVars.Instance.switchDim(GlobalVars.Instance.currentDimensionIter);
        Time.timeScale = 1f;
	}

    public void YouLose()
    {
        if (!gameIsPaused)
        {
            Pause();
        }
    }

    void Pause()
    {
        gameIsPaused = true;
        Time.timeScale = 0f;

        EndGameMenuUI.SetActive(true);
    }

    public void Restart()
    {
        GlobalVars.Instance.switchDim(GlobalVars.Instance.currentDimensionIter);

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

    void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1) || Input.GetKey(KeyCode.Keypad1))
        {
            GlobalVars.Instance.switchDim(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            GlobalVars.Instance.switchDim(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            GlobalVars.Instance.switchDim(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            GlobalVars.Instance.switchDim(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            GlobalVars.Instance.switchDim(5);
        }
    }

}
