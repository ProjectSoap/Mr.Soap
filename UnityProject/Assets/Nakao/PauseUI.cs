using UnityEngine;
using System.Collections;

public class PauseUI : MonoBehaviour {
    [SerializeField]
    PauseObject pausingStopUI;
    [SerializeField]
    PauseObject pausingStartUI;
    [SerializeField]
    PauseObject pauseObjects;
    public bool pausing;
    bool prevPausing;

	// Use this for initialization
	void Start () {
        pausingStartUI.PushPose();
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.F12))
        {
            //pausing = !pausing;
            
        }
        // ポーズ状態が変更されていたら、Pause/Resumeを呼び出す。
        if (prevPausing != pausing)
        {
            if (pausing) Pause();
            else Resume();
            prevPausing = pausing;
        }
	}

    public void ChangePause()
    {
        pausing = !pausing;
    }

    void Pause()
    {
        pausingStopUI.PushPose();
        pausingStartUI.PushPose();
        pauseObjects.PushPose();
    }

    void Resume()
    {
        pausingStopUI.PushPose();
        pausingStartUI.PushPose();
        pauseObjects.PushPose();
    }
}
