using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class S1Player : MonoBehaviour
{
    public Color[] ColorToChange;
    private int ColorIndex;
    private void Start()
    {
        ColorIndex = 0;
        ColorToChange = new Color[6] { Color.red, Color.green, Color.blue, Color.yellow, Color.cyan, new Color(0.6666667f, 0.6666667f, 0.6666667f, 1) };
    }

    public void ChangeColor()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange[ColorIndex]);
        if (ColorIndex == 5)
            ColorIndex = 0;
        else
            ColorIndex++;
    }
}
