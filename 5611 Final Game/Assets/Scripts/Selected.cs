using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selected : MonoBehaviour
{

    private bool isPlayed = false;
    public GameObject discardPile;
    public GameObject canvas;
    private GameObject startParent;

    void Awake() {
        canvas = GameObject.Find("Canvas");
	}

    // Start is called before the first frame update
    void Start() {
        startParent = transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setDiscardPile(GameObject discard) {
        Debug.Log(discard.name);
        this.discardPile = discard;
	}


    public void CardPlayed() {
        if (!isPlayed) {
            isPlayed = true;
            transform.SetParent(discardPile.transform, false);
            Debug.Log("Card Played");
            CardDiskData data = gameObject.GetComponent<CardDisplay>().getCardDiskData();
            Debug.Log("Deploying Modifiers\n" + data.ToString());
            GlobalVars.Instance.applyModifiers(data); 
        }
    }
}
