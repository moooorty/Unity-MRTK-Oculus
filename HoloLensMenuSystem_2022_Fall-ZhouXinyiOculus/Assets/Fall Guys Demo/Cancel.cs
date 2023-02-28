using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cancel : MonoBehaviour
{
    public BubbleOption BubOpt;
    public TouchBlast TB;
    public Color ColorToChange;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(Change);
    }

    public void Change(HandTrackingInputEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
    }
}
