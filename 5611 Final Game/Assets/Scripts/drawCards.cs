using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;
using System.IO;

public class drawCards : MonoBehaviour {

    public GameObject card1;

    public Text remaining;

    //Card types, whatever that could mean
    List<GameObject> cards = new List<GameObject>();
    //List<CardProperties> cardProperties = new List<CardProperties>();

    void Start() {
        Debug.Log("Print Starting");
        Setup();

    }

    public void Setup() {
        
        Deck deck = Deck.Instance;
        deck.clear();
        cards.Clear();
        Debug.Log(deck == null);
        cards.Add(card1);
        string[] cardPaths = { "coinMult", "HealthBoost", "HealthBoost2", "HealthBoost3", "SpeedBoost", "Stealth" };

        //cards.Add(card2);

        //string[] guids = AssetDatabase.FindAssets("", new[] { "Assets/Cards" });
        for (int i = 0; i < cardPaths.Length; i++) {
            //string assetPath = AssetDatabase.GUIDToAssetPath(guids[i]);
            CardProperties card = (CardProperties) Resources.Load("Cards/" + cardPaths[i]);
            //CardProperties card = (CardProperties)AssetDatabase.LoadAssetAtPath(assetPath, typeof(CardProperties));
            card.setCardDiskData(cardPaths[i]);

            deck.addCard(card);
            card.Print();
            Debug.Log("loaded");
        }
        Deck.Instance.expand(2);
        Deck.Instance.Shuffle();

        for (int i = 0; i < GlobalVars.Instance.startingCards; i++) {
            GlobalVars.Instance.drawCard();
        }
        remaining.text = "Cards Remaining: " + deck.cardsLeft().ToString();
    }

    public void OnClick() {
        GlobalVars.Instance.drawCard();
        remaining.text = "Cards Remaining: " + Deck.Instance.cardsLeft().ToString();
    }

    //void Update() {
    //    remaining.text = "Cards Remaining: " + Deck.Instance.cardsLeft().ToString();
    //}
}


