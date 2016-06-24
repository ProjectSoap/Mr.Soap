using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class SekkenSayUI : MonoBehaviour {

    //せっけんのセリフの種類
    public enum ESayTexName
    {
        SEKKEN_POP, //せっけんがでてきた
        RECOVERY,   //回復した
        RAIN,       //あめだ
        WIND,       //かぜがふいてきた
        FOG,        //まえがみえない
        CRASH,      //いてて
        BARRICADE   //これいじょうすすめないよ
    };
    //選択したセッケンキャラ
    public enum ESekkenNo
    {
        SekkenKun,
        SekkenChan,
        SekkenHero
    };

    [SerializeField, Tooltip("セリフを表示する時間")]
    private float drawTime = 3.0f;
    
    [SerializeField, Tooltip("せっけんくんのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
    private List<Sprite> sekkenKunSpriteList;
    [SerializeField, Tooltip("せっけんちゃんのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
    private List<Sprite> sekkenChanSpriteList;
    [SerializeField, Tooltip("せっけんヒーローのセリフテクスチャのリスト。ESayTexNameの順番にセットしてほしい")]
    private List<Sprite> sekkenHeroSpriteList;

    [SerializeField, Tooltip("せっけんのセリフオブジェクトをセットしてほしい")]
    private Image img;

    //表示時間保存
    private float drawTimeCount;
    //選択したセッケンキャラクタ番号
    private ESekkenNo sekkenNo = ESekkenNo.SekkenKun;

	// Use this for initialization
	void Start () {
        drawTimeCount = 999.0f;
	}
	
	// Update is called once per frame
	void Update () {
        //指定時間が経過していれば表示を止める
        if (drawTimeCount > drawTime)
        {
            img.gameObject.SetActive(false);
            return;
        }

        //タイマを進める
        drawTimeCount += Time.deltaTime;

        //表示
        img.gameObject.SetActive(true);
	}

    //せっけんくんのセリフを表示するにはここを一度呼び出して
    public void DrawSayTexture(ESayTexName eSayTexNo){
        //表示カウント初期化
        drawTimeCount = 0.0f;

        //表示テクスチャの切り替え
        switch(sekkenNo){
            case ESekkenNo.SekkenKun:
                img.sprite = sekkenKunSpriteList[(int)eSayTexNo];
                break;
            case ESekkenNo.SekkenChan:
                img.sprite = sekkenChanSpriteList[(int)eSayTexNo];
                break;
            case ESekkenNo.SekkenHero:
                img.sprite = sekkenHeroSpriteList[(int)eSayTexNo];
                break;
        }
        
    }

    //セリフをせっけん毎に切り替える処理
    public void ChangeSekken(ESekkenNo _no)
    {
        sekkenNo = _no;
    }
}
