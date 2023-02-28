using Microsoft.MixedReality.Toolkit.Input;
using Oculus.Interaction.Samples;
using Oculus.Platform.Samples.VrHoops;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;
using static Microsoft.MixedReality.Toolkit.Experimental.UI.KeyboardKeyFunc;

public class AB : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public TouchBlast TB;
    public Color ColorToChange;
    public Color ColorNoChange;
    public BubbleOption[] SibBubOpt;

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
        gameObject.GetComponent<MeshRenderer>().material.SetColor("Color_", ColorToChange);
    }
    void Update()
    {
        
    }
}
   
