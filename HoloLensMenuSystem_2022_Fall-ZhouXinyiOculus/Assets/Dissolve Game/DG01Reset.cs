using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG01Reset : MonoBehaviour
{
    public BubbleOption BubOpt;
    public TouchBlast TB;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;
    public BubbleOption[] SibBubOpt;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(ResetDissolve);
        foreach (var s in SibBubOpt)
        {
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
            TB.BeforeBlastEvent.AddListener(s.Playsound);
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
    }
}
