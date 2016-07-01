using UnityEngine;
using System.Collections;

/****************************************************
 * CarUnit.cs
 * 　車の移動を制御するスクリプト。
 * 　CarMovePoint.csと連携して動作する。
 * 　
 * 　このスクリプトにCarMovePoint情報が保存されていない場合や
 * 　CarMovePointに次のポイントの情報がセットされていなかった場合は
 * 　このスクリプトをセットしたオブジェクトは消滅する。
 * 　
 * 　移動速度は0~60の間で設定してください。
 * 
 ****************************************************/

public class CarUnit : MonoBehaviour {
    //初期に所属する移動ポイント
    public CarMovePoint fastPoint;
   
    private CarMovePoint nowPoint;      //現在所属のポイントスクリプト
    private CarMovePoint nextPoint;     //次のポイントスクリプト
    private Vector3 directionVec;       //移動方向
    private float moveSpeed;            //現在の移動スピード

    private float rotTimeMax;           //方向転換が完了するまでの時間
    private float nowRotTime;           //方向転換中の経過時間

    private float stopTime;

    private GameObject collisionObject; //ぶつかってきたやつの情報
    private float moveStartDistance;    //ぶつかってきたやつとこの値以上の距離がとれればOK

	// Use this for initialization
	void Start () {
        //初期ポイント無いなら消す
        if (fastPoint == null)
        {
            Destroy(gameObject);
        }

        //ポイント情報取得
        nowPoint = fastPoint;
        nextPoint = nowPoint.NextPoint;

        //初期ポイントに次のポイントが指定されてなければ消す
        if (nowPoint == null || nextPoint == null){
            Destroy(gameObject);
        } 

        //初期方向計算
        directionVec = nextPoint.transform.position - nowPoint.transform.position;
        directionVec = directionVec.normalized;

        //初期位置設定
        transform.position = nowPoint.transform.position;

        //初期スピード取得
        moveSpeed = nowPoint.NEXT_CAR_SPEED;

        //test
        rotTimeMax = 1.0f;
        nowRotTime = 0.0f;
        
        //停止時間設定
        stopTime = 0.0f;

        //衝突したオブジェクトの入れ物初期化
        collisionObject = null;
        moveStartDistance = 10.0f;
        BGMManager.Instance.PlaySELoopSpatialSum("Car_Move", this.gameObject);
	}
	
	// Update is called once per frame
	void Update () {
        float moveTime = Time.deltaTime;

        //停止チェック
        if (stopTime > 0.0f)
        {
            stopTime -= Time.deltaTime;
            if (stopTime <= 0.0f)
            {
                stopTime = -0.01f;
            }
            return;
        }
        //ぶつかってきたオブジェクトがある
        if (collisionObject != null)
        {
            Vector3 tempVec = collisionObject.transform.position - transform.position;
            //十分に離れた
            if (tempVec.magnitude > moveStartDistance)
            {
                collisionObject = null;
            }
            return;

        }

        //移動
        transform.position += directionVec.normalized * moveTime * moveSpeed;
        
        //回転
        Vector3 yTopVec;
        Vector3 sideVec;
        Vector3 distanceVec;
        float rad = 0;
        yTopVec.x = yTopVec.z = 0;
        yTopVec.y = 1;

        //ポイントに近づいて来たら次のポイントへの移動方向へ徐々に転向
        nowRotTime += moveTime;
        float rotPercent = nowRotTime / rotTimeMax;
        if (rotPercent > 1.0f) rotPercent = 1.0f;
        if (rotPercent < 0.0f) rotPercent = 0.0f;

        //向かうべき方向再計算(スラープ補間込)
        distanceVec = nextPoint.transform.position - transform.position;                //今いる位置から次のポイントへの方向
        directionVec = Vector3.Slerp(directionVec, distanceVec.normalized, rotPercent); //今向いている方向から次のポイントへの方向へスラープ補間
        directionVec = directionVec.normalized;

        
        //計算に使用するために、進行方向に対して直角なベクトル計算
        sideVec = Vector3.Cross(yTopVec,directionVec);
        sideVec = sideVec.normalized;

        //Y軸の回転
        rad = Mathf.Atan2(directionVec.x, directionVec.z);
        transform.rotation = Quaternion.AngleAxis(rad / 3.1415f * 180.0f, yTopVec);
        
        //坂などに対応するため進行方向に対して直角なベクトルを軸に回転
        rad = Mathf.Atan2(-directionVec.y, 1.0f);
        transform.rotation = Quaternion.AngleAxis(rad / 3.1415f * 180.0f, sideVec) * transform.rotation;

        //切り替えチェック
        if (distanceVec.magnitude < nextPoint.GOAL_WIDTH)
        {
            //切り替え処理
            PointUpdate();

            //スラープ開始
            rotTimeMax = nowPoint.ROTATION_TIME;
            nowRotTime = 0.0f;

            //今のポイントや、次のポイントが無ければ消す。
            if (nowPoint == null || nextPoint == null){
                Destroy(gameObject);
            }
        }

        
	}

    void OnCollisionEnter(Collision col)
    {
        if(col.transform.tag == "Player"){
            collisionObject = col.gameObject;
        }
    }

    //念のため停止中かどうかチェックできる。
    public bool isStop()
    {
        if (stopTime > 0.0f)
        {
            return true;
        }
        else//stopTimeは負で行動可能だった場合
        {
            return false;
        }
    }

    //車を停止させたい時は、この関数を呼び出して秒数を入れる。
    public void SetStopTime(float stopTimeSecond)
    {
        stopTime = stopTimeSecond;
    }

    private void PointUpdate(){
        //次のポイントへ切り替え
        nowPoint = nextPoint;
        nextPoint = nowPoint.NextPoint;

        //スピード取得
        moveSpeed = nowPoint.NEXT_CAR_SPEED;
    }
}
