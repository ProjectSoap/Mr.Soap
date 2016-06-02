using UnityEngine;
using System.Collections;


/*********************************************************
 * SaveDataManager.cs
 * 
 * セーブデータの読み込みと書き込みを一括管理する。
 * 
 *********************************************************/

public class SaveDataManager : MonoBehaviour {

    private PlayerPrefs prefs;

    public enum ESaveDataNo{
        PlayCount = 0,
        C1WashCount,
        C2WashCount,
        C3WashCount,
        C4WashCount,

        WashChainCount,
        ChachSopeCount,
        CrashCount,
        WashCarCount,
        RainPlayFlg,

        FogPlayFlg,
        WindPlayFlg,
        SekkenChanPlayFlg,
        SekkenKun0PlayFlg,
        C1HideWash,

        C2HideWash,
        C3HideWash,
        C4HideWash,
        RankingPoint1,
        RankingPoint2,

        RankingPoint3,
        RankingPoint4,
        RankingPoint5,
        RankingPoint6,
        RankingPoint7,

        RankingPoint8,
        RankingPoint9,
        RankingPoint10,

        SAVE_DATA_NUM   //セーブデータの個数
    };

    private string[] SaveDataName = 
    {
        "PlayCount",
        "C1WashCount",
        "C2WashCount",
        "C3WashCount",
        "C4WashCount",

        "WashChainCount",
        "ChachSopeCount",
        "CrashCount",
        "WashCarCount",
        "RainPlayFlg",

        "FogPlayFlg",
        "WindPlayFlg",
        "SekkenChanPlayFlg",
        "SekkenKun0PlayFlg",
        "C1HideWash",

        "C2HideWash",
        "C3HideWash",
        "C4HideWash",
        "RankingPoint1",
        "RankingPoint2",

        "RankingPoint3",
        "RankingPoint4",
        "RankingPoint5",
        "RankingPoint6",
        "RankingPoint7",

        "RankingPoint8",
        "RankingPoint9",
        "RankingPoint10",
        "none"
    };

    private int[] SaveDataMaxCount = {
        100,
        9999,
        9999,
        9999,
        9999,

        50,
        50,
        100,
        50,
        1,

        1,
        1,
        1,
        1,
        1,

        1,
        1,
        1,
        9999,
        9999,

        9999,
        9999,
        9999,
        9999,
        9999,

        9999,
        9999,
        9999,
        0,  //未使用
    };

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //セーブデータ読み込み。-1が帰ってきたら失敗。引数は呼び出しデータの種類
    public int LoadData(ESaveDataNo no)
    {
        int count = -1;

        //間違った値が来た
        if (no >= ESaveDataNo.SAVE_DATA_NUM)
        {
            return -1;
        }

        //セーブされているかどうか
        if (PlayerPrefs.HasKey(SaveDataName[(int)no]) == true)
        {
            count = PlayerPrefs.GetInt(SaveDataName[(int)no]);
            return count;
        }
        else//データがない
        {
            //セーブしてデータ作成。
            if (this.SaveData(no, 0) == true)
            {
                return 0;
            }
            else
            {
                return -1;
            }
        }
        
    }

    //セーブデータ書き込み。falseが帰ってきたら失敗。引数は書き込みデータの種類と書き込む値
    public bool SaveData(ESaveDataNo no, int data)
    {
        //間違った値が来た
        if (no >= ESaveDataNo.SAVE_DATA_NUM)
        {
            return false;
        }

        //セーブする前に値チェック
        if (data < 0)
        {
            data = 0;
        }
        if (data > SaveDataMaxCount[(int)no])
        {
            data = SaveDataMaxCount[(int)no];
        }

        //セーブする
        PlayerPrefs.SetInt(SaveDataName[(int)no], data);

        return true;
    }
}
