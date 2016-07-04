using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

/************************************************
 * 
 * ResultCanvasRanking.cs
 *  ランキングのポイント表示を画面ＵＩに適用する窓口。
 *  今回のランクが１～９なら後光を光らせる。
 *  今回のランクが１０以下ならランク外表示を有効化する。
 * 
 ************************************************/

public class ResultCanvasRanking : MonoBehaviour {

    public List<TexNum> rankingList;
    public List<Text> rankingName;
    public List<Text> rankingNameBack;

    public float colorChangeTime = 1.0f;
    public Color changeColor;

    private int rankNo = 0;
    private float colorTimeCount = 0.0f;

	// Use this for initialization
	void Start () {
        colorTimeCount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Color initColor;
        float percent = 0.0f;
        initColor.r = initColor.g = initColor.b = initColor.a = 1.0f;

        //色を変えたり戻したりする処理
        colorTimeCount += Time.deltaTime;
        if (colorTimeCount > colorChangeTime) 
            colorTimeCount -= colorChangeTime;

        //percentは最終的には0~1の間になる
        percent = colorTimeCount / colorChangeTime;
        percent = percent * 2.0f;
        if (percent > 1.0f)
        {
            percent = 2.0f - percent;
        }

        //出力カラー決定
        Color drawColor;
        drawColor = changeColor * percent + initColor * (1.0f - percent);
        rankingList[rankNo].ChangeColor(drawColor);

	}

    //ランクによってランク外を表示したりしなかったり変更する
    public void SetScoreRank(int rankIndexNo)
    {
        rankNo = rankIndexNo;
    }

    //ポイント、0~8とランク外の9以上
    public void SetPoint(int point4keta, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        rankingList[rankIndexNo].SetNum(point4keta);
    }
    public void SetRankName(string _name, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingName.Count - 1) return;

        rankingName[rankIndexNo].text = _name;
        rankingNameBack[rankIndexNo].text = _name;
    }

    public void ChangeColor(Color color, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        rankingList[rankIndexNo].ChangeColor(color);
    }
    public void ChangeActive(bool flg, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        //rankingListはこのスクリプトの孫。それより一つ上の子を指す
        rankingList[rankIndexNo].transform.parent.gameObject.SetActive(flg);
    }
}
