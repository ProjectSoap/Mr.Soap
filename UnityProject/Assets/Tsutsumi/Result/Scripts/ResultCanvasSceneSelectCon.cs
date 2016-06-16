using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ResultCanvasSceneSelectCon : MonoBehaviour {

    //外部からセット
    public List<GameObject> WakuPosList;
    public GameObject WakuMove;

    //設定
    public float MovePercent = 0.05f;
    public float RotTime = 2.0f;
    public float RotSize = 30;

    //内部計算用
    private int selectNo = 0;
    private float rotTimeCount = 0;

	// Use this for initialization
	void Start () {
        selectNo = 0;
        rotTimeCount = 0;
        WakuSelect(true);
	}
	
	// Update is called once per frame
	void Update () {
        if (selectNo < 0 || selectNo >= WakuPosList.Count) 
            return;

        //枠を割合毎に移動する
        WakuMove.transform.position = WakuMove.transform.position * (1.0f - MovePercent)
            + WakuPosList[selectNo].transform.position * MovePercent;

        //枠の回転
        rotTimeCount += Time.deltaTime;
        if(rotTimeCount > RotTime)
            rotTimeCount -= RotTime;

        //percentを-1~1の間に変更
        float percent = rotTimeCount / RotTime;
        percent *= 2.0f;
        if (percent > 1.0f)
            percent = 2.0f - percent;

        Vector3 axisRot = Vector3.zero;
        axisRot.z = 1.0f;
        WakuMove.transform.rotation = Quaternion.Euler(0, 0, 90 - RotSize * 0.5f + RotSize * percent);
	}

    public void WakuSelect(bool waku1Flg)
    {
        if (waku1Flg == true)
        {
            selectNo = 0;
        }
        else
        {
            selectNo = 1;
        }
    }
}
