using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABReset : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public Color ColorToChange;
    public Color ColorNoChange;
    public BubbleOption[] SibBubOpt;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;

    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(ResetChange);
       

    }
    public void ResetChange()
    {
        a1.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a2.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a3.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a4.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a5.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a6.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
        a7.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);

        BingoText.SetActive(false);
        WrongText.SetActive(false);
        foreach (var b in SibBubOpt)
        {
            Acce.BeforeHesEvent.AddListener(b.Close010Subs);
        }



    }
}
