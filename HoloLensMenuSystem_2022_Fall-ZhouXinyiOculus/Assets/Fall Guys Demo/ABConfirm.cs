using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ABConfirm : MonoBehaviour
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
    void Start()
    {
        BingoText.SetActive(false);
        WrongText.SetActive(false);
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();

        foreach (var item in SibBubOpt)
        {
            Acce.BeforeHesEvent.AddListener(item.Close010Subs);
        }

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(ConfirmChange);
    }
    public void ConfirmChange()
    {
        //foreach (GameObject b in targetball)
        //foreach(GameObject c in commonball)
        if (a1.GetComponent<AcceStimulate>().Invoked &&
            a3.GetComponent<AcceStimulate>().Invoked &&
            a5.GetComponent<AcceStimulate>().Invoked &&
            !a2.GetComponent<AcceStimulate>().Invoked &&
            !a4.GetComponent<AcceStimulate>().Invoked &&
            !a6.GetComponent<AcceStimulate>().Invoked &&
            !a7.GetComponent<AcceStimulate>().Invoked
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
