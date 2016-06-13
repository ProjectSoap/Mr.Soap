using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*******************************************************
 * 
 * AsobikataPicMove.cs
 *  写真オブジェクト本体にセットするスクリプト。
 *  ParamObjectの情報を元に写真オブジェクトのスケール制御、位置制御を行う。
 *  上位管理クラスより表示優先度の情報が来る。
 * 
 *******************************************************/
public class AsobikataPicMove : MonoBehaviour {

    //パラメータを定義するオブジェクト(外部からセット)
    public AsobikataPicParam ParamObject;

    //スプライト
    private SpriteRenderer thisRender;

    //内部計算用パラメータ
    private AsobikataPicParam.SAsobikataPicParam oldParam;
    private AsobikataPicParam.SAsobikataPicParam nextParam;
    private Vector3 nowPos;
    private Vector3 nowScale;
    private float moveTime;
    private int objNo;
    private int objectNum;      //リストの総数

	// Use this for initialization
	void Awake () {
        thisRender = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        //タイマー増加
        moveTime += Time.deltaTime;

        //位置計算
        CalcNowParam();

        transform.position = nowPos;
        transform.localScale = nowScale;
	}

    //外部より呼ばれる。初期化処理
    public void Init(int _objNo, int selectNo, int _objectNum)
    {
        moveTime = ParamObject.moveTime + 0.5f;
        objNo = _objNo;
        objectNum = _objectNum;
        CalcNewParam(selectNo);

        oldParam = nextParam;
        nowPos.x = nextParam.PosX;
        nowPos.y = nextParam.PosY;
        nowPos.z = 0.0f;
        nowScale.x = nextParam.ScaleX;
        nowScale.y = nextParam.ScaleY;
        nowScale.z = 1.0f;

        transform.position = nowPos;
        transform.localScale = nowScale;

        thisRender.sortingOrder = objectNum - Mathf.Abs(_objNo - selectNo);
    }

    //移動をさせる処理。
    public void StartMove(int selectNo)
    {
        moveTime = 0.0f;
        oldParam = nextParam;
        thisRender.sortingOrder = objectNum - Mathf.Abs(objNo - selectNo);
        CalcNewParam(selectNo);
    }

    //今のステータスを計算してセットする。
    private void CalcNowParam()
    {
        //移動完了済みなら
        if (moveTime > ParamObject.moveTime)
        {
            nowPos.x = nextParam.PosX;
            nowPos.y = nextParam.PosY;
            nowScale.x = nextParam.ScaleX;
            nowScale.y = nextParam.ScaleY;
            return;
        }

        //移動途中なので割合計算
        float newPercent = moveTime / ParamObject.moveTime;
        float oldPercent = 1.0f - newPercent;

        nowPos.x = oldParam.PosX * oldPercent + nextParam.PosX * newPercent;
        nowPos.y = oldParam.PosY * oldPercent + nextParam.PosY * newPercent;
        nowScale.x = oldParam.ScaleX * oldPercent + nextParam.ScaleX * newPercent;
        nowScale.y = oldParam.ScaleY * oldPercent + nextParam.ScaleY * newPercent;

    }

    //次のステータスを計算してセットする。
    private void CalcNewParam(int selectNo)
    {
        //負の値なら左側、０なら中央、正の値なら右側
        int noDistance = objNo - selectNo;

        //スケールを制御
        if (noDistance == 0)
        {
            //選択されているので大きく
            nextParam.ScaleX = ParamObject.SelectScaleX;
            nextParam.ScaleY = ParamObject.SelectScaleY;
        }
        else
        {
            //選択されていないので小さく
            nextParam.ScaleX = ParamObject.NotSelectScaleX;
            nextParam.ScaleY = ParamObject.NotSelectScaleY;
        }

        //位置を制御
        if (noDistance == 0)
        {
            //選択されているので中央へ
            nextParam.PosX = 0;
            nextParam.PosY = 0;
        }
        else
        {
            //選択されていないので位置計算
            nextParam.PosX = ParamObject.NotSelectDistanceX;
            nextParam.PosY = ParamObject.NotSelectDistanceY;

            //選択を過ぎたのかまだ来てないのか
            if(noDistance < 0)
            {
                noDistance++;
                nextParam.PosX *= -1.0f;
                nextParam.PosY *= -1.0f;
            }
            else
            {
                noDistance--;
            }

            //選択番号との距離で追加計算
            nextParam.PosX += ParamObject.MoveParamX * noDistance;
            nextParam.PosY += ParamObject.MoveParamY * noDistance;
            
        }
    }
}
