using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsobikataPictureBoardManager: MonoBehaviour {
    
    public float CHANGE_TIME = 0.5f;
    public float PARAM_MOVE_Y = 15.0f;
    
    public AsobikataSideTabList sideTab;
    public List<AsobikataPictureBoard> pictureBoardList;

    private float changeTimeCount;
    private int selectTabNo = 0;
    private int selectTabNoOld = 0;
    private bool upFlg = false;

	// Use this for initialization
	void Start () {
        selectTabNoOld = 1;
        selectTabNo = 0;
        sideTab.TabSelect(selectTabNo);
        changeTimeCount = CHANGE_TIME + 0.5f;
        upFlg = false;
	}
	
	// Update is called once per frame
	void Update () {
        //tab選択
        if (changeTimeCount > CHANGE_TIME && Fade.FadeEnd())
        {
            //Tab変更でTrue
            if (PictureBoardSelect() == true)
            {
                changeTimeCount = 0.0f;
                return;
            }
        }
        else
        {
            //タブ切り替え中
            changeTimeCount += Time.deltaTime;
            PictureBoardMove();
            return;
        }

        //左右選択
        bool rightflg = false;
        bool leftflg = false;
        if (Input.GetKeyDown(KeyCode.RightArrow) && Fade.FadeEnd())
        {
            rightflg = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Fade.FadeEnd())
        {
            leftflg = true;
        }
        pictureBoardList[selectTabNo].PictureMove(rightflg, leftflg);

	}

    //Tab選択
    private bool PictureBoardSelect()
    {
        selectTabNoOld = selectTabNo;
        if (Input.GetKeyDown(KeyCode.UpArrow) && Fade.FadeEnd())
        {
            //タブ番号変更
            selectTabNo--;
            if (selectTabNo < 0)
                selectTabNo = pictureBoardList.Count - 1;

            //タブオブジェクトが受け付けなかったら戻す
            if (sideTab.TabSelect(selectTabNo) == false)
            {
                selectTabNo = selectTabNoOld;
                return false;
            }

            upFlg = true;

            //SE再生
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
            return true;
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Fade.FadeEnd())
        {
            //タブ番号変更
            selectTabNo++;
            if (selectTabNo >= pictureBoardList.Count)
                selectTabNo = 0;

            //タブオブジェクトが受け付けなかったら戻す
            if (sideTab.TabSelect(selectTabNo) == false)
            {
                selectTabNo = selectTabNoOld;
                return false;
            }

            upFlg = false;

            //SE再生
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
            return true;
        }

        return false;
    }

    //Tab移動
    private void PictureBoardMove()
    {
        Transform selectObject = pictureBoardList[selectTabNo].transform;
        Transform selectObjectOld = pictureBoardList[selectTabNoOld].transform;

        //割合計算
        float percent = changeTimeCount / CHANGE_TIME;
        if (percent > 1.0f)
            percent = 1.0f;

        //移動方向計算
        float moveY = PARAM_MOVE_Y;

        //下入力だった
        if (upFlg == false)
        {
            moveY *= -1.0f;
        }

        //位置取得
        Vector3 selectObjOldPos = selectObjectOld.position;
        Vector3 selectObjPos = selectObject.position;
        

        //初期位置へ移動
        selectObjOldPos.y = 0 + transform.position.y;
        selectObjPos.y = moveY + transform.position.y;

        //移動量計算
        moveY = moveY * percent * -1.0f;
        selectObjOldPos.y += moveY;
        selectObjPos.y += moveY;


        //移動位置セット
        selectObjectOld.position = selectObjOldPos;
        selectObject.position = selectObjPos;

    }
}
