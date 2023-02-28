using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Toolkit.Input;
using Microsoft.MixedReality.Toolkit.UI;
using static UnityEngine.UI.CanvasScaler;
using Microsoft.MixedReality.Toolkit.Utilities;
using System.Security.AccessControl;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using TMPro;
using System.Diagnostics;

/// <summary>
/// Time delay, Acceleration, and velocity control component
/// </summary>
/// <param name="Delay">Bool for Time Delay mode, set manually</param>
/// <param name="Acce">Bool for Acceleration mode, set manually</param>
/// <param name="Velo">Bool for Velocity mode, set manually</param>
/// <param name="BeforeHesEvent">Event before the emission of HesEvent</param>
/// <param name="HesEvent">Event emitted when judged as hesitated</param>
/// <param name="OutEvent">Event emitted for normal execution</param>
/// <param name="AfterOutEvent">Event emitted after the finger is out</param>
/// <param name="UpdateT">Time after entering the option bubble</param>
/// <param name="WaitTime">Time waited for time delay mode</param>
/// <param name="HesTime">Time waited for Acce and Velo mode's hesitation confirmation</param>
/// <param name="vpre">Velocity for previous frame</param>
/// <param name="v">Velocity for current frame</param>
/// <param name="apre">Acceleration for previous frame</param>
/// <param name="a">Acceleration for current frame</param>
/// <param name="Vb">Velocity bound for Acce and Velo mode's A C condition</param>
/// <param name="Ab">Acceleration bound for Acce mode's B condition</param>
/// <param name="Vupper">Velocity bound for Acce mode's B condition</param>
/// <param name="OutFP"></param>
/// <param name="InFP"></param>
/// <param name="Hesitated">Bool set true if judged as hesitated</param>
/// <param name="Init">Bool set true if component start its first calculation</param>
/// <param name="Prepared">Bool set true if judged as prepared</param>
/// <param name="Entered">Bool set true if finger enters the bubble (first 0 of 010)</param>
/// <param name="CloseSti">Bool set true if decides to close the Bubble's OutEvent function</param>
/// <param name="Invoked">Bool set true if the Bubble's OutEvent is functioned</param>
/// <param name="bigger">Bool set true if previous velocity is bigger than Vb</param>
/// <param name="smaller">Bool set true if previous velocity is smaller than Vb</param>
/// <param name="curBigger">Bool set true if current velocity is bigger than Vb</param>
/// <param name="IndexTipPose1">Pose for previous frame's IndexTip pose</param>
/// <param name="IndexTipPose2">Pose for current frame's IndexTip pose</param>
/// <param name="IndexDistalPose">Pose for current Distal joint index finger pose</param>
/// <param name="IndexMiddlePose">Pose for current Middle joint index finger pose</param>
/// <param name="Bounds">Object's Bounds, to manipulate the collider</param>
/// <param name="boundCenter">Object bounds' center point</param>
/// <param name="OutColor">Color set when finger is out</param>
/// <param name="InColor">Color set when finger is in</param>
/// <param name="Debugger">Dynamic linking text debugger shower: Row 1 = velocity, Row 2 = Acceleration, ABC = trigger condition</param>
/// <param name="Debugger">Bool set true if want to show debugger</param>
/// <param name="condition">Trigger condition in the IxDL</param>
public class AcceStimulate : MonoBehaviour
{
    public UnityEvent BeforeHesEvent, HesEvent, OutEvent, AfterOutEvent;
    public float UpdateT, WaitTime, PrepTime, HesTime, vpre, v, apre, a, Vb, Ab, Vupper, OutFP, InFP, ac, vc;
    public bool Hesitated, Init, Delay, Fast, Acce, Velo, Prepared, Entered, CloseSti, Invoked, bigger, smaller, curBigger;
    MixedRealityPose IndexTipPose1;
    MixedRealityPose IndexTipPose2;
    MixedRealityPose IndexDistalPose;
    MixedRealityPose IndexMiddlePose;

    Bounds Bounds;
    public Vector3 boundCenter;
    Color OutColor, InColor;
    public TMP_Text[] Debugger;
    public bool showDebug;
    public char condition;
    void Start()
    {
        condition = 'N';
        Hesitated = false;
        Init = true;
        Prepared = false;
        Entered = false;
        CloseSti = false;
        Invoked = false;
        bigger = false;
        smaller = false;
        curBigger = false;
        UpdateT = 0.0f;
        //WaitTime = 0.5f;
        HesTime = 0.1f;
        vpre = float.MaxValue;
        v = float.MaxValue;
        apre = float.MaxValue;
        a = float.MaxValue;
        if (Acce)
            Vb = 0.2f;
        else if (Velo)
            Vb = 0.2f;
        Ab = -2;
        Vupper = 1f;

        Bounds = gameObject.GetComponent<Collider>().bounds;

        // LRD
        OutColor = new Color(0.627451f, 0.7058824f, 1, 0.572549f);
        InColor = new Color(1,1,1,1);
        OutFP = 0.38f;
        InFP = 3f;

        BeforeHesEvent ??= new UnityEvent();
        HesEvent ??= new UnityEvent();
        OutEvent ??= new UnityEvent();
        AfterOutEvent ??= new UnityEvent();
    }

    void Update()
    {
        // Make sure the collider moves with the body locked transform
        Bounds.center = transform.position;
    }

