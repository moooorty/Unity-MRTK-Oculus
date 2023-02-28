using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABAcc : MonoBehaviour
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

        foreach (var item in SibBubOpt)
        {
            Acce.BeforeHesEvent.AddListener(item.Close010Subs);
        }

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(Change);
    }

    public void Change()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);

    }
}
