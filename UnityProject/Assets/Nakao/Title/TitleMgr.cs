using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class TitleMgr : MonoBehaviour {


    public bool Jumpflag;
    public string JumpSceneName = null;

    private int wait;
    private int def;

    public float m_triggerTime = 10;

    public UnityEngine.UI.Image m_logoImage;
    public UnityEngine.UI.Image m_logoBackImage;

    public float m_logoTime;

    public enum State
    {
        HAL_LOGO,
        TITLE
    }

    public State m_state;

    public bool quit_ques;

    public GameObject endSystem;

    public GameObject frame;
    // ボタン
    public List<GameObject> endbutton;

    public int no;

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
        Jumpflag = quit_ques = false;
        // sprite 
        m_logoBackImage = GameObject.Find("HalLogoBack").GetComponent<Image>();
        m_logoImage = GameObject.Find("HalLogo").GetComponent<Image>();
        //def = GameObject.Find("Rogo").GetComponent<Rogo>().RotateSpeed;
        frame.transform.localPosition = endbutton[1].GetComponent<Transform>().transform.localPosition;
        endSystem.SetActive(false);
    }
	
	// Update is called once per frame
    Vector3 moveScale = new Vector3(3.0f, 3.0f, 3.0f);
    public float timer;
    public float ButtonWaitTime = 0.5f;
	void Update ()
    {
        switch (m_state)
        {
            case State.HAL_LOGO:
                m_logoTime += Time.deltaTime;
                if (3 <= m_logoTime)
                {
                    m_logoBackImage.color = new UnityEngine.Color(0, 0, 0, 0);
                    m_logoImage.color = new UnityEngine.Color(0, 0, 0, 0);
                    m_state = State.TITLE;
                    BGMManager.Instance.PlayBGM("Title", 1.0f);
                }
                break;
            case State.TITLE:


                if ((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0)) && quit_ques == false )
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

                //// セーブリセット
                //if (Input.GetKey(KeyCode.F12))
                //{
                //    m_triggerTime -= Time.deltaTime;
                //}
                //else 
                //{
                //    m_triggerTime = 10;
                //}
                //if (m_triggerTime <= 0)
                //{
                //    SaveDataManager manager = GameObject.Find("SaveDataManager").GetComponent<SaveDataManager>();
                //    manager.Reset();
                //    Debug.Log("Save data reset!");
                //}
                //

                break;
            default:
                break;
        }
    }

    void UpdateInput()
    {
        if (((Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))) && Jumpflag == false)
        {
            // ボタンを押した
            if (Jumpflag == false && quit_ques == false)
            {
                startTime = Time.timeSinceLevelLoad;
              //  BGMManager.Instance.PlaySE("se_select");
                moveScale = new Vector3(2.75f, 2.75f, 2.75f);
                Jumpflag = true;
            }
            if(quit_ques)
            {
                if (no == 0)
                {
                    Application.Quit();
                }
                else
                {
                    quit_ques = false;
                    endSystem.SetActive(false);
                }
            }
            
        }

        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if (quit_ques)
            {
                quit_ques = false;
                endSystem.SetActive(false);
            }
            else
            {
                endSystem.SetActive(true);
                quit_ques = true;
            }
        }

        if(quit_ques)
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                if (no > 0)
                {
                    no--;
                }
                //                BGMManager.Instance.PlaySE("se_key_move");
  //              BGMManager.Instance.PlaySE("Cursor_Move");
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                //              BGMManager.Instance.PlaySE("se_key_move");
                if (no < 1)
                {
                    no++;
                }
//                BGMManager.Instance.PlaySE("Cursor_Move");
            }
            ChangeMoveCur();
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

    private Transform ToTrans;
    private Transform FromTrans;
    public Vector3 ScalingCur = new Vector3(1.004f, 1.004f, 1.004f);
    public float MoveCurTime = 1.0f;
    private float startTime;
    void ChangeMoveCur()
    {
        ToTrans = endbutton[no].GetComponent<Transform>().transform;
        FromTrans = frame.GetComponent<Transform>().transform;

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveCurTime;

        // 移動
        frame.GetComponent<Transform>().transform.localPosition = ToTrans.transform.localPosition;


    }

}
