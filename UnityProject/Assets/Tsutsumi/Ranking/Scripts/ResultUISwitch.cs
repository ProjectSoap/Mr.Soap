using UnityEngine;
using System.Collections;

public class ResultUISwitch : MonoBehaviour {

    //外部からセット
    public ResultManagerSystem resultManagerSystem;
    public GameObject YogoreKesitaKazuUI;
    public GameObject YogoreUI;
    public GameObject KakeruUI;
    public GameObject PointUI;
    public GameObject PointKeta1UI;
    public GameObject PointKeta2UI;
    public GameObject PointKeta3UI;
    public GameObject PointKeta4UI;
    public TexNum2 PointScript;
    

    private float timeCount;
    private int point;
    

	// Use this for initialization
	void Awake () {
        timeCount = 0.0f;

        //ポイントを取得して画面へ反映
        PointUI.SetActive(true);
        point = ActionRecordManager.sActionRecord.C1WashCount +
            ActionRecordManager.sActionRecord.C2WashCount +
            ActionRecordManager.sActionRecord.C3WashCount +
            ActionRecordManager.sActionRecord.C4WashCount;

        if (point > 9999) point = 9999;
        if (point < 0) point = 0;
        PointScript.SetNum(point);

	}

    void Start()
    {
        
        PointKeta1UI.SetActive(false);
        PointKeta2UI.SetActive(false);
        PointKeta3UI.SetActive(false);
        PointKeta4UI.SetActive(false);
    }

	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;

	    //汚れ消した数文字UIと汚れUIを有効化
        if(timeCount > 1.5f){
            
            if (YogoreKesitaKazuUI.activeInHierarchy == false)
            {
                YogoreKesitaKazuUI.SetActive(true);
                YogoreUI.SetActive(true);
                //SEはここでならす
            }
        }
        //桁を順番に有効化
        if (timeCount > 2.0f)
        {
            if (PointKeta1UI.activeInHierarchy == false)
            {
                PointUI.SetActive(true);
                PointKeta1UI.SetActive(true);
                PointKeta2UI.SetActive(false);
                PointKeta3UI.SetActive(false);
                PointKeta4UI.SetActive(false);
                KakeruUI.SetActive(true);
                //SE

                
                if (point < 10)
                {
                    timeCount += 1.5f;
                }
            }
        }
        if (timeCount > 2.5f)
        {
            if (point >= 10)
            {
                if (PointKeta2UI.activeInHierarchy == false)
                {
                    PointKeta2UI.SetActive(true);
                    //SE


                    if (point < 100)
                    {
                        timeCount += 1.0f;
                    }
                }
            }
        }
        if (timeCount > 3.0f)
        {
            if (point >= 100)
            {
                if (PointKeta3UI.activeInHierarchy == false)
                {
                    PointKeta3UI.SetActive(true);
                    //SE


                    if (point < 1000)
                    {
                        timeCount += 0.5f;
                    }
                }
            }
        }
        if (timeCount > 3.5f)
        {
            if (point >= 1000)
            {
                if (PointKeta4UI.activeInHierarchy == false)
                {
                    PointKeta4UI.SetActive(true);
                    //SE


                }
            }
        }

        if (timeCount > 4.0f)
        {
            resultManagerSystem.SetPointDrawEndFlg(true);
        }

    }
}
