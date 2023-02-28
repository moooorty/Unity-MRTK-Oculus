using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AB010 : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public Color ColorToChange;
    public Color ColorNoChange;
    public BubbleOption[] SibBubOpt;
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        Acce.OutEvent.AddListener(Change);
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        foreach (var b in SibBubOpt)
        {
            Acce.OutEvent.AddListener(b.Close010Subs);
        }
    }
    public void Change()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);

    }
}
