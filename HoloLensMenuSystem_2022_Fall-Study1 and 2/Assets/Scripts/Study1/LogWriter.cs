using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;

public class LogWriter : MonoBehaviour
{
    private string date, filePath, fileName, fileFullPath;
    private AcceStimulate accesti;
    private FileStream fs;
    private StreamWriter sw;
    private int caseTime;
    void Start()
    {
        date = DateTime.Now.ToString("yyyyMMdd-HH-mm-ss");
        filePath = Application.persistentDataPath + "/Logs/"+date+"/";
        fileName = date + "-" + name +".csv";
        fileFullPath = filePath + fileName;
        Debug.Log(fileFullPath);
        accesti = gameObject.GetComponent<AcceStimulate>();
        caseTime = 1;
        InitialPath();

        // Start writing once entering, no need to set invoked as true because we want it record every frame inside
        accesti.InEvent.AddListener(Write);
        // Increment the case time after getting the finger out
        accesti.AfterOutEvent.AddListener(increCase);
    }

    public string FormatMsg()
    {
        return caseTime + "," + Time.time + "," + name + "," + accesti.UpdateT + "," + accesti.v.ToString() + "," + accesti.a.ToString() + "," + accesti.condition + "," + accesti.PrepTime;
    }
    public void InitialPath()
    {
        if (!Directory.Exists(filePath))
        {
            Directory.CreateDirectory(filePath);
        }
        if (!File.Exists(fileFullPath))
        {
            // Close the operator after creation
            fs = new FileStream(fileFullPath, FileMode.Create, FileAccess.Write);
            sw = new StreamWriter(fs);
            sw.WriteLine("Case,RunTime,Object,EnterTime,Velocity,Acceleration,Condition,PrepTime");
            sw.Close();
            fs.Close();
        }
    }
    public void Write()
    {
        // New an fs and sw, close an fs and sw after writing
        // because a writer and file stream will lock the memory storage so that other gameobject cannot write
        fs = new FileStream(fileFullPath, FileMode.Append, FileAccess.Write);
        sw = new StreamWriter(fs);
        string log = FormatMsg();
        sw.WriteLine(log);
        sw.Close();
        fs.Close();
    }
    public void increCase()
    {
        caseTime++;
    }
}
