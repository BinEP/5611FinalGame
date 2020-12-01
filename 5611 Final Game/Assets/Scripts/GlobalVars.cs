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

    public int playerHealth = 100;
    public float playerSpeed = 7f;
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

    public EquipabbleItem equipabbleItem = null;

    public Text healthText;
    public Text coinText;

    public int startingCards = 3;

    public GameObject card1;
    public GameObject playerArea;
    public GameObject discardPile;
    public int numCards;
    List<GameObject> cards = new List<GameObject>();

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

        cards.Add(card1);
       
    }

    void Update() {
        healthText.text = "Health: " + playerHealth.ToString();
        coinText.text = "Coins: " + totalCoins.ToString();
        if (playerHealth <= 0) {
            FindObjectOfType<GameManagerScript>().YouLose();
        }
	}

    public void applyModifiers(CardDiskData data) {
        playerHealth = Mathf.Min(120, playerHealth + data.addHealth);
        playerSpeed += data.speedModifier;
        for (int i = 0; i < data.addCoins; i++) {
            collectCoin();
		}
        playerStealth = Mathf.Min(0.75f, playerStealth + data.stealthModifier);
        Debug.Log("Applied Modifiers");
    }

    public void collectCoin() {
        if (++totalCoins % coinsPerDraw == 0) drawCard();
        SoundManagerScript.playSound("coin");

    }

    public void drawCard() {
        if (Deck.Instance.cardsLeft() < 1) return;
        

        for (int i = 0; i < numCards; i++) {
            GameObject playerCard = Instantiate(cards[UnityEngine.Random.Range(0, cards.Count)], new Vector3(0, 0, 0), Quaternion.identity);
            Selected script = playerCard.GetComponent<Selected>();
            script.setDiscardPile(discardPile);
            CardDisplay cardProps = playerCard.GetComponent<CardDisplay>();
            cardProps.updateProperties(Deck.Instance.getCard());

            playerCard.transform.SetParent(playerArea.transform, false);
        }
        SoundManagerScript.playSound("draw");
    }
}


[Serializable]
public class CardDiskData {

    public int addHealth;
    public int addCoins;
    public int speedModifier;
    public int stealthModifier;


    //Only from a file
    private CardDiskData() { }

    public static CardDiskData GetFromString(string config) {
        return JsonUtility.FromJson<CardDiskData>(config);
    }

    public static CardDiskData GetFromFile(string path) {
        //try {

            string config = LoadResourceTextfile(path);
            return GetFromString(config);
        //} catch (Exception e) {
        //    Debug.LogException(e);
        //    Debug.Log("ERROR:" + e);
        //    return null;
        //}
    }

    public static string LoadResourceTextfile(string path) {
        TextAsset targetFile = Resources.Load<TextAsset>(path);
        return targetFile.text;
    }

    public string ToString() {
        return "Add Health: " + addHealth + "\tAdd Coins: " + addCoins + "\tSpeed Modifier: " + speedModifier + "\tStealth Modifier: " + stealthModifier;

    }


     

}