using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {


    public bool Jumpflag;
    public string JumpSceneName = null;

    private int wait;
    private int def;
    
    /*
    // デバッグ
    void DebugInput()
    {
        if (Input.GetKeyUp(KeyCode.F12))
        {
            Application.LoadLevel("NetSelect");
        }
    }
    */
	// Use this for initialization
	void Start () {
        Application.targetFrameRate = 60;   //60flame設定
        Jumpflag = false;
        //def = GameObject.Find("Rogo").GetComponent<Rogo>().RotateSpeed;
        BGMManager.Instance.PlayBGM("Title",1.0f);
	}
	
	// Update is called once per frame
    Vector3 moveScale = new Vector3(3.0f, 3.0f, 3.0f);
    public float timer;
    public float ButtonWaitTime = 0.5f;
	void Update () {
        if(Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            //Fade  fade = GameObject.Find("Fade").GetComponent<Fade>();
            //fade.ChangeScene("menu");
            Fade.ChangeScene("Menu");
        }
        // デバッグ
       // DebugInput();

        if (timer > ButtonWaitTime)
        {
            UpdateInput();

            JumpScene();
        }
        else
        {
            timer += Time.deltaTime;
        }

    }

    void UpdateInput()
    {
        if ((Input.anyKeyDown &&
            (Input.GetKeyDown(KeyCode.Mouse0) == false) &&
            (Input.GetKeyDown(KeyCode.Mouse1) == false) &&
            (Input.GetKeyDown(KeyCode.Mouse2) == false)) && Jumpflag == false)
        {
            // ボタンを押した
            if (Jumpflag == false)
            {
                startTime = Time.timeSinceLevelLoad;
              //  BGMManager.Instance.PlaySE("se_select");
                moveScale = new Vector3(2.75f, 2.75f, 2.75f);
            }
            Jumpflag = true;
        }
    }

    void JumpScene()
    {
        if (!(Input.anyKey) && Jumpflag == true)
        {
            if (wait < 160)
            {
                wait++;
               // GameObject.Find("Rogo").GetComponent<Rogo>().RotateSpeed += wait / 15;
                if (wait == 75)
                {
                    /*
                    if (FadeObj == null)
                    {
                        FadeObj = Fade.Instantiate(prefab, JumpSceneName, 2.0f);
                    }
                     */
                }
            }
            else
            {
               // GameObject.Find("Rogo").GetComponent<Rogo>().RotateSpeed = def;
                wait = 0;
            }
        }
    }

    private Transform ToPos;
    private Transform FromPos;
    float time;
    private float startTime;

}
