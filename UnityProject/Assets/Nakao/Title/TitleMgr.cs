using UnityEngine;
using System.Collections;

public class TitleMgr : MonoBehaviour {


    public bool Jumpflag;
    public string JumpSceneName = null;

    private int wait;
    private int def;

    public float m_triggerTime = 10;

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
        if(Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            //Fade  fade = GameObject.Find("Fade").GetComponent<Fade>();
            //fade.ChangeScene("menu");
            BGMManager.Instance.PlaySE("Title_Start");
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

        // セーブリセット
        if (Input.GetKey(KeyCode.F12))
        {
            m_triggerTime -= Time.deltaTime;
        }
        else 
        {
            m_triggerTime = 10;
        }
        if (m_triggerTime <= 0)
        {
            SaveDataManager manager = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
            manager.Reset();
            Debug.Log("Save data reset!");
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
