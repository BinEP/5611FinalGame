using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "defaultCard", menuName = "Card")] //Could be Card/stuff for a submenu
public class CardProperties : ScriptableObject
{
    public string name;
    public string description;

    public Sprite artwork;

    public int cost;
    public int attack;
    public int health;


    private CardDiskData data;

    public void Print() {
        Debug.Log(name + ": " + description + "\tCost: " + cost + "\tAttack: " + attack + "\tHealth: " + health);

    }

    public CardProperties Copy() {
        CardProperties cp = (CardProperties) ScriptableObject.CreateInstance("CardProperties");
        cp.name = name;
        cp.description = description;
        cp.artwork = artwork;
        cp.data = JsonUtility.FromJson<CardDiskData>(JsonUtility.ToJson(data));
        return cp;
	}

    public CardDiskData getCardModifierData() {
        return data;
	}

    public void setCardDiskData(string path) {
        Debug.Log("Attempting to Load from: " + "CardModifiers/" + path + ".json");
		data = CardDiskData.GetFromFile("CardModifiers/" + path);
        Debug.Log(data.ToString());
    }
}
