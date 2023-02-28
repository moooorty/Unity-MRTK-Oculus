using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DG_ACC_Confirm : MonoBehaviour
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
        TB = gameObject.GetComponent<TouchBlast>();
        BingoAudio = GetComponent<AudioSource>();
        WrongAudio = GetComponent<AudioSource>();

        //foreach (var item in SibBubOpt)
        //{
        //    Acce.BeforeHesEvent.AddListener(item.Close010Subs);
        //    TB.BeforeBlastEvent.AddListener(item.Close01Subs);
        //}

        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);
        Acce.HesEvent.AddListener(Confirm);
      
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
            BingoText.SetActive(true);
            Acce.OutEvent.AddListener(BubOpt.SaveEffect);
            Invoke("AutoReset", 2);
            WrongText.SetActive(false);
            foreach (var b in SibBubOpt)
            {
                b.GetComponent<TouchBlast>().Invoked = true;
                b.GetComponent<AcceStimulate>().Invoked = true;
            }

            gameObject.GetComponent<AcceStimulate>().Invoked = true;
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
