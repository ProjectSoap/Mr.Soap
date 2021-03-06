﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*********************************************************
 * PlayRecordSystem(実績画面システム)
 * 
 * 実績画面内の状態遷移を制御。
 * 
 *********************************************************/

public class PlayRecordSystem : MonoBehaviour {

    private RecordObjectList recordList;
    private int recordListNum;
    private int selectLecordNo;
    private bool firstFlg;

    //外部からセットされるオブジェクト
    public SelectPointer pointerObject;
    public RecordNameSprite RecordName;
    public RecordNameSprite RecordConditionName;
    public Image ClearTextImage;
    public RecordClearSitara ClearSitara;
    public RecordClearCount ClearCount;

	// Use this for initialization
	void Start () {
        recordList = transform.FindChild("RecordObjectList").GetComponent<RecordObjectList>();
        recordListNum = recordList.GetRecordNum();
        selectLecordNo = 0;

        //獲得済み表示のオンオフ
        RecordName.SetImageSprite(0);
        RecordConditionName.SetImageSprite(0);
        ClearTextImage.enabled = true;

        selectLecordNo = 0;
        //ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        firstFlg = true;

        //戻るボタンで戻るもしくは達成したときの報酬表示
        ClearSitara.ChangeClearSitara(selectLecordNo);
        //達成率表示
        ClearCount.TextUpdate(selectLecordNo);
	}
	
	// Update is called once per frame
	void Update () {
        //初回のみ。(汚いけど許して)
        if (firstFlg == true)
        {
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
            firstFlg = false;
        }

        int noX = selectLecordNo % 10;
        int noY = selectLecordNo / 10;

        if (Input.GetKeyDown(KeyCode.RightArrow) && Fade.FadeEnd())
        {
            noX++;
            if (noX >= 10)
            {
                noX = 0;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Fade.FadeEnd())
        {
            noX--;
            if (noX < 0)
            {
                noX = 9;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Fade.FadeEnd())
        {
            noY--;
            if (noY < 0)
            {
                noY = recordListNum / 10 - 1;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Fade.FadeEnd())
        {
            noY++;
            if (noY >= recordListNum / 10)
            {
                noY = 0;   
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
	}

    private void ChangeRecord(SelectPointer.EMoveMode mode)
    {
        Vector3 pos;
        pos = recordList.GetRecordPos(selectLecordNo);
        pointerObject.MovePointer(mode, pos);

        //テキスト情報修正
        RecordConditionName.SetImageSprite(selectLecordNo);
        //開放報酬切り替え
        ClearSitara.ChangeClearSitara(selectLecordNo);
        //達成率表示
        ClearCount.TextUpdate(selectLecordNo);

        //獲得済み表示のオンオフ
        if (recordList.GetStatusLevel(selectLecordNo) == 0)
        {
            RecordName.SetImageSprite(selectLecordNo);
            RecordName.gameObject.SetActive(false);
            ClearTextImage.enabled = false;
        }
        else
        {
            RecordName.SetImageSprite(selectLecordNo);
            RecordName.gameObject.SetActive(true);
            ClearTextImage.enabled = true;
        }
    }
}
