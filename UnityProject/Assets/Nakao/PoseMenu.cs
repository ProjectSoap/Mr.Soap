using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class PoseMenu : MonoBehaviour {

    // カーソル
    public GameObject SelectCur;
    private Vector3 OffsetScale;

    // ボタン
    public List<GameObject> Button;
    public int SelectNow;
    public int SelectOld;
    /*    
        public List<Vector3> ButtonScale;
        private bool JumpFlag;
        private bool JumpSceneFlag;
        private int JumpWait;
        public int JumpWaitMax = 5;
        // Use this for initialization
        */
    void Start()
    {
        OffsetScale = SelectCur.GetComponent<Transform>().transform.localScale;
        SelectCur.transform.position = Button[0].GetComponent<Transform>().transform.position;
    }

    void Init()
    {
        OffsetScale = SelectCur.GetComponent<Transform>().transform.localScale;
        SelectCur.transform.position = Button[0].GetComponent<Transform>().transform.position;
        SelectNow = 0;
    }

    // Update is called once per frame
    public float timer;
    public float ButtonWaitTime = 1.2f;

    void Update()
    {
        UpdateInput();
       // SelectJump();
        // ボタンの色が変わる
        ChangeMoveColor();

        // カーソルの移動
        ChangeMoveCur();

        // ボタンの拡縮
        ChangeMoveButton();
    }

    // 入力処理
    private bool controllerFlagU;       // 闇
    private bool controllerFlagD;       // 闇
    private bool controllerFlagL;       // 闇
    private bool controllerFlagR;       // 闇
    private int controllerWait;
    public int controllerWaitTime = 10;
    void UpdateInput()
    {
        Color r;
        r.r = r.a = 1.0f;
        r.b = r.g = 0.0f;
        Color w = new Color(1.0f, 1.0f, 1.0f, 1.0f);
       
        if(Input.GetKeyDown(KeyCode.F12))
        {
           // Init();
        }
        
        // コントローラの上下左右押すー
        if (controllerWait < controllerWaitTime)
        {
            controllerWait++;
        }
        else
        {
            SelectJump();
           
        }

        if (Input.GetKey(KeyCode.Escape))
        {
            //Application.LoadLevel("sekTitle");
        }
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            JumpScene();
           // Application.LoadLevel("Select");
        }
    }

    public void SelectJump()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            SelectNow++;
            controllerWait = 0;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            SelectNow--;
            controllerWait = 0;
        }

        if (SelectNow > Button.Count - 1)
        {
            SelectNow = 0;
        }
        else if (SelectNow < 0)
        {
            SelectNow = Button.Count - 1;
        }
        if (SelectOld != SelectNow)
        {
            startTime = Time.timeSinceLevelLoad;
            SelectOld = SelectNow;
        }

    }

    void JumpScene()
    {
        /*
        if(JumpSceneFlag == true)
        {
            if (JumpWait < JumpWaitMax)
            {
                JumpWait++;
            }
            else
            {
                switch (SelectNow)
                {
                    case 0:
                        JumpSceneFade("NetSelect");
                        break;
                    case 1:
                        JumpSceneFade("Tuto");
                        break;
                    case 2:
                        JumpSceneFade("Option");
                        break;
                    default:
                        Debug.LogError("MenuMgr->SelectNow Error!");
                        break;
                }
            }
        }*/
    }

    public void JumpSceneFade(string scenename)
    {
        /*
        if (FadeObj == null)
        {
            FadeObj = Fade.Instantiate(prefab, scenename, 1.0f);
        }
        */
    }

    private Transform ToTrans;
    private Transform FromTrans;
    public Vector3 ScalingCur = new Vector3(1.04f, 1.04f, 1.04f);
    public float MoveCurTime = 1.0f;
    private float startTime;
    float time;
    bool sek;
    void ChangeMoveCur()
    {
        ToTrans = Button[SelectNow].GetComponent<Transform>().transform;
        
        FromTrans = SelectCur.GetComponent<Transform>().transform;
       // FromTrans.localPosition -= new Vector3(30, 0, 0);
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveCurTime;
        if (sek)
        {
            time += Time.deltaTime;
            if (time > 1.0f)
            {
                sek = !sek;
            }
        }
        else
        {
            time -= Time.deltaTime;
            if (time < 0.0f)
            {
                sek = !sek;
            }
        }
        // 移動
        SelectCur.GetComponent<Transform>().transform.localPosition = Vector3.Lerp(FromTrans.localPosition, ToTrans.localPosition + new Vector3(30,-30,0), rate);
        Quaternion rot  = Quaternion.Lerp(Quaternion.Euler(new Vector3(0,0,60)),Quaternion.Euler(new Vector3(0,0,120)),time);
        SelectCur.GetComponent<Image>().transform.rotation = rot;
        // 拡縮
        //  ToTrans = Button[SelectNow].transform.FindChild("Background").transform;
        Vector3 calcscale;
        //calcscale = Vector3.Scale(ButtonScale[SelectNow], ScalingCur);
        //        SelectCur.GetComponent<Transform>().transform.localScale = Vector3.Lerp(FromTrans.localScale, Vector3.Scale(ToTrans.localScale, calcscale), rate);

    }


    /// <summary>
    /// ボタンの拡縮
    /// </summary>
    public float MoveButtonTime = 0.4f;
    void ChangeMoveButton()
    {
        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveButtonTime;

        for (int i = 0; i < Button.Count - 1; i++)
        {
            ToTrans = Button[i].GetComponent<Transform>().transform;
            //  ToTrans.localScale = Vector3.Lerp(ToTrans.localScale, ButtonScale[i], rate);
        }
    }


    /// <summary>
    /// カラーの変更
    /// </summary>
    public Color SelectColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color NoneColor = new Color(0.3f, 0.3f, 0.3f, 1.0f);
    public float MoveColorTime = 0.8f;
    void ChangeMoveColor()
    {
        MoveColorTime = 0.8f;

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveColorTime;

        for (int i = 0; i < Button.Count - 1; i++)
        {
            //Color NowColor = Button[i].transform.FindChild("Background").GetComponent<UISprite>().color;
            if (i == SelectNow)
            {
                //  Button[i].transform.FindChild("Background").GetComponent<UISprite>().color = Color.Lerp(NowColor, SelectColor, rate);
            }
            else
            {
                //    Button[i].transform.FindChild("Background").GetComponent<UISprite>().color = Color.Lerp(NowColor, NoneColor, rate);
            }
        }
    }
}