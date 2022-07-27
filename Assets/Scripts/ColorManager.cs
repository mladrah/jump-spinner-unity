using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorManager : MonoBehaviour
{
    public GameObject trailManager;

    private void Start() {
        
    }


    //@Code Monkey
    private float HexToFloatNormalized(string hex) {
        return HexToDec(hex) / 255f;
    }

    private int HexToDec(string hex) {
        int dec = System.Convert.ToInt32(hex, 16);
        return dec;
    }




}
