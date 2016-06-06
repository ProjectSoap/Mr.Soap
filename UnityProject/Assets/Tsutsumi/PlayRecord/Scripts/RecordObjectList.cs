using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/*********************************************************
 * RecordObjectList(実績オブジェクトをまとめる)
 * 
 * 実績画面にて1つの実績のパラメータなどを保持する。
 * 
 *********************************************************/



public class RecordObjectList : MonoBehaviour {
    //複製するプレハブの種類を指定
    public GameObject recordObjectPrefab;
    //複製したプレハブの親を指定
    public GameObject canvasRecord;
    //パーセント表示を行いたいテキストオブジェクトを外部からセットしてください。
    public Text TPercent;
    //実績をチェックするスクリプトを外部からセットしてください。
    public CheckRecordCondition recordChecker;

    //実績の名前を定義する
    private string[] RECORD_NAME_LIST = {
        "開始のせっけん",
        "落とし見習いのせっけん",
        "落とし一人前のせっけん",
        "落とし名人のせっけん",
        "落とし達人のせっけん",

        "落とし神のせっけん",
        "プレイ見習いのせっけん",
        "プレイ名人のせっけん",
        "プレイ達人のせっけん",
        "コンボ見習いのせっけん",

        "コンボ名人のせっけん",
        "コンボ達人のせっけん",
        "復活のせっけん",
        "無敵のせっけん",
        "衝突のせっけん",

        "当たり屋のせっけん",
        "洗車のせっけん",
        "洗車の極みせっけん",
        "雨のせっけん",
        "霧のせっけん",

        "風のせっけん",
        "No3のせっけん",
        "No2のせっけん",
        "No1のせっけん",
        "女の子のせっけん",

        "製作者せっけん",
        "隠れせっけん１",
        "隠れせっけん２",
        "隠れせっけん３",
        "隠れせっけん４",
    };

    //実績取得条件の説明テキスト
    private string[] RECORD_CONDITION_TEXT_LIST = {
        "ゲームを初めて起動した",
        "区間１で汚れを３０個落とした",
        "区間２で汚れを５０個落とした",
        "区間３で汚れを７０個落とした",
        "区間４で汚れを１００個落とした",

        "汚れを合計９９９９個落とした",
        "ゲームを１５回プレイした",
        "ゲームを３０回プレイした",
        "ゲームを１００回プレイした",
        "ウォッシュチェインを１０回達成した",

        "ウォッシュチェインを３０回達成した",
        "ウォッシュチェインを５０回達成した",
        "せっけんを２０回獲得した",
        "せっけんを５０回獲得した",
        "障害物に３０回ぶつかった",

        "障害物に１００回ぶつかった",
        "車の汚れを２０回落とした",
        "車の汚れを５０回落とした",
        "気候雨で遊んだ",
        "気候霧で遊んだ",

        "気候風で遊んだ",
        "ランキングで３位に入る",
        "ランキングで２位に入る",
        "ランキングで１位に入る",
        "せっけんちゃんで遊んだ",

        "せっけんくん０で遊んだ",
        "区間１でレアな汚れを落とした",
        "区間２でレアな汚れを落とした",
        "区間３でレアな汚れを落とした",
        "区間４でレアな汚れを落とした",
    };

    private int[] RECORD_STATUS_LIST = {
        1,
        1,
        1,
        1,
        2,

        3,
        1,
        1,
        2,
        1,
        
        1,
        2,
        1,
        2,
        1,

        2,
        1,
        2,
        2,
        2,

        2,
        1,
        1,
        2,
        1,

        1,
        3,
        3,
        3,
        3
    };

    private const int RECORD_NUM = 30;      //複製数
    private GameObject[] recordObjectList;  //レコードオブジェクト本体

