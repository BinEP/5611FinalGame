using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public static class GlobalVars
{
    //private static GlobalVars _instance;
    //public static GlobalVars Instance
    //{
    //    get { return _instance; }
    //}

    //global vars here
    public static Vector2 gravityDir = new Vector2(0.0f, -1.0f);
    public static float gravityScale = 70f;

    public static int playerHealth = 100;
    public static float playerSpeed = 15f;
    public static float playerMaxFallSpeed = 60f;
    public static float playerJump = 3f;
    public static int totalCoins = 0;
    public static int coinsPerDraw = 3;
    public static float playerStealth = 0f;

    public static float enemySpeed = 5f;
    public static float enemyAggroSpeed = 10.0f;
    public static float enemyVisiblityAngle = 45f;
    public static float enemyBaseVisibility = 15f;
    public static float enemyRearVisibility = 7f;
    public static float enemyAttackRange = 4f;
    public static int enemyAttackValue = 0;
    public static int enemyStartingHealth = 20;

    public static int numDimensions = 5;
    public static int currentDimensionIter = 1;
    public static GameObject Dimensions;
    public static List<DimensionHandler> dimensionHandlers;
    public static DimensionHandler currentDim = null;

    public static EquipabbleItem equipabbleItem = null;
    
    public static GameObject playerArea;

    public static float getVisibilityModifier()
    {
        return 1f - playerStealth;
    }

    public static float getEnemyVisibility()
    {
        return enemyBaseVisibility * getVisibilityModifier();
    }

    public static float getEnemyRearVisibility()
    {
        return enemyRearVisibility * getVisibilityModifier();
    }


    public static void collectCoin() {
        SoundManagerScript.playSound("coin");
        totalCoins++;
    }

    public static DimensionHandler getCurrentDim() {
        return currentDim;
    }
}
