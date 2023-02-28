using JetBrains.Annotations;
using Microsoft.MixedReality.Toolkit;
using Microsoft.MixedReality.Toolkit.Input;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG01Confirm : MonoBehaviour
{
    public BubbleOption BubOpt;
    public TouchBlast TB;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;
    public BubbleOption[] SibBubOpt, Restart;
    public AcceStimulate Acce;
    public AudioSource BingoAudio, WrongAudio;
    public AudioClip clip1, clip2;
    public Color SleepColor;
    // Start is called before the first frame update
    void Start()
    {
        BingoText.SetActive(false);
        WrongText.SetActive(false);
        BubOpt = gameObject.GetComponent<BubbleOption>();
        TB = gameObject.GetComponent<TouchBlast>();
        TB.OnTouchStarted.AddListener(Confirm);
        Acce = gameObject.GetComponent<AcceStimulate>();
        
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        BingoAudio = GetComponent<AudioSource>();
        WrongAudio = GetComponent<AudioSource>();
        //foreach (var b in SibBubOpt)
        //{
        //    TB.BeforeBlastEvent.AddListener(b.Playsound);
        //}
    }

    public void Confirm(HandTrackingInputEventData eventData)
    {
        //foreach (GameObject b in targetball)
        //foreach(GameObject c in commonball)
        if (a1.GetComponent<TouchBlast>().Invoked &&
            a2.GetComponent<TouchBlast>().Invoked &&
            a3.GetComponent<TouchBlast>().Invoked &&
            !a4.GetComponent<TouchBlast>().Invoked &&
            !a5.GetComponent<TouchBlast>().Invoked &&
            !a6.GetComponent<TouchBlast>().Invoked &&
            !a7.GetComponent<TouchBlast>().Invoked
            )
        {


            foreach (var b in SibBubOpt)
            {
                b.GetComponent<TouchBlast>().Invoked = true;
                b.GetComponent<AcceStimulate>().Invoked = true;
            }

            gameObject.GetComponent<TouchBlast>().Invoked = true;

            BingoText.SetActive(true);
            BingoAudio.PlayOneShot(clip1, 1f);

        }
        else
        {
            WrongText.SetActive(true);
            WrongAudio.PlayOneShot(clip2, 1f);
        }

        foreach (var b in SibBubOpt)
        {
            b.GetComponent<TouchBlast>().Invoked = false;
        }

    }
    public void AutoReset()
    {
       
        foreach (var b in SibBubOpt)
        {
            b.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
            b.GetComponent<TouchBlast>().Invoked = false;
        }
        foreach (var s in Restart)
        {
            s.GetComponent<MeshRenderer>().material.SetFloat("_FP", 2);
            s.GetComponent<MeshRenderer>().material.SetColor("_Color", SleepColor);
        }
            BingoText.SetActive(false);
        WrongText.SetActive(false);
    }
}
