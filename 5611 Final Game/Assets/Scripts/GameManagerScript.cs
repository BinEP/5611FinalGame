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


    public GameObject Dimension1;
    public GameObject Dimension2;
    public GameObject Dimension3;
    public GameObject Dimension4;
    public GameObject Dimension5;

    void Start()
    {
        SetDimension(GlobalVars.Instance.currentDimension);
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
        SetDimension(GlobalVars.Instance.currentDimension);

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
            SetDimension(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            SetDimension(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            SetDimension(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            SetDimension(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            SetDimension(5);
        }
    }

    private void SetDimension(int dim)
    {
        GlobalVars.Instance.currentDimension = dim;

        Dimension1.SetActive(false);
        Dimension2.SetActive(false);
        Dimension3.SetActive(false);
        Dimension4.SetActive(false);
        Dimension5.SetActive(false);

        switch (dim)
        {
            case 1:
                Dimension1.SetActive(true);
                break;
            case 2:
                Dimension2.SetActive(true);
                break;
            case 3:
                Dimension3.SetActive(true);
                break;
            case 4:
                Dimension4.SetActive(true);
                break;
            case 5:
                Dimension5.SetActive(true);
                break;
        }
        Debug.Log("Dimension " + dim);
    }
}
