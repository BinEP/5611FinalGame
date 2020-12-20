using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GlobalVars : MonoBehaviour
{
    private static GlobalVars _instance;
    public static GlobalVars Instance
    {
        get { return _instance; }
    }

    //global vars here
    public Vector2 gravityDir = new Vector2(0.0f, -1.0f);
    public float gravityScale = 0f;

    public int playerHealth = 100;
    public float playerSpeed = 7f;
    public float playerMaxFallSpeed = 60f;
    public float playerJump = 5f;
    public int totalCoins = 0;
    public int coinsPerDraw = 3;
    public float playerStealth = 0f;

    public float enemySpeed = 5f;
    public float enemyAggroSpeed = 10.0f;
    public float enemyVisiblityAngle = 45f;
    public float enemyBaseVisibility = 15f;
    public float enemyRearVisibility = 7f;
    public float enemyAttackRange = 4f;
    public int enemyAttackValue = 50;
    public int enemyStartingHealth = 20;

    public int numDimensions = 0;
    public int currentDimensionIter = 0;
    public GameObject Dimensions;
    private List<DimensionHandler> dimensionHandlers;
    private DimensionHandler currentDim = null;

    public EquipabbleItem equipabbleItem = null;

    public Text healthText;
    public Text coinText;
    
    public GameObject playerArea;

    public void Start()
    {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
        dimensionHandlers = new List<DimensionHandler>();
        DimensionHandler[] dims = Dimensions.GetComponentsInChildren<DimensionHandler>();
        foreach (DimensionHandler dim in dims)
        {
            dimensionHandlers.Add(dim);
        }
    }

    public float getVisibilityModifier()
    {
        return 1f - playerStealth;
    }

    public float getEnemyVisibility()
    {
        return enemyBaseVisibility * getVisibilityModifier();
    }

    public float getEnemyRearVisibility()
    {
        return enemyRearVisibility * getVisibilityModifier();
    }

    private void Awake()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            _instance = this;
        }
       
    }

    public void init() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    void Update() {
        healthText.text = "Health: " + playerHealth.ToString();
        coinText.text = "Coins: " + totalCoins.ToString();
        if (playerHealth <= 0) {
            FindObjectOfType<GameManagerScript>().YouLose();
        }
	}

    public void collectCoin() {
        SoundManagerScript.playSound("coin");
        totalCoins++;
    }

    public void switchDim(int newDim)
    {
        Debug.Log("Switching to dimension " + newDim);
        if (newDim > numDimensions || newDim < 1)
        {
            Debug.Log("You just tried to switch to an invalid dimension?!? What are you thinking?!?");
        }
        else
        {
            currentDimensionIter = newDim;

            if (numDimensions > 0)
            {
                foreach (DimensionHandler myDim in dimensionHandlers)
                {
                    if (myDim.Dim == newDim)
                    {
                        myDim.gameObject.SetActive(true);
                        currentDim = myDim;
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

    public DimensionHandler getCurrentDim()
    {
        return currentDim;
    }
}
