using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class RecordClearCount : MonoBehaviour {

    [SerializeField]
    private CheckRecordCondition checkRecord;

    [SerializeField]
    private List<bool> nonDrawFlg;

    private Text thisText;

	// Use this for initialization
	void Awake () {
        thisText = transform.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TextUpdate(int no)
    {
        int clearCount = checkRecord.GetClearConditionCount((CheckRecordCondition.ERecordName)no);
        int nowCount = checkRecord.GetNowConditionCount((CheckRecordCondition.ERecordName)no);
        
        //クリア値以上の値を表示しないように丸め込む。
        if (nowCount > clearCount)
        {
            nowCount = clearCount;
        }
        thisText.text = nowCount.ToString() + "/" + clearCount.ToString();

        if (no >= 0 && no < nonDrawFlg.Count)
        {
            if (nonDrawFlg[no] == true)
            {
                thisText.text = "";
            }
        }
    }
}
