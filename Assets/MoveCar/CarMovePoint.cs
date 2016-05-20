using UnityEngine;
using System.Collections;

/****************************************************
 * CarMovePoint.cs
 * 　車の移動の目的地を設定するスクリプト。
 * 　CarUnit.csと連携して動作する。
 * 　
 * 　NextPointに次のポイントの情報がセットされていなかった場合は
 * 　通りがかったCarUnitは消滅するのでご注意。
 * 　
 * 　移動速度は0~60の間で設定してください。
 * 　あと次のポイントを真上に設定するのはやめて下さい。(恐らく車の姿勢がバグります)
 * 
 ****************************************************/

public class CarMovePoint : MonoBehaviour {

    public CarMovePoint NextPoint;
    public float NEXT_CAR_SPEED;    //次のポイントを目指す際の車のスピード
    public float ROTATION_TIME;     //車が次のポイントの方向へ向くまでかかる時間
    public float GOAL_WIDTH;        //この値の距離まで近づいたら車は次のポイントを目指す

	// Use this for initialization
	void Start () {
        //60以上は許さん(次のポイントを通り過ぎてしまい、ガタガタしだすので)
        if (NEXT_CAR_SPEED > 60.0f)
        {
            NEXT_CAR_SPEED = 60.0f;
        }
        //負の値も許さん(バックして永遠にポイントにたどり着かないので)
        if (NEXT_CAR_SPEED < 0)
        {
            NEXT_CAR_SPEED = 0.0f;
        }
        //時間なので負の値はNG
        if (ROTATION_TIME < 0)
        {
            ROTATION_TIME = 0.0f;
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
