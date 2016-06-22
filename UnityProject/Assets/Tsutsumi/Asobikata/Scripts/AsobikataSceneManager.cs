using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/*********************************************************
 * 
 * AsobikataSceneManager.cs
 *  遊び方シーン全体を管理するスクリプト。
 *  入力をチェックして、シーンの移動や写真の移動を伝えたりする。
 * 
 *********************************************************/
public class AsobikataSceneManager : MonoBehaviour {

    //写真のゲームオブジェクト(外部からセット)
    public AsobikataPicParam PicParam;
    public List<AsobikataPicMove> PicObjList;
    public GameObject rightArrow;
    public GameObject leftArrow;
    public GameObject rightArrowActive;
    public GameObject leftArrowActive;
    public AsobikataText asobikataTextObject;
    public AsobikataSekken asobikataSekkenObject;

    //現在選択中の写真
    private int selectPicNo = 0;
    private float selectedTime;
    private int arrowActiveSwitch;  // -1,0,1 :０で表示しない。その他は片方表示
    private int arrowSwitch;        // -1,0,1 :０で両方表示。その他は片方表示

	// Use this for initialization
	void Start () {
        selectPicNo = 0;
        selectedTime = 0.0f;
        arrowActiveSwitch = 0;
        arrowSwitch = 0;

        for (int i = 0; i < PicObjList.Count; ++i)
        {
            PicObjList[i].Init(i, 0, PicObjList.Count);
        }
        //テキストきりかえ
        asobikataTextObject.TextSwitch(selectPicNo);
        //せっけん切り替え
        asobikataSekkenObject.SetSelectNo(selectPicNo);

        //BGM再生
        if (BGMManager.Instance != null)
        {
            //名前わかり次第入れる
            //BGMManager.Instance.PlayBGM("", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
        selectedTime += Time.deltaTime;


        //現在の番号からスイッチ切り替え
        arrowSwitch = 0;
        if (selectPicNo == 0)
        {
            arrowSwitch = 1;
        }
        if (selectPicNo == PicObjList.Count - 1)
        {
            arrowSwitch = -1;
        }
        if (selectedTime > PicParam.moveTime)
        {
            //アクティブ矢印初期化
            arrowActiveSwitch = 0;

            //左右入力を確認
            if (Input.GetKeyDown(KeyCode.RightArrow) && Fade.FadeEnd())
            {
                if (selectPicNo < PicObjList.Count-1)
                {
                    selectedTime = 0.0f;
                    selectPicNo++;
                    arrowActiveSwitch = 1;
                    for (int i = 0; i < PicObjList.Count; ++i)
                    {
                        PicObjList[i].StartMove(selectPicNo);
                    }
                    //テキスト切り替え
                    asobikataTextObject.TextSwitch(selectPicNo);
                    //せっけん切り替え
                    asobikataSekkenObject.SetSelectNo(selectPicNo);

                    //SE再生
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Move");
                    }
                }
            }
            if (Input.GetKeyDown(KeyCode.LeftArrow) && Fade.FadeEnd())
            {
                if (selectPicNo > 0)
                {
                    selectedTime = 0.0f;
                    selectPicNo--;
                    arrowActiveSwitch = -1;
                    for (int i = 0; i < PicObjList.Count; ++i)
                    {
                        PicObjList[i].StartMove(selectPicNo);
                    }
                    //テキスト切り替え
                    asobikataTextObject.TextSwitch(selectPicNo);
                    //せっけん切り替え
                    asobikataSekkenObject.SetSelectNo(selectPicNo);

                    //SE再生
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Move");
                    }
                }
            }

            //戻るボタン
            if (Input.GetKeyDown(KeyCode.Escape) && Fade.FadeEnd())
            {
                //SE再生
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Cancel");
                }

                //シーン遷移開始
                Fade.ChangeScene("Menu");
            }
        }
        
        //矢印制御
        switch (arrowSwitch)
        {
            case -1:
                rightArrow.SetActive(false);
                leftArrow.SetActive(true);
                break;
            case 0:
                rightArrow.SetActive(true);
                leftArrow.SetActive(true);
                break;
            case 1:
                rightArrow.SetActive(true);
                leftArrow.SetActive(false);
                break;
        }
        switch (arrowActiveSwitch)
        {
            case -1:
                rightArrowActive.SetActive(false);
                leftArrowActive.SetActive(true);
                break;
            case 0:
                rightArrowActive.SetActive(false);
                leftArrowActive.SetActive(false);
                break;
            case 1:
                rightArrowActive.SetActive(true);
                leftArrowActive.SetActive(false);
                break;
        }
        
	}
}
