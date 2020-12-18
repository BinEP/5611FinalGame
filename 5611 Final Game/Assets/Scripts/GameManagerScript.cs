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
        SetDimention(GlobalVars.Instance.currentDimensionIter);
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
        SetDimention(GlobalVars.Instance.currentDimensionIter);

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
            SetDimention(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            SetDimention(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            SetDimention(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            SetDimention(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            SetDimention(5);
        }
    }

    private void SetDimention(int dim)
    {
        GlobalVars.Instance.currentDimensionIter = dim;

        GlobalVars.Instance.Dimension1.SetActive(false);
        GlobalVars.Instance.Dimension2.SetActive(false);
        GlobalVars.Instance.Dimension3.SetActive(false);
        GlobalVars.Instance.Dimension4.SetActive(false);
        GlobalVars.Instance.Dimension5.SetActive(false);

        switch (dim)
        {
            case 1:
                GlobalVars.Instance.Dimension1.SetActive(true);
                break;
            case 2:
                GlobalVars.Instance.Dimension2.SetActive(true);
                break;
            case 3:
                GlobalVars.Instance.Dimension3.SetActive(true);
                break;
            case 4:
                GlobalVars.Instance.Dimension4.SetActive(true);
                break;
            case 5:
                GlobalVars.Instance.Dimension5.SetActive(true);
                break;
        }
        Debug.Log("Dimension " + dim);
    }
}
