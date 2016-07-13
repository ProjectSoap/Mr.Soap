using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecordFireControll : MonoBehaviour {

    [SerializeField]
    private CheckRecordCondition recordCondition;

    [SerializeField]
    private List<GameObject> fireList;

	// Use this for initialization
	void Start () {
        bool clearFlg = true;

        for (int i = 0; i < 30; ++i)
        {
            if (recordCondition.CheckRecordConditionClear(i) == false)
            {
                clearFlg = false;
                break;
            } 
        }

        //全部クリアなら花火演出する
        if (clearFlg == true)
        {
            for (int i = 0; i < fireList.Count; ++i)
            {
                if (fireList[i] != null)
                {
                    fireList[i].SetActive(true);
                }
            }
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
