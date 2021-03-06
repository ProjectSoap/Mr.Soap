﻿using UnityEngine;
using System.Collections;

/*********************************************************
 * CheckRecordCondition.cs
 * 
 * セーブデータから実績が完了しているかチェックする。
 * 
 *********************************************************/


public class CheckRecordCondition : MonoBehaviour {
    //=============================定数定義=============================
    public enum ERecordName
    {
        Kaisi,
        OtosiMinarai,
        OtosiItininnmae,
        OtosiMeizin,
        OtosiTatuzin,

        OtosiKami,
        PlayMinarai,
        PlayMeizin,
        PlayTatuzin,
        ComboMinarai,

        ComboMeizin,
        ComboTatuzin,
        Hukkatu,
        Muteki,
        Shoutotu,

        Atariya,
        Sensha,
        SenshaKIWAMI,
        ame,
        kiri,

        kaze,
        no3,
        no2,
        no1,
        onnnanoko,

        seisakusha,
        kakure1,
        kakure2,
        kakure3,
        kakure4
    };

    //=============================構造体定義=============================
    public struct SCondition
    {
        public SaveDataManager.ESaveDataNo checkDataNo;    //参照する必要のあるセーブデータの種類
        public int conditionClearCount;                    //この値以上でクリアとする

        public SCondition(SaveDataManager.ESaveDataNo no, int count)
        {
            checkDataNo = no;
            conditionClearCount = count;
        }
    };


    //=============================メンバ変数定義=============================
    //セーブデータマネージャを外部からセットしてください。
    public SaveDataManager saveDataCon;
    
    private const int RECORD_NUM = 30;      //複製数
    private SCondition[] sConditionList;    //実績の条件をデータ化したもの

    //============================以下関数定義=============================
	// Use this for initialization
    void Awake() {
        //セットされていなければ自分を消去する。
        if (saveDataCon == null)
        {
            Destroy(gameObject);
        }
        
        //実績の条件を作成。
        sConditionList = new SCondition[RECORD_NUM]{
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 0),
            new SCondition(SaveDataManager.ESaveDataNo.C1WashCount, 100),
            new SCondition(SaveDataManager.ESaveDataNo.C2WashCount, 150),
            new SCondition(SaveDataManager.ESaveDataNo.C3WashCount, 150),
            new SCondition(SaveDataManager.ESaveDataNo.C4WashCount, 200),

            //いくつかのデータを参照する必要がある実績になるので個別処理を
            new SCondition(SaveDataManager.ESaveDataNo.SAVE_DATA_NUM, 1000),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 10),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 20),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 10),

            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 30),
            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.ChachSopeCount, 25),
            new SCondition(SaveDataManager.ESaveDataNo.ChachSopeCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.CrashCount, 25),

            new SCondition(SaveDataManager.ESaveDataNo.CrashCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.WashCarCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.WashCarCount, 100),
            new SCondition(SaveDataManager.ESaveDataNo.RainPlayFlg, 1),
            new SCondition(SaveDataManager.ESaveDataNo.FogPlayFlg, 1),

            new SCondition(SaveDataManager.ESaveDataNo.WindPlayFlg, 1),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint3, 81),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint2, 91),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint1, 101),
            new SCondition(SaveDataManager.ESaveDataNo.SekkenChanPlayFlg, 1),

            new SCondition(SaveDataManager.ESaveDataNo.SekkenKun0PlayFlg, 1),
            new SCondition(SaveDataManager.ESaveDataNo.C1HideWash, 1),
            new SCondition(SaveDataManager.ESaveDataNo.C2HideWash, 1),
            new SCondition(SaveDataManager.ESaveDataNo.C3HideWash, 1),
            new SCondition(SaveDataManager.ESaveDataNo.C4HideWash, 1),
        };
        
	}

    // Update is called once per frame
    void Update()
    {
	}

    //実績が達成されたかセーブデータをチェックして確認する
    public bool CheckRecordConditionClear(int recordNo)
    {
        int data = 0;

        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return false;
        }

        //セーブデータ取得と例外処理(複数の実績を見る必要があるものがある。)
        if (sConditionList[recordNo].checkDataNo != SaveDataManager.ESaveDataNo.SAVE_DATA_NUM)
        {
            data = saveDataCon.LoadData(sConditionList[recordNo].checkDataNo);
        }
        else
        {
            data = saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C1WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C2WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C3WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C4WashCount);
        }

        //クリア判定
        if (data >= sConditionList[recordNo].conditionClearCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    //実績が達成されたか引数の値をチェックして確認する
    public bool CheckRecordConditionClear(int recordNo, int score)
    {
        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return false;
        }

        //クリア判定
        if (score >= sConditionList[recordNo].conditionClearCount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool CheckRecordConditionClear(ERecordName recordNo, int score)
    {
        return CheckRecordConditionClear((int)recordNo, score);
    }
    public bool CheckRecordConditionClear(ERecordName recordNo)
    {
        return CheckRecordConditionClear((int)recordNo);
    }

    public int GetClearConditionCount(ERecordName recordNo)
    {
        if (recordNo < 0 || (int)recordNo >= RECORD_NUM)
        {
            return -1;
        }
        //クリア条件の数を取得
        return sConditionList[(int)recordNo].conditionClearCount;
    }
    public int GetNowConditionCount(ERecordName recordNo)
    {
        if (recordNo < 0 || (int)recordNo >= RECORD_NUM)
        {
            return -1;
        }
        //現在のカウントを取得
        int data = 0;
        if (sConditionList[(int)recordNo].checkDataNo != SaveDataManager.ESaveDataNo.SAVE_DATA_NUM)
        {
            data = saveDataCon.LoadData(sConditionList[(int)recordNo].checkDataNo);
        }
        else
        {
            data = saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C1WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C2WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C3WashCount);
            data += saveDataCon.LoadData(SaveDataManager.ESaveDataNo.C4WashCount);
        }
        return data;
    }
}
