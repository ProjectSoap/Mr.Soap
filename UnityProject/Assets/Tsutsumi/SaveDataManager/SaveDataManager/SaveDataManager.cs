﻿using UnityEngine;
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

    public enum ESaveDataStringNo
    {
        RankingName1,
        RankingName2,
        RankingName3,
        RankingName4,
        RankingName5,

        RankingName6,
        RankingName7,
        RankingName8,
        RankingName9,
        RankingName10
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

    private string[] SaveDataStringName =
    {
        "RankingName1",
        "RankingName2",
        "RankingName3",
        "RankingName4",
        "RankingName5",

        "RankingName6",
        "RankingName7",
        "RankingName8",
        "RankingName9",
        "RankingName10"
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
        100,
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
        int _playCount = LoadData(ESaveDataNo.PlayCount);


        //初回起動ならランキングを初期化
        if (_playCount <= 0)
        {
            SaveData(ESaveDataNo.RankingPoint1, 100);
            SaveData(ESaveDataNo.RankingPoint2, 90);
            SaveData(ESaveDataNo.RankingPoint3, 80);
            SaveData(ESaveDataNo.RankingPoint4, 70);
            SaveData(ESaveDataNo.RankingPoint5, 60);

            SaveData(ESaveDataNo.RankingPoint6, 50);
            SaveData(ESaveDataNo.RankingPoint7, 40);
            SaveData(ESaveDataNo.RankingPoint8, 30);
            SaveData(ESaveDataNo.RankingPoint9, 20);
            SaveData(ESaveDataNo.RankingPoint10, 10);

            SaveData(ESaveDataStringNo.RankingName1, "ヒーロー");
            SaveData(ESaveDataStringNo.RankingName2, "せっけん");
            SaveData(ESaveDataStringNo.RankingName3, "バブル");
            SaveData(ESaveDataStringNo.RankingName4, "あわ");
            SaveData(ESaveDataStringNo.RankingName5, "シャボン");

            SaveData(ESaveDataStringNo.RankingName6, "ウォッシュ");
            SaveData(ESaveDataStringNo.RankingName7, "ママ");
            SaveData(ESaveDataStringNo.RankingName8, "パパ");
            SaveData(ESaveDataStringNo.RankingName9, "ベイビー");
            SaveData(ESaveDataStringNo.RankingName10, "ホエー");
        }
	}
    private float upCount = 0.0f;
	// Update is called once per frame
	void Update () {
        /*
        if (Input.GetKey(KeyCode.UpArrow) == true)
        {
            upCount += Time.deltaTime;

            //長押しされた
            if (upCount > 5.0f)
            {
                //最大値セーブ
                for (int i = 0; i < (int)ESaveDataNo.C1HideWash; ++i)
                {
                    SaveData((ESaveDataNo)i, SaveDataMaxCount[i]);
                }
            }
            if (upCount > 10.0f)
            {
                //最大値セーブ
                for (int i = 0; i < (int)ESaveDataNo.SAVE_DATA_NUM; ++i)
                {
                    SaveData((ESaveDataNo)i, SaveDataMaxCount[i]);
                }
                upCount = 0.0f;
            }
        }
        else
        {
            upCount = 0.0f;
        }
        */
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

    //セーブデータ読み込み。
    public string LoadData(ESaveDataStringNo no)
    {
        //セーブされているかどうか
        if (PlayerPrefs.HasKey(SaveDataStringName[(int)no]) == true)
        {
            string name = PlayerPrefs.GetString(SaveDataStringName[(int)no]);
            return name;
        }
        else//データがない
        {
            //セーブしてデータ作成。
            if (this.SaveData(no, "") == true)
            {
                return null;
            }
            else
            {
                return null;
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
    public bool SaveData(ESaveDataStringNo no, string data)
    {
        //セーブする
        PlayerPrefs.SetString(SaveDataStringName[(int)no], data);
        return true;
    }

    //セーブデータ全削除。
    public void Reset()
    {
        //全てのデータに０代入
        for (int i = 0; i < (int)ESaveDataNo.SAVE_DATA_NUM; ++i)
        {
            SaveData((ESaveDataNo)i, 0);
        }
        //ランキング初期化
        SaveData(ESaveDataNo.RankingPoint1, 100);
        SaveData(ESaveDataNo.RankingPoint2, 90);
        SaveData(ESaveDataNo.RankingPoint3, 80);
        SaveData(ESaveDataNo.RankingPoint4, 70);
        SaveData(ESaveDataNo.RankingPoint5, 60);

        SaveData(ESaveDataNo.RankingPoint6, 50);
        SaveData(ESaveDataNo.RankingPoint7, 40);
        SaveData(ESaveDataNo.RankingPoint8, 30);
        SaveData(ESaveDataNo.RankingPoint9, 20);
        SaveData(ESaveDataNo.RankingPoint10, 10);

        SaveData(ESaveDataStringNo.RankingName1, "ヒーロー");
        SaveData(ESaveDataStringNo.RankingName2, "パパ");
        SaveData(ESaveDataStringNo.RankingName3, "ママ");
        SaveData(ESaveDataStringNo.RankingName4, "ボク");
        SaveData(ESaveDataStringNo.RankingName5, "ワタシ");

        SaveData(ESaveDataStringNo.RankingName6, "シンジ");
        SaveData(ESaveDataStringNo.RankingName7, "タケシ");
        SaveData(ESaveDataStringNo.RankingName8, "ピース");
        SaveData(ESaveDataStringNo.RankingName9, "シャム");
        SaveData(ESaveDataStringNo.RankingName10, "ホエー");
    }
}
