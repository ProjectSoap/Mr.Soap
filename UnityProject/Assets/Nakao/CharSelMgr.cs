﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class CharSelMgr : MonoBehaviour {
    [SerializeField]
    PauseUI charUI;
    enum Charselect
    {
        SELECT,
        PLAY,
        FADE
    };

    [SerializeField]
    List<GameObject> soaps;

    [SerializeField]
    SelectCharacter soap;

    [SerializeField]
    SelectingCharactor no;

    Charselect state;

    [SerializeField]
    SaveDataManager save;

    [SerializeField]
    List<NoOpenCharcter> selifu;

    [SerializeField]
    Text name;

    [SerializeField]
    Text status;

	[SerializeField]
	SelectPlayMode m_playMode;
	// Use this for initialization
	void Start () {
		state = Charselect.SELECT;
        //ゲーム中の選択状態をリセット
        ActionRecordManager.sActionRecord.Reset();

        //実績解放によって表示を変更
        if (save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.no1))
        {
            soaps[3].active = false;
        }
        else
        {
            soaps[2].active = false;
        }

        if (save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.kakure1) && save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.kakure2) && save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.kakure3) && save.GetComponentInChildren<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.kakure4))
        {
            soaps[1].active = false;
        }
        else
        {
            soaps[0].active = false;
        }
        BGMManager.Instance.PlayBGM("Character_Choice_BGM", 1.0f);
	}
	
	// Update is called once per frame
	void Update () {
	    if(((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))) && Fade.FadeEnd())
        {
            switch(state)
            {
                case Charselect.SELECT:
                    {
                        //回転が終わっているかキャラが解放されているかチェック
                        if (CheckOpenChar(soap.GetCharNo()) && soap.EndRotation())
                        {
                            state = Charselect.PLAY;
                            //キャラセレクトからモードセレクトへ移行準備
                            soap.SetEnter();
                            no.SetCharNo(soap.GetCharNo());
                            //表示切替
                            charUI.ChangePause();
                            BGMManager.Instance.PlaySE("Character_Decision");
                            selifu[0].Sleep();
                            selifu[1].Sleep();
                        }
                        else
                        {
                            BGMManager.Instance.PlaySE("Not_Character_Decision");
                            if (soap.EndRotation())
                            {
                                switch (soap.GetCharNo())
                                {
                                    case 1:
                                        selifu[0].Active();
                                        selifu[1].Sleep();
                                        break;
                                    case 2:
                                        selifu[1].Active();
                                        selifu[0].Sleep();
                                        break;
                                    default:
                                        break;
                                }
                            }
                        }
                        break;
                    }
                case Charselect.PLAY:
                    {
                      
//                        Application.LoadLevel("Main");
                        state = Charselect.FADE;
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                        Fade.ChangeScene("main");
                        //プレイモードセット
						SceneData.modeSelect = (PlayModeState) m_playMode.SelectNow;

						break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            switch (state)
            {
                case Charselect.SELECT:
                    {
                        Fade.ChangeScene("Menu");
                        break;
                        
                    }
                case Charselect.PLAY:
                    {
                        state = Charselect.SELECT;
                        charUI.ChangePause();
                        break;
                    }
            }
        }

        if(state == Charselect.PLAY)
        {
            //1ｆ対応
            if(!CheckOpenChar(soap.GetCharNo()))
            {
                state = Charselect.SELECT;
                charUI.ChangePause();
            }
        }

        switch (soap.GetCharNo())
        {
            case 0:
                name.text = "せっけんくん";
                status.text = "せっけんくんは\nのうりょくが\nへいきんてきな\nキャラクターだ";
                name.color = status.color =  new Color(0.0f,0.8f,1.0f);
                selifu[0].Sleep();
                selifu[1].Sleep();
                break;
            case 1:
                if(soaps[0].active)
                {
                    name.text = "せっけんヒーロー";
                    status.text = "せっけんヒーローはスピードが\nはやいので\nテクニックがひつようだ！";
                    
                    name.color = status.color = new Color(1.0f, 0.6f, 0.0f);
                }
                else
                {
                    name.text = status.text = "";
                }
                break;
            case 2:
                if (soaps[2].active)
                {
                    name.text = "せっけんちゃん";
                    status.text = "せっけんちゃんは\nかいふくりょくがたかいが\nうけるダメージがおおきいぞ";
                    name.color = status.color = new Color(1.0f, 0.0f, 0.4f);
                }
                else
                {
                    name.text = status.text = "";
                }
                break;
            default:
                break;
        }
	}

    //キャラ解放チェック
    bool CheckOpenChar(int no)
    {
        switch(no)
        {
            case 0 :
                {
                    return true;
                }
            case 1:
                {
                    if(soaps[0].active)
                    {
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            case 2:
                {
                    if (soaps[2].active)
                    {
                        return true;
                    }
                    else
                    {
                        break;
                    }
                }
            default:
                {
                    return false;
                }
        }
        return false;
    }
}
