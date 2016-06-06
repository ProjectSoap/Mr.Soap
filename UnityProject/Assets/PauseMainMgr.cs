using UnityEngine;
using System.Collections;

public class PauseMainMgr : MonoBehaviour {
    enum MainPoseState
    {
        NONE = 0,
        POSE,
        MENU,
        PLAY_RECORD
    };
    [SerializeField]
    PauseUI ui;
    [SerializeField]
    PoseMenu menu;
    [SerializeField]
    PoseMenu Ref;
    [SerializeField]
    PauseMenuMode mode;

    MainPoseState state;
	// Use this for initialization
	void Start () {
        state = MainPoseState.NONE;
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.F3))
        {
            switch (state)
            {
                case MainPoseState.NONE:
                    {
                        state = MainPoseState.POSE;
                        ui.ChangePause();
                        break;
                    }
                default :
                    {
                        //state = MainPoseState.NONE;
                        //ui.ChangePause();
                        break;
                    }
            }
        }
        if(Input.GetKeyDown(KeyCode.Return))
        {
           switch(state)
           {
               case MainPoseState.POSE:
                   {
                       switch(menu.SelectNow)
                       {
                           case 0:
                               {
                                   state = MainPoseState.NONE;
                                   ui.ChangePause();
                                   break;
                               }
                           case 1:
                               {
                                   state = MainPoseState.MENU;
                                   mode.ChangePauseMenu((int)MainPoseState.MENU);
                                   break;
                               }
                           case 2:
                               {
                                   state = MainPoseState.PLAY_RECORD;
                                   mode.ChangePauseMenu((int)MainPoseState.PLAY_RECORD);
                                   break;
                               }
                       }
                       break;
                   }
               case MainPoseState.MENU:
                   {
                       switch (Ref.SelectNow)
                       {
                           case 0:
                               {
                                   Fade.ChangeScene("Menu");
                                   break;
                               }
                           case 1:
                               {
                                   state = MainPoseState.POSE;
                                   mode.ChangePauseMenu((int)MainPoseState.POSE);
                                   break;
                               }
                       }
                       break;
                   }
               case MainPoseState.PLAY_RECORD:
                   {
                       break;
                   }
           }
        }

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            switch (state)
            {
                case MainPoseState.POSE:
                    {
                        state = MainPoseState.NONE;
                        ui.ChangePause();
                        break;
                    }
                case MainPoseState.MENU:
                    {
                        state = MainPoseState.POSE;
                        mode.ChangePauseMenu((int)MainPoseState.POSE);
                        break;
                    }
                case MainPoseState.PLAY_RECORD:
                    {
                        state = MainPoseState.POSE;
                        mode.ChangePauseMenu((int)MainPoseState.POSE);
                        break;
                    }
                default:
                    {
                        break;
                    }
            }
        }

	}
}
