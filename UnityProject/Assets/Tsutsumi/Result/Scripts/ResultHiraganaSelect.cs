using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultHiraganaSelect : MonoBehaviour {

    [SerializeField]
    private ResultHiraganaData[] gyou = new ResultHiraganaData[18];
    private int gyouNo = 0;     //ア行、カ行など行を示す番号
    private int aiueoNo = 0;    //あいうえおなど行内の母音を切り替える番号

    private ResultHiraganaData.EHiraganaType type = ResultHiraganaData.EHiraganaType.TYPE_TEXT;
    private Text selectText;

	// Use this for initialization
	void Awake () {
        aiueoNo = 0;
        gyouNo = 0;
        type = ResultHiraganaData.EHiraganaType.TYPE_TEXT;
        selectText = selectText = gyou[gyouNo].GetText(aiueoNo);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //選択
    public Text GetText()
    {
        //テキストデータスクリプトから情報取得
        selectText = gyou[gyouNo].GetText(aiueoNo);
        return selectText;
    }
    public ResultHiraganaData.EHiraganaType GetType()
    {
        //テキストデータスクリプトから情報取得
        type = gyou[gyouNo].GetType();
        return type;
    }
    public float GetScaleX()
    {
        float scaleX;
        scaleX = gyou[gyouNo].GetScale();
        return scaleX;
    }

    public Text RightSelect()
    {
        //母音番号切り替え
        aiueoNo++;

        //右の行にいっちゃう
        if (aiueoNo >= 5)
        {
            //母音番号は最初から
            aiueoNo = 0;

            //行番号切り替え
            gyouNo++;

            //もし画面右から左の行に移動するなら特例処理
            if (gyouNo % 3 == 0)
            {
                gyouNo -= 3;
            }
        }

        //テキストデータスクリプトから情報取得
        type = gyou[gyouNo].GetType();
        selectText = gyou[gyouNo].GetText(aiueoNo);

        //テキスト情報がなかった
        if (selectText == null)
        {
            //テキスト情報の入ったものが出るまで再帰処理
            selectText = RightSelect();
            return selectText;
        }
        return selectText;
    }

    public Text LeftSelect()
    {
        //母音番号切り替え
        aiueoNo--;

        //左の行にいっちゃう
        if (aiueoNo < 0)
        {
            //母音番号は最後から
            aiueoNo = 4;

            //行番号切り替え
            gyouNo--;

            //もし画面左から右の行に移動するなら特例処理
            if (gyouNo % 3 == 2)
            {
                gyouNo += 3;
            }
        }

        //テキストデータスクリプトから情報取得
        selectText = gyou[gyouNo].GetText(aiueoNo);

        //テキスト情報がなかった
        if (selectText == null)
        {
            //テキスト情報の入ったものが出るまで再帰処理
            selectText = LeftSelect();
            return selectText;
        }
        return selectText;
    }

    public Text TopSelect()
    {
        //行番号切り替え
        gyouNo -= 3;

        //もし画面上から下の行に移動するなら特例処理
        if (gyouNo < 0)
        {
            gyouNo += 18;
        }

        //テキストデータスクリプトから情報取得
        selectText = gyou[gyouNo].GetText(aiueoNo);

        //テキスト情報がなかった
        if (selectText == null)
        {
            //母音番号を中央へ
            aiueoNo = 2;
            selectText = gyou[gyouNo].GetText(aiueoNo);
            return selectText;
        }
        return selectText;
    }

    public Text DownSelect()
    {
        //行番号切り替え
        gyouNo += 3;

        //もし画面上から下の行に移動するなら特例処理
        if (gyouNo >= 18)
        {
            gyouNo -= 18;
        }

        //テキストデータスクリプトから情報取得
        selectText = gyou[gyouNo].GetText(aiueoNo);

        //テキスト情報がなかった
        if (selectText == null)
        {
            //母音番号を中央へ
            aiueoNo = 2;
            selectText = gyou[gyouNo].GetText(aiueoNo);
            return selectText;
        }
        return selectText;
    }
}
