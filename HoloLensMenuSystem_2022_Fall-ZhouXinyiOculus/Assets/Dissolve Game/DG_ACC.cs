using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_ACC : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public BubbleOption[] SibBubOpt;
    public GameObject WrongText;
    public Color SleepColor, AwakeColor;
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        TB = gameObject.GetComponent<TouchBlast>();

        foreach (var item in SibBubOpt)
        {
            item.GetComponent<MeshRenderer>().material.SetFloat("_FP", 2);
            item.GetComponent<MeshRenderer>().material.SetColor("_Color", SleepColor);
            Acce.BeforeHesEvent.AddListener(item.Close010Subs);
            TB.BeforeBlastEvent.AddListener(item.Close01Subs);

        }

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(Dissolve);
        Acce.OutEvent.AddListener(Acce.showDebugger);
    }

    public void Dissolve()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 1);
        WrongText.SetActive(false);
        foreach (var s in SibBubOpt)
        {
            s.GetComponent<TouchBlast>().Invoked = false;
            s.GetComponent<MeshRenderer>().material.SetFloat("_FP", 0.38f);
            s.GetComponent<MeshRenderer>().material.SetColor("_Color", AwakeColor);
        }
    }
}
