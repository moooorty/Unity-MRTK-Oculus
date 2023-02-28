using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG010 : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public BubbleOption[] SibBubOpt;
    public GameObject WrongText;
    public Color SleepColor, AwakeColor;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        TB = gameObject.GetComponent<TouchBlast>();
        Acce.OutEvent.AddListener(Dissolve);
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        foreach (var s in SibBubOpt)
        {
            s.GetComponent<TouchBlast>().Invoked = true;
            s.GetComponent<MeshRenderer>().material.SetFloat("_FP", 2);
            s.GetComponent<MeshRenderer>().material.SetColor("_Color", SleepColor);
            Acce.OutEvent.AddListener(s.Close010Subs);
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
       }
 
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
