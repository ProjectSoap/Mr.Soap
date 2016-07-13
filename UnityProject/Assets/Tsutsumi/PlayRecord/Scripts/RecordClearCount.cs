using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecordClearCount : MonoBehaviour {

    [SerializeField]
    private CheckRecordCondition checkRecord;

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
        thisText.text = nowCount.ToString() + "/" + clearCount.ToString();
    }
}
