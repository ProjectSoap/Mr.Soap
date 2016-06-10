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
	// Use this for initialization
	void Start () {
        state = Charselect.SELECT;
        if (PlayerPrefs.GetInt("SekkenChanPlayFlg", -1) > 0)
        {
            soaps[1].active = false;
        }
        else
        {
            soaps[0].active = false;
        }

        if (PlayerPrefs.GetInt("SekkenKun0PlayFlg", -1) > 0)
        {
            soaps[3].active = false;
        }
        else
        {
            soaps[2].active = false;
        }

	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(state)
            {
                case Charselect.SELECT:
                    {
                        if (CheckOpenChar(soap.GetCharNo()))
                        {
                            state = Charselect.PLAY;
                            charUI.ChangePause();
                        }
                        break;
                    }
                case Charselect.PLAY:
                    {
//                        Application.LoadLevel("Main");
                        state = Charselect.FADE;
                        Fade.ChangeScene("main");
                        no.SetCharNo(soap.GetCharNo());
                        break;
                    }

                default:
                    {
                        break;
                    }
            }
        }
        if(Input.GetKeyDown(KeyCode.Backspace))
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
                    if(soaps[2].active)
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
                    if (soaps[0].active)
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
