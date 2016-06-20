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

        pointUIPos = PointUI.transform.position;
        pointUIPos.x = 5.0f;
        pointUIPos.y = -2.0f;
        //pointDistance = Mathf.Abs(PointKeta1UI.transform.position.x - PointKeta2UI.transform.position.x);
        pointDistance = -1.5f;
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
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Result_2");
                }
            }
        }
        //桁を順番に有効化
        if (timeCount > 2.0f)
        {
            if (PointKeta1UI.activeInHierarchy == false)
            {
                PointPositionControll(1);
                PointUI.SetActive(true);
                PointKeta1UI.SetActive(true);
                PointKeta2UI.SetActive(false);
                PointKeta3UI.SetActive(false);
                PointKeta4UI.SetActive(false);
                KakeruUI.SetActive(true);
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Result_3");
                }
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
                    PointPositionControll(2);
                    PointKeta2UI.SetActive(true);
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Result_3");
                    }
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
                    PointPositionControll(3);
                    PointKeta3UI.SetActive(true);
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Result_3");
                    }
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
                    PointPositionControll(4);
                    PointKeta4UI.SetActive(true);
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Result_3");
                    }
                }
            }
        }

        if (timeCount > 4.0f)
        {
            resultManagerSystem.SetPointDrawEndFlg(true);
        }

    }


    //ポイント中央ぞろえ制御用
    private Vector3 pointUIPos;
    private float pointDistance;

    private void PointPositionControll(int keta)
    {
        Vector3 temp = pointUIPos;

        switch (keta)
        {
            case 1:
                PointKeta1UI.transform.position = temp;
                break;
            case 2:
                temp.x -= pointDistance * 0.5f;
                PointKeta1UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta2UI.transform.position = temp;
                break;
            case 3:
                temp.x -= pointDistance;
                PointKeta1UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta2UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta3UI.transform.position = temp;
                break;
            case 4:
                temp.x -= pointDistance * 0.5f;
                temp.x -= pointDistance;
                PointKeta1UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta2UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta3UI.transform.position = temp;
                temp.x += pointDistance;
                PointKeta4UI.transform.position = temp;
                break;
        }
    }
}