	// Use this for initialization
	void Start () {

	    //RecordObjectを生成。
        recordObjectList = new GameObject[RECORD_NUM];

        /*
        int clearCount = 0; 
        
        //実績情報を読み込みと判断
        for (int i = 0; i < RECORD_NUM; ++i)
        {
            //実績オブジェクトをUnity上に生成。
            recordObjectList[i] = ((GameObject)Instantiate(recordObjectPrefab));
            recordObjectList[i].transform.SetParent(canvasRecord.transform);

            //実績レベルを設定。条件クリアしてなければhatena(0)にする
            if (recordChecker.CheckRecordConditionClear(i) == true)
            {
                recordObjectList[i].GetComponentInChildren<RecordObject>().SetImageTextureAndLevel(RECORD_STATUS_LIST[i]); //1,2,3
                clearCount++;
            }
            else
            {
                recordObjectList[i].GetComponentInChildren<RecordObject>().SetImageTextureAndLevel(0);
            }

            Vector2 temp;
            temp.x = 0;
            temp.y = 0;
            recordObjectList[i].GetComponent<RectTransform>().anchoredPosition = temp;
            recordObjectList[i].GetComponent<RectTransform>().sizeDelta = temp;

            //実績オブジェクトの位置を調整する
            Vector2 temp2;

            temp.x = 0.09f * (i % 10) - 0.045f + 0.1f;
            temp.y = -0.16f * (i / 10) - 0.075f + 0.75f;

            temp2.x = 0.09f * (i % 10) + 0.045f + 0.1f;
            temp2.y = -0.16f * (i / 10) + 0.075f + 0.75f;

            recordObjectList[i].GetComponent<RectTransform>().anchorMin = temp;
            recordObjectList[i].GetComponent<RectTransform>().anchorMax = temp2;
        }

        //実績開放率テキスト表示
        int percent = clearCount * 100 / 30;
        TPercent.text = "達成率" + percent.ToString() + "%！";
        */

        ReLoadRecord();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //実績の総数を知らせる。
    public int GetRecordNum()
    {
        return RECORD_NUM;
    }

    //実績が表示されている位置を調べる
    public Vector3 GetRecordPos(int recordNo)
    {
        Vector3 pos;
        pos.x = pos.y = pos.z = 0;

        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return pos;
        }
        else
        {
            return recordObjectList[recordNo].transform.position;
        }
    }

    //実績名を取得する
    public string GetRecordName(int recordNo)
    {
        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return null;
        }
        else
        {
            return RECORD_NAME_LIST[recordNo];
        }
    }

    //実績の条件文を取得する
    public string GetRecordCondtion(int recordNo)
    {
        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return null;
        }
        else
        {
            return RECORD_CONDITION_TEXT_LIST[recordNo];
        }
    }

    //実績のランクを取得する
    public int GetStatusLevel(int recordNo)
    {
        if (recordNo < 0 || recordNo >= RECORD_NUM)
        {
            return -1;
        }
        else
        {
            return recordObjectList[recordNo].GetComponent<RecordObject>().statusLevel;
        }
    }

    //実績の再読み込み
    public void ReLoadRecord()
    {
        int clearCount = 0;

        //実績情報を読み込みと判断
        for (int i = 0; i < RECORD_NUM; ++i)
        {
            //実績オブジェクトをUnity上に生成。
            recordObjectList[i] = ((GameObject)Instantiate(recordObjectPrefab));
            recordObjectList[i].transform.SetParent(canvasRecord.transform);

            //実績レベルを設定。条件クリアしてなければhatena(0)にする
            if (recordChecker.CheckRecordConditionClear(i) == true)
            {
                recordObjectList[i].GetComponentInChildren<RecordObject>().SetImageTextureAndLevel(RECORD_STATUS_LIST[i]); //1,2,3
                clearCount++;
            }
            else
            {
                recordObjectList[i].GetComponentInChildren<RecordObject>().SetImageTextureAndLevel(0);
            }

            Vector2 temp;
            temp.x = 0;
            temp.y = 0;
            recordObjectList[i].GetComponent<RectTransform>().anchoredPosition = temp;
            recordObjectList[i].GetComponent<RectTransform>().sizeDelta = temp;

            //実績オブジェクトの位置を調整する
            Vector2 temp2;

            temp.x = 0.09f * (i % 10) - 0.045f + 0.1f;
            temp.y = -0.16f * (i / 10) - 0.075f + 0.75f;

            temp2.x = 0.09f * (i % 10) + 0.045f + 0.1f;
            temp2.y = -0.16f * (i / 10) + 0.075f + 0.75f;

            recordObjectList[i].GetComponent<RectTransform>().anchorMin = temp;
            recordObjectList[i].GetComponent<RectTransform>().anchorMax = temp2;
        }

        //実績開放率テキスト表示
        int percent = clearCount * 100 / 30;
        TPercent.text = "達成率" + percent.ToString() + "%！";
    }

    
}