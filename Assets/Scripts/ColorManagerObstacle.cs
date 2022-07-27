using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ColorManagerObstacle
{
    public static bool defaultColor;
    public static bool blackWhiteColor;
    public static bool redColor;
    public static bool greenColor;
    public static bool blueColor;
    public static bool yellowColor;
    public static bool turkeyColor;
    public static bool pinkColor;
    public static bool orangeColor;

    public static void SetAllFalse() {
        defaultColor = false;
        blackWhiteColor = false;
        redColor = false;
        greenColor = false;
        blueColor = false;
        yellowColor = false;
        turkeyColor = false;
        pinkColor = false;
        orangeColor = false;
    }

    public static Color ChangeObstacleColor() {
        Color col = Color.blue;

        if (defaultColor) {
            col = Random.ColorHSV(0f, 1f, 1f, 1f, 1f, 1f);
        } else if (blackWhiteColor) {
            col = Random.ColorHSV(0f, 0f, 0f, 0f, 0.1f, 1f);
        } else if (redColor) {
            col = Random.ColorHSV(0f, 0f, 1f, 1f, 0.2f, 1f);
        } else if (greenColor) {
            col = Random.ColorHSV(0.35f, 0.35f, 1f, 1f, 0.2f, 1f);
        } else if (blueColor) {
            col = Random.ColorHSV(0.65f, 0.65f, 1f, 1f, 0.2f, 1f);
        } else if (yellowColor) {
            col = Random.ColorHSV(0.16667f, 0.16667f, 1f, 1f, 0.2f, 1f);
        } else if (turkeyColor) {
            col = Random.ColorHSV(0.5f, 0.5f, 1f, 1f, 0.2f, 1f);
        } else if (pinkColor) {
            col = Random.ColorHSV(0.875f, 0.875f, 1f, 1f, 0.2f, 1f);
        } else if (orangeColor) {
            col = Random.ColorHSV(0.07f, 0.07f, 1f, 1f, 0.2f, 1f);
        }

        return col;
    }
}
