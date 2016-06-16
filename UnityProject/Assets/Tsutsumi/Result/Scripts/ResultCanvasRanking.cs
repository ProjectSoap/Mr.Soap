using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public GameObject BackLight;
    public float colorChangeTime;
    public Color changeColor;

    private int rankNo = 0;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
        Color initColor;
        initColor.r = initColor.g = initColor.b = initColor.a = 1.0f;

        //色を変えたり戻したりする処理
        colorChangeTime += Time.deltaTime;


	}

    //ランクによってランク外を表示したりしなかったり変更する
    public void SetScoreRank(int rankIndexNo)
    {
        rankNo = rankIndexNo;

        //if(rankIndexNo)
    }

    //ポイント、0~8とランク外の9以上
    public void SetPoint(int point4keta, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        rankingList[rankIndexNo].SetNum(point4keta);
    }
    public void ChangeColor(Color color, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        rankingList[rankIndexNo].ChangeColor(color);
    }
    public void ChangeDrawSwitch(bool flg, int rankIndexNo)
    {
        //範囲チェック
        if (rankIndexNo < 0) return;
        if (rankIndexNo > rankingList.Count - 1) return;

        rankingList[rankIndexNo].gameObject.SetActive(flg);
    }
}
