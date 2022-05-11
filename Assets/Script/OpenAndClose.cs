using System.Collections;
using System.Collections.Generic;
using System;
using System.Net;
using UnityEngine;

public class OpenAndClose : MonoBehaviour
{ 
    public static void Go()
    {
        SceneLoader.Load(IsOpen ? "MainScene 1" : "CLOSETIME");
    } 

    public static bool IsOpen
    {
        get
        {
            DateTime n = DateTime.Now;
            // WebClient wc = new WebClient();
            //  string s = wc.DownloadString("https://xs238699.xsrv.jp/contents/MetaAteliersPICTURE/server/IsOpen.txt");
            // bool IsOpen = (s == "1" || 7 < n.Hour && n.Hour < 23);
            bool IsOpen = (/*s == "1" || */7 < n.Hour && n.Hour < 23);
            // wc.Dispose();
            return IsOpen;
        }
    }
}
