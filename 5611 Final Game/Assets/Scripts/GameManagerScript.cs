using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class GameManagerScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject EndGameMenuUI;
    public GameObject StartScreenUI;
    public string nextLevelName;

    public Text healthText;
    public Text coinText;

    public GameObject Dimensions;
    public GameObject firstDimension;
    private List<DimensionHandler> dimensionHandlers = new List<DimensionHandler>();

    public GameObject enemy;

    private bool firstTime = true;

    void Start()
    {
        GlobalVars.Dimensions = Dimensions;
        GlobalVars.currentDim = firstDimension.GetComponent<DimensionHandler>();

        DimensionHandler[] tempDims = Dimensions.GetComponentsInChildren<DimensionHandler>();
        foreach (DimensionHandler myDim in tempDims)
        {
            dimensionHandlers.Add(myDim);
        }


            switchDim(GlobalVars.currentDimensionIter);
        if (GlobalVars.startTheWorld)
        {
            Pause(false);
            StartScreenUI.SetActive(true);
        }
    }

    public void YouLose()
    {
        if (!gameIsPaused)
        {
            GlobalVars.startTheWorld = false;
            Pause(true);
        }
    }

    void Pause(bool showEndScreen)
    {
        gameIsPaused = true;
        Time.timeScale = 0f;

        if (showEndScreen) EndGameMenuUI.SetActive(true);
    }

    public void beginTheGame()
    {
        gameIsPaused = false;
        StartScreenUI.SetActive(false);
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void Restart()
    {
        resetGlobalVars();
        switchDim(GlobalVars.currentDimensionIter);
        EndGameMenuUI.SetActive(false);
        gameIsPaused = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        Time.timeScale = 1f;
    }

    public void resetGlobalVars()
    {
        GlobalVars.playerHealth = GlobalVars.playerHealthStart;
        GlobalVars.totalCoins = 0;
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
            switchDim(1);
        }
        else if (Input.GetKey(KeyCode.Alpha2) || Input.GetKey(KeyCode.Keypad2))
        {
            switchDim(2);
        }
        else if (Input.GetKey(KeyCode.Alpha3) || Input.GetKey(KeyCode.Keypad3))
        {
            switchDim(3);
        }
        else if (Input.GetKey(KeyCode.Alpha4) || Input.GetKey(KeyCode.Keypad4))
        {
            switchDim(4);
        }
        else if (Input.GetKey(KeyCode.Alpha5) || Input.GetKey(KeyCode.Keypad5))
        {
            switchDim(5);
        }

        healthText.text = "Health: " + GlobalVars.playerHealth.ToString();
        coinText.text = "Features: " + GlobalVars.totalCoins.ToString();
        if (GlobalVars.playerHealth <= 0)
        {
            YouLose();
        }

        float toCompare = GlobalVars.totalCoins / 5.0f;
        if (toCompare > GlobalVars.numEnemies)
        {
            Instantiate(enemy);
            GlobalVars.numEnemies++;
        }
    }

    public void switchDim(int newDim)
    {
        Debug.Log("Switching to dimension " + newDim);
        if (newDim > GlobalVars.numDimensions || newDim < 1)
        {
            Debug.Log("You just tried to switch to an invalid dimension?!? What are you thinking?!?");
        }
        else
        {
            GlobalVars.currentDimensionIter = newDim;

            if (GlobalVars.numDimensions > 0)
            {
                foreach (DimensionHandler myDim in dimensionHandlers)
                {
                    if (myDim.Dim == newDim)
                    {
                        myDim.gameObject.SetActive(true);
                        GlobalVars.currentDim = myDim;
                        //GameObject dimGO = myDim.GetComponentInParent<GameObject>();
                    }
                    else
                    {
                        myDim.gameObject.SetActive(false);
                        //GameObject dimGO = myDim.GetComponentInParent<GameObject>();
                    }
                }
            }
        }
    }

}
