using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_Fast_Confirm : MonoBehaviour
{
    public BubbleOption BubOpt;
    public AcceStimulate Acce;
    public BubbleOption[] SibBubOpt,Restart;
    public GameObject a1, a2, a3, a4, a5, a6, a7;
    public GameObject BingoText, WrongText;
    public TouchBlast TB;
    public AudioSource BingoAudio, WrongAudio;
    public AudioClip clip1, clip2;
    public Color SleepColor;
    void Start()
    {
        BingoText.SetActive(false);
        WrongText.SetActive(false);
        BubOpt = gameObject.GetComponent<BubbleOption>();
        Acce = gameObject.GetComponent<AcceStimulate>();
        Acce.HesEvent.AddListener(Acce.OpenCloseSti);
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.OutEvent.AddListener(Confirm);
        Acce.OutEvent.AddListener(Acce.SetInvoked);
        TB = gameObject.GetComponent<TouchBlast>();
        BingoAudio = GetComponent<AudioSource>();
        WrongAudio = GetComponent<AudioSource>();
       
        //foreach (var b in SibBubOpt)
        //{
        //    Acce.OutEvent.AddListener(b.Close010Subs);
        //    TB.BeforeBlastEvent.AddListener(b.Close01Subs);
        //}
    }

    public void Confirm()
    {
        //foreach (GameObject b in targetball)
        //foreach(GameObject c in commonball)
        if (a1.GetComponent<AcceStimulate>().Invoked &&
            a2.GetComponent<AcceStimulate>().Invoked &&
            a3.GetComponent<AcceStimulate>().Invoked &&
            !a4.GetComponent<AcceStimulate>().Invoked &&
            !a5.GetComponent<AcceStimulate>().Invoked &&
            !a6.GetComponent<AcceStimulate>().Invoked &&
            !a7.GetComponent<AcceStimulate>().Invoked
            )
        {

            foreach (var b in SibBubOpt)
            {
                b.GetComponent<TouchBlast>().Invoked = true;
                b.GetComponent<AcceStimulate>().Invoked = true;
            }


            gameObject.GetComponent<AcceStimulate>().Invoked = true;
            BingoText.SetActive(true);
            Invoke("AutoReset", 2);
            WrongText.SetActive(false);
            BingoAudio.PlayOneShot(clip1, 1f);
        }
        else
        {
            WrongText.SetActive(true);
            WrongAudio.PlayOneShot(clip2, 1f);
        }
    }
    public void AutoReset()
    {
        foreach (var b in SibBubOpt)
        {
            b.GetComponent<MeshRenderer>().material.SetFloat("_Dissolve", 0);
            b.GetComponent<TouchBlast>().Invoked = false;
            b.GetComponent<AcceStimulate>().Invoked = false;
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
