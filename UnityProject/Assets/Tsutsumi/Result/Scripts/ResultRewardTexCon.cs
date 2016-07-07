using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class ResultRewardTexCon : MonoBehaviour {
    
    //実績名テクスチャリスト
    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    Image recordNameImage;
    [SerializeField]
    CheckRecordCondition recordChecker;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool SetRecordImage(int recordNo)
    {
        if (recordNo < 0 || recordNo >= 30) return false;

        if (recordNo < 26)
        {
            recordNameImage.sprite = sprites[recordNo];
        }
        else
        {
            bool clearFlg;
            clearFlg = true;

            //隠れ石鹸全部開放したかチェック
            for (int i = 26; i < 30; ++i)
            {
                if (recordChecker.CheckRecordConditionClear(i) == false)
                {
                    clearFlg = false;
                }
            }

            //全部開放してた
            if (clearFlg == true)
            {
                recordNameImage.sprite = sprites[recordNo];
            }
            else
            {
                recordNameImage.sprite = null;
            }

        }

        //入れたスプライト情報が有効なのか無効なのか
        if (recordNameImage.sprite == null)
        {
            recordNameImage.enabled = false;
        }
        else
        {
            recordNameImage.enabled = true;
        }

        return true;
    }
}
