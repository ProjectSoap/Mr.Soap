using UnityEngine;
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

    //外部からセットされるオブジェクト
    public SelectPointer pointerObject;
    public Text TRecordName;
    public Text TConditionText;
    public Image ClearTextImage;

	// Use this for initialization
	void Start () {
        recordList = transform.FindChild("RecordObjectList").GetComponent<RecordObjectList>();
        recordListNum = recordList.GetRecordNum();
        selectLecordNo = 0;


        //テキストを初期設定
        TConditionText.text = recordList.GetRecordCondtion(selectLecordNo);
        //獲得済み表示のオンオフ
        if (recordList.GetStatusLevel(selectLecordNo) == 0)
        {
            TRecordName.text = "";
            ClearTextImage.enabled = false;
        }
        else
        {
            TRecordName.text = recordList.GetRecordName(selectLecordNo);
            ClearTextImage.enabled = true;
        }

        selectLecordNo = 0;
        ChangeRecord(SelectPointer.EMoveMode.NORMAL);
	}
	
	// Update is called once per frame
	void Update () {
        int noX = selectLecordNo % 10;
        int noY = selectLecordNo / 10;

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            noX++;
            if (noX >= 10)
            {
                noX = 0;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            noX--;
            if (noX < 0)
            {
                noX = 9;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            noY--;
            if (noY < 0)
            {
                noY = recordListNum / 10 - 1;
            }
            selectLecordNo = noY * 10 + noX;
            ChangeRecord(SelectPointer.EMoveMode.NORMAL);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
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
        TConditionText.text = recordList.GetRecordCondtion(selectLecordNo);

        //獲得済み表示のオンオフ
        if (recordList.GetStatusLevel(selectLecordNo) == 0)
        {
            TRecordName.text = "";
            ClearTextImage.enabled = false;
        }
        else
        {
            TRecordName.text = recordList.GetRecordName(selectLecordNo);
            ClearTextImage.enabled = true;
        }
    }
}
