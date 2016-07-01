using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsobikataSideTabList : MonoBehaviour {

    public float PARAM_X_MOVE = 0.5f;              //X方向移動量
    public List<GameObject> sideTabList;

    //移動にかかっている時間
    private float CHANGE_TIME;      //マネージャーより取得する。
    private float moveTimeCount;    //Tab移動経過時間
    private int selectTabNoOld;     //前回の選択タブ
    private int selectTabNo;        //今回の選択タブ

	// Use this for initialization
	void Awake () {
        AsobikataPictureBoardManager parentManager;
        parentManager = transform.parent.GetComponent<AsobikataPictureBoardManager>();

        CHANGE_TIME = parentManager.CHANGE_TIME * 0.2f;
        moveTimeCount = 999.0f;
        selectTabNoOld = selectTabNo = 0;
	}
	
	// Update is called once per frame
	void Update () {
        //移動させる
        if (moveTimeCount < CHANGE_TIME)
        {
            moveTimeCount += Time.deltaTime;

            //移動割合計算
            float percent;
            percent = moveTimeCount / CHANGE_TIME;
            if (percent > 1.0f)
                percent = 1.0f;

            //前回選択されたTabを割合に基づきデフォルト位置へ移動
            Vector3 pos;
            pos = sideTabList[selectTabNoOld].transform.position;
            pos.x = transform.position.x + PARAM_X_MOVE * (1.0f - percent);
            sideTabList[selectTabNoOld].transform.position = pos;

            //選択されたTabを割合に基づき移動
            pos = sideTabList[selectTabNo].transform.position;
            pos.x = transform.position.x + PARAM_X_MOVE * percent;
            sideTabList[selectTabNo].transform.position = pos;
        }
	}

    //受付すればTrueを返す。移動中などで受け付けられないときはFalse。
    public bool TabSelect(int _selectTabNo)
    {
        //受付拒否
        if (_selectTabNo < 0 || _selectTabNo >= sideTabList.Count)
            return false;
        if (moveTimeCount < CHANGE_TIME)
            return false;

        //選択Tabを変更する
        selectTabNoOld = selectTabNo;
        selectTabNo = _selectTabNo;

        //時間初期化
        moveTimeCount = 0.0f;

        return true;
    }
}
