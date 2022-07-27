using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundVFX : MonoBehaviour
{
    private ParticleSystem ps;

    GradientColorKey[] colorKey;
    GradientAlphaKey[] alphaKey;
    // Start is called before the first frame update
    void Start()
    {
        ps = GetComponent<ParticleSystem>();
        Gradient gradient = new Gradient();

        var col = ps.colorOverLifetime;
        col.enabled = true;

        Gradient grad = new Gradient();
        grad.SetKeys(new GradientColorKey[] 
        { new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f), 0.0f), new GradientColorKey(Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f), 1.0f) },
            new GradientAlphaKey[] 
            { new GradientAlphaKey(1.0f, 1.0f), new GradientAlphaKey(1.0f, 1.0f) });

        col.color = grad;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
