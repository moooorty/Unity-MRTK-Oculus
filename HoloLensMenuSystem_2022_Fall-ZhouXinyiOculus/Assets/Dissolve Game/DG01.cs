using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class DG01 : MonoBehaviour
{
    public BubbleOption BubOpt;
    public TouchBlast TB;
    public BubbleOption[] SibBubOpt;
    public GameObject WrongText;
    public Color SleepColor, AwakeColor;
    // Start is called before the first frame update
    void Start()
    {
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(Dissolve);
        foreach (var s in SibBubOpt)
        {
            s.GetComponent<MeshRenderer>().material.SetFloat("_FP", 2);
            s.GetComponent<MeshRenderer>().material.SetColor("_Color", SleepColor);
            TB.BeforeBlastEvent.AddListener(s.Close01Subs);
            TB.BeforeBlastEvent.AddListener(s.Playsound);
        }
    }

    public void Dissolve(HandTrackingInputEventData eventData)
    {
        gameObject.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 1);
        WrongText.SetActive(false);
        foreach (var s in SibBubOpt)
        {
     
            s.GetComponent<MeshRenderer>().material.SetFloat("_FP", 0.38f);
            s.GetComponent<MeshRenderer>().material.SetColor("_Color", AwakeColor);
        }

    }
}
