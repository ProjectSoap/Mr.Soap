using UnityEngine;
using System.Collections;

/*****************************************************
 * 
 * AsobikataPicParam.cs
 *  写真の移動に関するパラメーター類を定義する。
 *  写真オブジェクト本体から参照が来る。
 * 
 *****************************************************/

public class AsobikataPicParam : MonoBehaviour {

    //公開する構造体
    public struct SAsobikataPicParam
    {
        public float ScaleX;          //大きさX
        public float ScaleY;          //大きさY
        public float PosX;            //位置X
        public float PosY;            //位置Y
    };

    //公開する変数
    public float SelectScaleX = 2.0f;           //一番表の写真の大きさX
    public float SelectScaleY = 1.0f;           //一番表の写真の大きさY
    public float NotSelectScaleX = 0.5f;        //非選択時の写真の大きさX
    public float NotSelectScaleY = 0.25f;       //非選択時の写真の大きさY

    public float MoveParamX = 0.5f;             //非選択写真間の距離X
    public float MoveParamY = -0.5f;            //非選択写真間の距離Y
    public float NotSelectDistanceX = 6.0f;     //選択写真と非選択写真の距離X
    public float NotSelectDistanceY = -3.5f;    //選択写真と非選択写真の距離Y

    public float moveTime = 0.5f;               //移動にかかる時間
    

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
