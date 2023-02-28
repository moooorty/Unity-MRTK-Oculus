using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetAcceMode : MonoBehaviour
{
    public bool Delay, Fast, Acce, Velo;
    public float DelayTime;
    void Start()
    {
        SetAllAcces(gameObject);
    }

    public void SetAllAcces(GameObject Root)
    {
        if (Root.transform.childCount == 0) { return; }
        foreach (Transform t in Root.transform)
        {
            SetAllAcces(t.gameObject);
            AcceStimulate tas = t.gameObject.GetComponent<AcceStimulate>();
            if (tas)
            {
                tas.Delay = Delay;
                tas.Fast = Fast;
                tas.Acce = Acce;
                tas.Velo = Velo;
                tas.WaitTime = DelayTime;
            }
        }
    }
}
