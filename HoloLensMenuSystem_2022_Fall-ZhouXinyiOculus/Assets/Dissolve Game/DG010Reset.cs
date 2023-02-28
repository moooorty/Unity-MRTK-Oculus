using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG010Reset : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;
    public BubbleOption[] SibBubOpt, Confirm;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        TB.OnTouchStarted.AddListener(ResetDissolve);
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        TB.BeforeBlastEvent.AddListener(BubOpt.Playsound);
        foreach (var b in SibBubOpt)
        {
            Acce.OutEvent.AddListener(b.Reset010dissolve);
            TB.BeforeBlastEvent.AddListener(b.Close01Subs);
           
        }
    }

    public void ResetDissolve(HandTrackingInputEventData eventData)
    {
        a1.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a2.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a3.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a4.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a5.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a6.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
        a7.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);


        BingoText.SetActive(false);
        WrongText.SetActive(false);
        foreach(var b in Confirm)
        {
            b.GetComponent<MeshRenderer>().material.SetFloat("_FP", 3);
        }
    }
    
}
