using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABC : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public Color ColorToChange;
    public Color ColorNoChange;
    public BubbleOption[] SibBubOpt;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;
    public bool Invoked;
    //public GameObject[] targetball;
    //public GameObject[] commonball;
    void Start()
    {
        BingoText.SetActive(false);
        WrongText.SetActive(false);
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(Confirm);
        Acce = gameObject.GetComponent<AcceStimulate>();

        foreach (var s in SibBubOpt)
        {
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
        }

    }

    public void Confirm(HandTrackingInputEventData eventData)
    {
        //foreach (GameObject b in targetball)
        //foreach(GameObject c in commonball)
        if (a1.GetComponent<TouchBlast>().Invoked &&
            a3.GetComponent<TouchBlast>().Invoked &&
            a5.GetComponent<TouchBlast>().Invoked &&
            !a2.GetComponent<TouchBlast>().Invoked &&
            !a4.GetComponent<TouchBlast>().Invoked &&
            !a6.GetComponent<TouchBlast>().Invoked &&
            !a7.GetComponent<TouchBlast>().Invoked
            )
        {
            BingoText.SetActive(true);
            WrongText.SetActive(false);
            a2.GetComponent<TouchBlast>().Invoked = true;
            a4.GetComponent<TouchBlast>().Invoked = true;
            a6.GetComponent<TouchBlast>().Invoked = true;
            a7.GetComponent<TouchBlast>().Invoked = true;

        }
        else
            WrongText.SetActive(true);
    }
}
