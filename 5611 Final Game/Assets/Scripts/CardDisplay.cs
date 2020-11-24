using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardDisplay : MonoBehaviour
{

    public CardProperties card;

    public Text nameText;
    public Text descriptionText;

    public Image artworkImage;

    // Start is called before the first frame update
    void Start() {
        updateProperties(null);
    }

    public void updateProperties(CardProperties newCard) {
        if (newCard == null) return;
        card = newCard;
        card.Print();
        nameText.text = card.name;
        descriptionText.text = card.description;
        artworkImage.sprite = card.artwork;
    }

    public CardProperties getProperties() {
        return card;
	}

    public CardDiskData getCardDiskData() {
        return card.getCardModifierData();
    }

  

}
