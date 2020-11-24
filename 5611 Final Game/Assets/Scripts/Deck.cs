using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Deck : MonoBehaviour
{
    private static Deck _instance;
    public static Deck Instance {
        get { return _instance; }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }
    }

    List<CardProperties> cards = new List<CardProperties>();

    public void addCard(CardProperties cardProperties) {
        cards.Add(cardProperties);
	}

    public CardProperties getCard() {
        //int rand = UnityEngine.Random.Range(0, cards.Count);
        //CardProperties card = cards[rand];
        //cards.RemoveAt(rand);
        //return card;
        if (cards.Count < 1) return null;
        CardProperties cp = cards[0];
        cards.RemoveAt(0);
        return cp;
	}

    public void Shuffle() {
        cards.Shuffle();
	}

    public int cardsLeft() {
        return cards.Count;
	}

    public void expand(int mult) {
        int origCount = cards.Count;
        for (int i = 0; i < origCount; i++) {
            for (int j = 0; j < mult; j++) {
                cards.Add(cards[i].Copy());
			}
		}
	}

    public void clear() {
        cards.Clear();
	}
}

public static class Generics {
    private static System.Random rng = new System.Random();
    public static void Shuffle<T>(this IList<T> list) {
        int n = list.Count;
        while (n > 1) {
            n--;
            int k = rng.Next(n + 1);
            T value = list[k];
            list[k] = list[n];
            list[n] = value;
        }
    }
}

//public class Card {

//	private GameObject onScreen;
//	private CardProperties properties;

//	public Card(GameObject go, CardProperties prop) {
//        this.onScreen = go;
//        this.properties = prop;
//	}

//    public void setProperties(CardProperties newProperties) {
//        this.properties = newProperties;
//        CardDisplay cd = (CardDisplay)onScreen.GetComponent<CardDisplay>();
//        cd.updateProperties(newProperties);
//	}

//    public CardProperties getProperties() {
//        return properties;
//	}
//}

//public class DictStuff {

//    public IEnumerable<TValue> RandomValues<TKey, TValue>(IDictionary<TKey, TValue> dict) {
//        Random rand = new Random();
//        List<TValue> values = Enumerable.ToList(dict.Values);
//        int size = dict.Count;
//        while (true) {
//            yield return values[rand.Next(size)];
//        }
//    }
//}