    void FixedUpdate()
    {
        Bounds = gameObject.GetComponent<Collider>().bounds;
        if (HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexTip, Handedness.Any, out IndexTipPose2))
        {
            #region V and A Calculation: V is without +-, A has +- for V's tendency to change bigger or smaller
            if (Init)
            {
                Init = false;
                IndexTipPose1 = IndexTipPose2;
                return;
            }


            v = (IndexTipPose2.Position - IndexTipPose1.Position).magnitude / Time.fixedDeltaTime;
            if (v > Vb)
                curBigger = true;
            else
                curBigger = false;
            IndexTipPose1 = IndexTipPose2;
            if (vpre != float.MaxValue)
            {
                a = (v - vpre) / Time.fixedDeltaTime;
            }
            vpre = v;
            apre = a;

            //if (showDebug)
            //{
            //    Debugger[0].text = v.ToString();
            //    Debugger[1].text = a.ToString();
            //    Debugger[2].text = condition.ToString();
            //}
            #endregion

            #region 010 Behavior
            // Try get the position of Distal and Middle Joint
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexDistalJoint, Handedness.Any, out IndexDistalPose);
            HandJointUtils.TryGetJointPose(TrackedHandJoint.IndexMiddleJoint, Handedness.Any, out IndexMiddlePose);
            // Judge if the fingertip is inside
            if (Bounds.Contains(IndexTipPose2.Position) || Bounds.Contains(IndexDistalPose.Position))
            {
                Entered = true;
                // Change Outlook
                if (!CloseSti)
                    InMaterial();
                UpdateT += Time.deltaTime;

                // If already enter hesitate state, do not act more
                if (Hesitated)
                    return;
                // else, act as the set mode (Delay, Fast, Acce, or Velo)
                if (Delay || Fast)
                {
                    if (UpdateT > WaitTime)
                    {
                        // Invoke the HesEvent if time exceeds the WaitTime
                        if (!Invoked)
                        {
                            BeforeHesEvent.Invoke();
                            HesEvent.Invoke();
                        }
                        Hesitated = true;
                    }
                }
                else if (Acce)
                {
                    if (Prepared)
                    {
                        PrepTime += Time.deltaTime;
                        if (PrepTime > HesTime)
                        {
                            //Invoke the HesEvent if prepared status exceeds the HesTime
                            if (!Invoked)
                            {
                                BeforeHesEvent.Invoke();
                                HesEvent.Invoke();
                            }
                            Hesitated = true;
                        }
                        // Condition C
                        else if (smaller && curBigger)
                        {
                            condition = 'C';
                            Prepared = false;
                        }
                    }
                    // Condition A and B
                    else if (JudgeAcce())
                    {
                        PrepTime = 0;
                        Prepared = true;
                    }
                }
                else if (Velo)
                {
                    if (Prepared)
                    {
                        PrepTime += Time.deltaTime;
                        if (PrepTime > HesTime)
                        {
                            //Invoke the HesEvent if prepared status exceeds the HesTime
                            if (!Invoked)
                            {
                                BeforeHesEvent.Invoke();
                                HesEvent.Invoke();
                            }
                            Hesitated = true;
                        }
                        // Condition C
                        else if (smaller && curBigger)
                        {
                            condition = 'C';
                            Prepared = false;
                        }
                    }
                    // Condition A
                    else if (JudgeV())
                    {
                        PrepTime = 0;
                        Prepared = true;
                    }
                }

                if (curBigger)
                    bigger = true;
                else
                    smaller = true;
                //if (curBigger)
                //{
                //    bigger = true;
                //    smaller = false;
                //}
                //else
                //{
                //    smaller = true;
                //    bigger = false;
                //}
            }
            else
            {
                // After the 0 of 010, performing the 10 of 010
                if (Entered)
                {
                    // Out of the inner area
                    OutMaterial();
                    if (!CloseSti && !Invoked)
                    {
                        // Invoke the OutEvent (the 10 of 010)
                        OutEvent.Invoke();
                    }
                    AfterOutEvent.Invoke();
                }
                UpdateT = 0;
                Prepared = false;
                Entered = false;
                Hesitated = false;
                CloseSti = false;
            }
            #endregion
        }
    }

    /// <summary>
    /// Set ClostSti as true
    /// </summary>
    public void OpenCloseSti()
    {
        CloseSti = true;
    }

    /// <summary>
    /// Condition A and B for Acce mode
    /// </summary>
    /// <returns></returns>
    public bool JudgeAcce()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if ((v < Vupper && a < Ab))
            {
                ac = a;
                vc = v;
                condition = 'B';
                return true;
            }

            if (bigger && !curBigger)
            {
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Condition A for Velo mode
    /// </summary>
    /// <returns></returns>
    public bool JudgeV()
    {
        if (vpre != float.MaxValue && apre != float.MaxValue)
        {
            if (bigger && !curBigger)
            {
                condition = 'A';
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Set Invoked as true
    /// </summary>
    public void SetInvoked()
    {
        Invoked = true;
    }

    /// <summary>
    /// Set Invoked as false
    /// </summary>
    public void UnInvoked()
    {
        Invoked = false;
    }

    public void OutMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", OutColor);
        gameObject.GetComponent<MeshRenderer>().material.SetFloat("_FP", OutFP);
    }

    public void InMaterial()
    {
        gameObject.GetComponent<MeshRenderer>().material.SetColor("_Color", InColor);
        gameObject.GetComponent<MeshRenderer>().material.SetFloat("_FP", InFP);
    }
    public void showDebugger()
    {
        if (showDebug)
        {
            Debugger[0].text = "A=" + ac.ToString();
            Debugger[1].text = "V=" + vc.ToString();
            Debugger[2].text = "C=" + condition.ToString();
        }
    }
}

