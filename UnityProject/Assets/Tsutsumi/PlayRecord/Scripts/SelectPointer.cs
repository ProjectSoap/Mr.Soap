using UnityEngine;
using System.Collections;

/*********************************************************
 * SelectPointer
 * 
 * 実績画面で選択している実績を示す枠の制御を行う
 * 
 *********************************************************/

public class SelectPointer : MonoBehaviour
{
    public enum EMoveMode
    {
        NORMAL = 0,
        UP,
        DOWN,
        RIGHT,
        LEFT,
    };
    //目指す位置
    private EMoveMode moveMode;
    private Vector3 goalPoint;
    private float abnormalMoveTimer;    //ノーマル以外の時に使用される


	// Use this for initialization
	void Awake () {
        moveMode = EMoveMode.NORMAL;
        abnormalMoveTimer = 0.0f;
        goalPoint = transform.position;

        Vector2 temp, temp2;
        temp.x = 0.09f - 0.045f + 0.1f;
        temp.y = 0.16f - 0.075f + 0.4f;

        temp2.x = 0.09f + 0.045f + 0.1f;
        temp2.y = 0.16f + 0.075f + 0.4f;

        transform.GetComponent<RectTransform>().anchorMin = temp;
        transform.GetComponent<RectTransform>().anchorMax = temp2;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 addTempVec;
        addTempVec.x = addTempVec.y = addTempVec.z = 0.0f;
        goalPoint.z = -1.0f;

        //ノーマルモード時のみ
        if (moveMode == EMoveMode.NORMAL)
        {
            transform.position += (goalPoint - transform.position) * 0.2f;
        }

        //カーソルが上下左右に一定時間移動する
        if (abnormalMoveTimer > 0.01f)
        {
            switch (moveMode)
            {
                case EMoveMode.UP:
                    addTempVec.x = 0.0f;
                    addTempVec.y = 150.0f;
                    addTempVec.z = 0.0f;
                    break;
                case EMoveMode.DOWN:
                    addTempVec.x = 0.0f;
                    addTempVec.y = -150.0f;
                    addTempVec.z = 0.0f;
                    break;
                case EMoveMode.RIGHT:
                    addTempVec.x = 150.0f;
                    addTempVec.y = 0.0f;
                    addTempVec.z = 0.0f;
                    break;
                case EMoveMode.LEFT:
                    addTempVec.x = -150.0f;
                    addTempVec.y = 0.0f;
                    addTempVec.z = 0.0f;
                    break;
            }
            transform.position += addTempVec * Time.deltaTime;
            abnormalMoveTimer -= Time.deltaTime;

            //ノーマル状態へ復帰する時の処理
            if (abnormalMoveTimer <= 0.01f)
            {
                switch (moveMode)
                {
                    case EMoveMode.UP:
                        addTempVec.x = 0.0f;
                        addTempVec.y = -150.0f;
                        addTempVec.z = 0.0f;
                        break;
                    case EMoveMode.DOWN:
                        addTempVec.x = 0.0f;
                        addTempVec.y = 150.0f;
                        addTempVec.z = 0.0f;
                        break;
                    case EMoveMode.RIGHT:
                        addTempVec.x = -150.0f;
                        addTempVec.y = 0.0f;
                        addTempVec.z = 0.0f;
                        break;
                    case EMoveMode.LEFT:
                        addTempVec.x = 150.0f;
                        addTempVec.y = 0.0f;
                        addTempVec.z = 0.0f;
                        break;
                }
                transform.position = goalPoint + addTempVec;
                moveMode = EMoveMode.NORMAL;
                abnormalMoveTimer = 0.0f;
            }
        }
        else
        {
            //念のため
            moveMode = EMoveMode.NORMAL;
            abnormalMoveTimer = 0.0f;
        }
	}

    //カーソル移動させたいときはこれを使用してください。
    public void MovePointer(EMoveMode mode, Vector3 point)
    {
        moveMode = mode;
        goalPoint = point;

        if (moveMode != EMoveMode.NORMAL)
        {
            abnormalMoveTimer = 0.3f;
        }

        //SE
        BGMManager.Instance.PlaySE("Cursor_Move");
    }
}
