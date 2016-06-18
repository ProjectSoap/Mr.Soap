using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectPlayMode : MonoBehaviour
{

    // カーソル
    public GameObject SelectCur;
    private Vector3 OffsetScale;

    // ボタン
    public List<GameObject> Button;
    public List<Image> Key;
    public int SelectNow;
    public int SelectOld;


    public Vector2 ButtonScale;
    /*
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

    // Update is called once per frame
    public float timer;
    public float ButtonWaitTime = 1.2f;

    void Update()
    {
        UpdateInput();
        SelectJump();
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

        if (Fade.FadeEnd())
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                //       Key[0].color = r;
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                //     Key[1].color = r;
            }

            // コントローラの上下左右押すー
            if (controllerWait < controllerWaitTime)
            {
                controllerWait++;
            }
            else
            {
                // Key[0].color=r;
                if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    if(SelectNow>0)
                    {
                        SelectNow--;
                    }
                    //                BGMManager.Instance.PlaySE("se_key_move");
                    controllerFlagU = true;
                    controllerFlagD = false;
                    controllerWait = 0;
                }
                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    //              BGMManager.Instance.PlaySE("se_key_move");
                    if(SelectNow<1)
                    {
                        SelectNow++;
                    }

                    controllerFlagU = false;
                    controllerFlagD = true;
                    controllerWait = 0;
                }
                else if (Input.GetKeyDown(KeyCode.LeftArrow))
                {
                    controllerWait = 0;
                }

                else if (Input.GetKeyDown(KeyCode.RightArrow))
                {
                    controllerWait = 0;
                }

                if (Input.GetAxisRaw("Horizontal") > 0.9f && controllerFlagL == false)
                {
                    //            BGMManager.Instance.PlaySE("se_key_move");
                    controllerFlagL = true;
                    controllerFlagR = false;
                    controllerWait = 0;
                }
                else if (Input.GetAxisRaw("Horizontal") < -0.9f && controllerFlagR == false)
                {
                    //          BGMManager.Instance.PlaySE("se_key_move");
                    controllerFlagL = false;
                    controllerFlagR = true;
                    controllerWait = 0;
                }
            }
        }

    }

    

    public void SelectJump()
    {
        /*
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            SelectNow++;
        }
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            SelectNow--;
        }
       
        if(SelectNow>Button.Count-1)
        {
            SelectNow = 0;
        }
        else if(SelectNow<0)
        {
            SelectNow = Button.Count - 1;
        }
         * */

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
        }
        */
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
    public Vector3 ScalingCur = new Vector3(1.004f, 1.004f, 1.004f);
    public float MoveCurTime = 1.0f;
    private float startTime;
    void ChangeMoveCur()
    {
        ToTrans = Button[SelectNow].GetComponent<Transform>().transform;
        FromTrans = SelectCur.GetComponent<Transform>().transform;

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveCurTime;

        // 移動
        SelectCur.GetComponent<Transform>().transform.localPosition = Vector3.Lerp(FromTrans.localPosition, ToTrans.localPosition, rate);

       
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
            Vector2 scale = new Vector2(10, 10);
            ButtonScale = Button[SelectNow].GetComponent<Image>().rectTransform.sizeDelta + scale;
            Key[0].GetComponent<Image>().rectTransform.sizeDelta = Vector2.Lerp(ToTrans.localScale, ButtonScale, rate);
        }
    }


    /// <summary>
    /// カラーの変更
    /// </summary>
    public Color SelectColor = new Color(1.0f, 1.0f, 1.0f, 1.0f);
    public Color NoneColor = new Color(0.3f, 0.3f, 0.3f, 0.5f);
    public float MoveColorTime = 0.8f;
    void ChangeMoveColor()
    {
        MoveColorTime = 0.8f;

        var diff = Time.timeSinceLevelLoad - startTime;
        var rate = diff / MoveColorTime;
        Color NowColor = Key[0].GetComponent<Image>().color;
        Key[0].GetComponent<Image>().color = Color.Lerp(NowColor, NoneColor, rate);
        for (int i = 0; i < Button.Count; i++)
        {
            NowColor = Button[i].GetComponent<Image>().color;


            if (i == SelectNow)
            {
                Button[i].GetComponent<Image>().color = Color.Lerp(NowColor, SelectColor, rate);
            }
            else
            {
                Button[i].GetComponent<Image>().color = Color.Lerp(NowColor, NoneColor, rate);
            }
        }
    }
}
