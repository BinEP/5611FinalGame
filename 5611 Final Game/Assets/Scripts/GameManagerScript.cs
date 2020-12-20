using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour
{
    public bool gameIsPaused = false;
    public GameObject EndGameMenuUI;
    public string nextLevelName;
    public Text coinsText;
    public Text healthText;
    public GameObject Dimensions;

    void Start()
    {
        GlobalVars.Dimensions = Dimensions;
        GlobalVars.dimensionHandlers = new List<DimensionHandler>();
        DimensionHandler[] dims = Dimensions.GetComponentsInChildren<DimensionHandler>();
        foreach (DimensionHandler dim in dims) {
            GlobalVars.dimensionHandlers.Add(dim);
        }


        switchDim(GlobalVars.currentDimensionIter);
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
        switchDim(GlobalVars.currentDimensionIter);

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
        coinsText.text = "Coins: " + GlobalVars.totalCoins.ToString();


        if (GlobalVars.playerHealth <= 0) {
            FindObjectOfType<GameManagerScript>().YouLose();
        }
    }

    public static void switchDim(int newDim) {
        Debug.Log("Switching to dimension " + newDim);
        if (newDim > GlobalVars.numDimensions || newDim < 1) {
            Debug.Log("You just tried to switch to an invalid dimension?!? What are you thinking?!?");
        } else {
            GlobalVars.currentDimensionIter = newDim;

            if (GlobalVars.numDimensions > 0) {
                foreach (DimensionHandler myDim in GlobalVars.dimensionHandlers) {
                    if (myDim.Dim == newDim) {
                        myDim.gameObject.SetActive(true);
                        GlobalVars.currentDim = myDim;
                        //GameObject dimGO = myDim.GetComponentInParent<GameObject>();
                    } else {
                        myDim.gameObject.SetActive(false);
                        //GameObject dimGO = myDim.GetComponentInParent<GameObject>();
                    }
                }
            }
        }
    }



}
