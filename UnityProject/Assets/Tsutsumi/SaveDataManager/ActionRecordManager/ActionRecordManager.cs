﻿using UnityEngine;
using System.Collections;

public class ActionRecordManager : MonoBehaviour {

    //構造体定義
    public struct SActionRecord
    {
        public int C1WashCount;
        public int C2WashCount;
        public int C3WashCount;
        public int C4WashCount;

        public int WashChainCount;
        public int ChachSopeCount;
        public int CrashCount;
        public int WashCarCount;

        public bool isRain;
        public bool isFog;
        public bool isWind;

        public bool isSekkenChanPlay;
        public bool isSekkenKunPlay;

        public bool C1HideWashFlg;
        public bool C2HideWashFlg;
        public bool C3HideWashFlg;
        public bool C4HideWashFlg;

        public void Reset()
        {
            C1WashCount = 0;
            C2WashCount = 0;
            C3WashCount = 0;
            C4WashCount = 0;
            WashChainCount = 0;
            ChachSopeCount = 0;
            CrashCount = 0;
            WashCarCount = 0;

            isRain = false;
            isFog = false;
            isWind = false;

            isSekkenChanPlay = false;
            isSekkenKunPlay = false;

            C1HideWashFlg = false;
            C2HideWashFlg = false;
            C3HideWashFlg = false;
            C4HideWashFlg = false;
        }
    }

    //static変数(Scene間の橋渡し用)
    public static SActionRecord sActionRecord;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}