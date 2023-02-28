using Microsoft.MixedReality.Toolkit.Input;
using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABR : MonoBehaviour
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
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(Change);
        Acce = gameObject.GetComponent<AcceStimulate>();

        foreach (var s in SibBubOpt)
        {
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
        }

    }
    public void Change(HandTrackingInputEventData eventData)
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




    }

}
