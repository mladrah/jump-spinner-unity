using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailManager : MonoBehaviour
{
    public GameObject[] trails;
    private Gradient colorGradient;
    private GradientColorKey[] colorKey;

    public void ChangeTrail(GameObject spinnerTrail, int index) {
        float startWidth = trails[index].GetComponent<TrailRenderer>().startWidth;
        float endWidth = trails[index].GetComponent<TrailRenderer>().endWidth;
        float widthMult = trails[index].GetComponent<TrailRenderer>().widthMultiplier;
        AnimationCurve widthCruve = trails[index].GetComponent<TrailRenderer>().widthCurve;
        float time = trails[index].GetComponent<TrailRenderer>().time;
        //Gradient gradient = trails[index].GetComponent<TrailRenderer>().colorGradient;

        spinnerTrail.GetComponent<TrailRenderer>().startWidth = startWidth;
        spinnerTrail.GetComponent<TrailRenderer>().endWidth = endWidth;
        spinnerTrail.GetComponent<TrailRenderer>().widthMultiplier = widthMult;
        spinnerTrail.GetComponent<TrailRenderer>().widthCurve = widthCruve;
        spinnerTrail.GetComponent<TrailRenderer>().time = time;
        //spinnerTrail.GetComponent<TrailRenderer>().colorGradient = gradient;
    }

    public void ChangeTrailStartColor(GameObject trail, Color currentSelectedColor) {
        TrailRenderer tr = trail.GetComponent<TrailRenderer>();

        colorKey = new GradientColorKey[2];
        colorGradient = new Gradient();

        colorKey[0].color = currentSelectedColor;
        colorKey[0].time = 0.0f;
        colorKey[1].color = tr.colorGradient.colorKeys[1].color;
        colorKey[1].time = tr.colorGradient.colorKeys[1].time;

        colorGradient.SetKeys(colorKey, tr.colorGradient.alphaKeys);

        tr.colorGradient = colorGradient;

    }

    public void ChangeTrailEndColor(GameObject trail, Color currentSelectedColor) {
        TrailRenderer tr = trail.GetComponent<TrailRenderer>();

        colorKey = new GradientColorKey[2];
        colorGradient = new Gradient();

        colorKey[1].color = currentSelectedColor;
        colorKey[1].time = tr.colorGradient.colorKeys[1].time;
        colorKey[0].color = tr.colorGradient.colorKeys[0].color;
        colorKey[0].time = tr.colorGradient.colorKeys[0].time;

        colorGradient.SetKeys(colorKey, tr.colorGradient.alphaKeys);

        tr.colorGradient = colorGradient;

        Material mat = tr.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", colorKey[1].color);

        trail.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorKey[1].color;
    }

    public void ChangeTrailColorRandom(GameObject trail) {
        TrailRenderer tr = trail.GetComponent<TrailRenderer>();

        colorKey = new GradientColorKey[2];
        colorGradient = new Gradient();

        colorKey[1].color = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        colorKey[1].time = tr.colorGradient.colorKeys[1].time;
        colorKey[0].color = tr.colorGradient.colorKeys[0].color;
        colorKey[0].time = tr.colorGradient.colorKeys[0].time;

        colorGradient.SetKeys(colorKey, tr.colorGradient.alphaKeys);

        tr.colorGradient = colorGradient;

        Material mat = tr.GetComponent<Renderer>().material;
        mat.SetColor("_EmissionColor", colorKey[1].color);

        trail.transform.GetChild(0).GetComponent<SpriteRenderer>().color = colorKey[1].color;
    }

}
