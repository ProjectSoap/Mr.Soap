using UnityEngine;
using System.Collections;

/*********************************************************
 * CheckRecordCondition.cs
 * 
 * セーブデータから実績が完了しているかチェックする。
 * 
 *********************************************************/


public class CheckRecordCondition : MonoBehaviour {
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
            new SCondition(SaveDataManager.ESaveDataNo.C1WashCount, 30),
            new SCondition(SaveDataManager.ESaveDataNo.C2WashCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.C3WashCount, 70),
            new SCondition(SaveDataManager.ESaveDataNo.C4WashCount, 100),

            //いくつかのデータを参照する必要がある実績になるので個別処理を
            new SCondition(SaveDataManager.ESaveDataNo.SAVE_DATA_NUM, 9999),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 15),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 30),
            new SCondition(SaveDataManager.ESaveDataNo.PlayCount, 100),
            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 10),

            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 30),
            new SCondition(SaveDataManager.ESaveDataNo.WashChainCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.ChachSopeCount, 20),
            new SCondition(SaveDataManager.ESaveDataNo.ChachSopeCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.CrashCount, 20),

            new SCondition(SaveDataManager.ESaveDataNo.CrashCount, 100),
            new SCondition(SaveDataManager.ESaveDataNo.WashCarCount, 20),
            new SCondition(SaveDataManager.ESaveDataNo.WashCarCount, 50),
            new SCondition(SaveDataManager.ESaveDataNo.RainPlayFlg, 1),
            new SCondition(SaveDataManager.ESaveDataNo.FogPlayFlg, 1),

            new SCondition(SaveDataManager.ESaveDataNo.WindPlayFlg, 1),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint3, 301),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint2, 501),
            new SCondition(SaveDataManager.ESaveDataNo.RankingPoint1, 1001),
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
}
