using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// For all, Out-Enter-Out will have material change be like Out-in-out coded in AcceStimulate.cs
public class S1AccMode : MonoBehaviour
{
    public AcceStimulate Acce;
    public S1Player Player;
    //public CancelRainbow CancelR;
    public void SetHesMode()
    {
        Acce.BeforeHesEvent.AddListener(Acce.SetInvoked);

        Acce.HesEvent.AddListener(Player.ChangeColor);
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.HesEvent.AddListener(Acce.showDebugger);

        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
    }
    public void SetFastMode()
    {
        Acce.HesEvent.AddListener(Acce.OutMaterial);
        Acce.HesEvent.AddListener(Acce.OpenCloseSti); // CloseSti set to be false after out

        Acce.OutEvent.AddListener(Player.ChangeColor);
    }
    public void Set01Mode()
    {
        Acce.InEvent.AddListener(Acce.SetInvoked);
        Acce.InEvent.AddListener(Player.ChangeColor);
        Acce.InEvent.AddListener(Acce.OutMaterial);
        Acce.AfterOutEvent.AddListener(Acce.UnInvoked);
    }
    public void Set010Mode()
    {
        Acce.OutEvent.AddListener(Player.ChangeColor);
    }
    //public void set010cancelmode()
    //{
    //    acce.inevent.addlistener(cancelr.cancel);
    //    acce.outevent.addlistener(player.changecolor);
    //}
 
}
