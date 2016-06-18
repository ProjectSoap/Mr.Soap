using UnityEngine;
using System.Collections;

public class ResultSekkenControll : MonoBehaviour {

    public GameObject Sekkenkun;
    public GameObject Sekkenchan;
    public GameObject Sekkenkun0;

    public int motion2Point = 50;
    public int motion3Point = 100;

    public enum ESekkenNo
    {
        No_Sekkenkun,
        No_Sekkenchan,
        No_Sekkenkun0
    };

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //スコアと使用キャラクターを元にせっけんくんを制御する。
    public void SelectSekkenAndAnimation(ESekkenNo no, int scorePoint)
    {
        string TriggerName;
        Sekkenkun.SetActive(false);
        Sekkenchan.SetActive(false);
        Sekkenkun0.SetActive(false);

        //スコアを元にアニメーション切り替え
        if (scorePoint < motion2Point)
        {
            TriggerName = "KanasimiTrig";
        }
        else
        {
            if (scorePoint < motion3Point)
            {
                TriggerName = "TereTrig";
            }
            else
            {
                TriggerName = "YorokobiTrig";
            }
        }

        //使用キャラクターによってアクティブ切り替え
        switch (no)
        {
            case ESekkenNo.No_Sekkenkun:
                Sekkenkun.SetActive(true);
                Sekkenkun.GetComponent<Animator>().SetTrigger(TriggerName);
                break;
            case ESekkenNo.No_Sekkenchan:
                Sekkenchan.SetActive(true);
                Sekkenchan.GetComponent<Animator>().SetTrigger(TriggerName);
                break;
            case ESekkenNo.No_Sekkenkun0:
                Sekkenkun0.SetActive(true);
                Sekkenkun0.GetComponent<Animator>().SetTrigger(TriggerName);
                break;
        }
    }
}
