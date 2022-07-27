using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    private static bool pitchDone = false;
    private static bool richtung = false;

    public static string openingClip = "opening";
    public static string dieClip = "die";
    public static string pointClip = "point";
    public static string buttonClip = "button";

    private static AudioClip opening, die, point, button;
    private static AudioSource audioSrc;
    public GameObject Pitcher;
    private static AudioSource audioSrcPitcher;
    void Start() {
        audioSrc = GetComponent<AudioSource>();
        audioSrcPitcher = Pitcher.GetComponent<AudioSource>();

        opening = Resources.Load<AudioClip>("Opening");
        die = Resources.Load<AudioClip>("Death_0");
        point = Resources.Load<AudioClip>("Point_2");
        button = Resources.Load<AudioClip>("Point_2");

    }

    [System.Obsolete]
    public static void PlaySound(string clip) {
        switch (clip) {
            case "opening":
                audioSrc.PlayOneShot(opening);
                break;
            case "die":
                audioSrc.PlayOneShot(die);
                break;
            case "point":
                audioSrcPitcher.PlayOneShot(point);

                if (!pitchDone) {
                    richtung = true;
                    pitchDone = true;
                }

                if (richtung) {
                    audioSrcPitcher.pitch += 0.005f;
                    if (audioSrcPitcher.pitch >= 1.1f) {
                        richtung = false;
                    }
                } else {
                    audioSrcPitcher.pitch -= 0.005f;
                    if (audioSrcPitcher.pitch <= 1f)
                        richtung = true;
                }

                break;
            case "button":
                audioSrc.PlayOneShot(button);
                break;
        }

    }

    [System.Obsolete]
    public static bool pitchBeginning() {
        int pitchHighLow = Random.RandomRange(0, 1);
        if (pitchHighLow == 1)
            return true;
        else
            return false;
    }
}
