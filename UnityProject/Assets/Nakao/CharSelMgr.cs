using UnityEngine;
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
	SelectPlayMode m_playMode;
	// Use this for initialization
	void Start () {
		state = Charselect.SELECT;

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
                        if (CheckOpenChar(soap.GetCharNo()) && soap.EndRotation())
                        {
                            state = Charselect.PLAY;
                            soap.SetEnter();
                            charUI.ChangePause();
                            BGMManager.Instance.PlaySE("Character_Decision");
                        }
                        else
                        {
                            BGMManager.Instance.PlaySE("Not_Character_Decision");
                        }
                        break;
                    }
                case Charselect.PLAY:
                    {
                        if(!CheckOpenChar(soap.GetCharNo()))
                        {
                            state = Charselect.SELECT;
                            charUI.ChangePause();
                        }
//                        Application.LoadLevel("Main");
                        state = Charselect.FADE;
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                        Fade.ChangeScene("main");
                        no.SetCharNo(soap.GetCharNo());
						no.PlayMode = (PlayModeState) m_playMode.SelectNow;

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
            if(!CheckOpenChar(soap.GetCharNo()))
            {
                state = Charselect.SELECT;
                charUI.ChangePause();
            }
        }
	}

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
