using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailColors : MonoBehaviour
{
    private TrailRenderer tr;
    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    public GameObject glow;
    // Start is called before the first frame update
    void Start() {
        //tr = GetComponent<TrailRenderer>();
        //Gradient gradient = new Gradient();

        //// Populate the alpha  keys at relative time 0 and 1  (0 and 100%)
        //alphaKey = new GradientAlphaKey[2];
        //alphaKey[0].alpha = 1.0f;
        //alphaKey[0].time = 0.0f;
        //alphaKey[1].alpha = 1f;
        //alphaKey[1].time = 1.0f;

        ////GANZ RECHTS
        //colorKey[1].color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);

        //gradient.SetKeys(colorKey, alphaKey);
        //tr.colorGradient = gradient;

        //Material mat = GetComponent<Renderer>().material;
        //mat.SetColor("_EmissionColor", colorKey[1].color);

        //glow.GetComponent<SpriteRenderer>().color = colorKey[1].color;


    }
}
