using UnityEngine;
using System.Collections;

public class CharSelMgr : MonoBehaviour {
    [SerializeField]
    PauseUI charUI;
    enum Charselect
    {
        SELECT,
        PLAY
    };
    Charselect state;
	// Use this for initialization
	void Start () {
        state = Charselect.SELECT;
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Return))
        {
            switch(state)
            {
                case Charselect.SELECT:
                    {
                        state = Charselect.PLAY;
                        charUI.ChangePause();
                        break;
                    }
                case Charselect.PLAY:
                    {
//                        Application.LoadLevel("Main");
                        Fade.ChangeScene("main");
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
}
