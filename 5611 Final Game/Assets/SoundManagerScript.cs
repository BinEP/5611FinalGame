using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour {


    private static SoundManagerScript _instance;
    public static SoundManagerScript Instance {
        get { return _instance; }
    }

    private void Awake() {
        if (_instance != null && _instance != this) {
            Destroy(this.gameObject);
        } else {
            _instance = this;
        }

        coin = Resources.Load<AudioClip>("coin");
        draw = Resources.Load<AudioClip>("draw");
        attack = Resources.Load<AudioClip>("attack");

        audioSrc = GetComponent<AudioSource>();
    }

    public static AudioClip coin, draw, attack;
    static AudioSource audioSrc;

    public static void playSound(string clip) {
        switch (clip) {
            case "coin":
                audioSrc.PlayOneShot(coin);
                break;
            case "draw":
                audioSrc.PlayOneShot(draw);
                break;
            case "attack":
                audioSrc.PlayOneShot(attack);
                break;
            default:
                break;
        }
    }
}