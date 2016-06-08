using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PauseMenuMode : MonoBehaviour {

    public List<PauseObject> pauseMenu;
    public int beforeMode;
	// Use this for initialization
	void Start () {
        pauseMenu[1].PushPose();
        pauseMenu[2].PushPose();
	}
	
    public void ChangePauseMenu(int pauseState)
    {
        switch(pauseState)
        {
            case 1:
                {
                    if(beforeMode == 2)
                    {
                        pauseMenu[0].PushPose();
                        pauseMenu[1].PushPose();
                    }
                    if (beforeMode == 3)
                    {
                        pauseMenu[0].PushPose();
                        pauseMenu[2].PushPose();
                    }
                    break;
                }
            case 2:
                {
                    pauseMenu[0].PushPose();
                    pauseMenu[1].PushPose();
                    beforeMode = pauseState;
                    break;
                }
            case 3:
                {
                    pauseMenu[0].PushPose();
                    pauseMenu[2].PushPose();
                    beforeMode = pauseState;
                    break;
                }
        }
    }
	
}
