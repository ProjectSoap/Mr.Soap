using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultRankingNameSystem : MonoBehaviour {

    [SerializeField]
    private float ANDER_BAR_TIME = 1.0f;
    private float anderBarTimeCount = 0.0f;

    [SerializeField]
    private Color SelectTextColor;
    private Color NonSelectTextColor;

    [SerializeField]
    private GameObject cursor;

    [SerializeField]
    private ResultHiraganaSelect hiraganaSelect;

    [SerializeField]
    private Text InputString;

    [SerializeField]
    private SaveDataManager saveDataManager;
    
    //Hiraganaから送られるテキスト情報の入れ物
    private Text SelectText;
    private ResultHiraganaData.EHiraganaType type;

    //入力文字の長さを検出
    private int textLength = 0;

    //今回のランクを外部からセット
    private SaveDataManager.ESaveDataStringNo rank;

    private bool StartChecker = false;
	// Use this for initialization
	void Start () {
        anderBarTimeCount = 0.0f;
        SelectText = hiraganaSelect.GetText();
        NonSelectTextColor = SelectText.color;
        SelectText.color = SelectTextColor;
        textLength = 0;
        StartChecker = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void SetRank(int _rank)
    {
        rank = (SaveDataManager.ESaveDataStringNo)_rank;
    }


    //trueで終了押された
    public bool Select()
    {
        if (StartChecker == false)
        {
            return false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) && Fade.FadeEnd())
        {
            //色制御と選択
            SelectText.color = NonSelectTextColor;
            SelectText = hiraganaSelect.RightSelect();
            NonSelectTextColor = SelectText.color;
            SelectText.color = SelectTextColor;

            type = hiraganaSelect.GetType();

            //SE
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow) && Fade.FadeEnd())
        {
            //色制御と選択
            SelectText.color = NonSelectTextColor;
            SelectText = hiraganaSelect.LeftSelect();
            NonSelectTextColor = SelectText.color;
            SelectText.color = SelectTextColor;

            type = hiraganaSelect.GetType();

            //SE
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
        }
        if (Input.GetKeyDown(KeyCode.UpArrow) && Fade.FadeEnd())
        {
            //色制御と選択
            SelectText.color = NonSelectTextColor;
            SelectText = hiraganaSelect.TopSelect();
            NonSelectTextColor = SelectText.color;
            SelectText.color = SelectTextColor;

            type = hiraganaSelect.GetType();

            //SE
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && Fade.FadeEnd())
        {
            //色制御と選択
            SelectText.color = NonSelectTextColor;
            SelectText = hiraganaSelect.DownSelect();
            NonSelectTextColor = SelectText.color;
            SelectText.color = SelectTextColor;

            type = hiraganaSelect.GetType();

            //SE
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Move");
            }
        }

        //カーソルスケール変更
        Vector3 scale = cursor.transform.localScale;
        scale.x = hiraganaSelect.GetScaleX();
        cursor.transform.localScale = scale;

        //カーソル位置変更
        cursor.transform.position = SelectText.transform.position;

        //アンダーバー制御
        if (textLength < 5)
        {
            InputString.text = InputString.text.Replace("＿", "");
            anderBarTimeCount += Time.deltaTime;

            if (anderBarTimeCount < ANDER_BAR_TIME * 0.5f)
            {
                InputString.text = InputString.text + "＿";
            }
            if (anderBarTimeCount > ANDER_BAR_TIME)
            {
                anderBarTimeCount = 0.0f;
            }

        }

        //戻るキー押された
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            //テキスト一文字削除
            if (textLength > 0)
            {
                InputString.text = InputString.text.Replace("＿", "");
                InputString.text = InputString.text.Remove(textLength - 1);
                textLength--;
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Cancel");
                }
            }
        }

        //決定キー押された
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
        {
            switch (type)
            {
            case ResultHiraganaData.EHiraganaType.TYPE_TEXT:
                if(InputString != null && textLength < 5)
                {
                    InputString.text = InputString.text.Replace("＿", "");
                    InputString.text = InputString.text + SelectText.text;
                    textLength++;
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                    }
                }
                break;

            case ResultHiraganaData.EHiraganaType.TYPE_BACKSPACE:
                //テキスト一文字削除
                if (textLength > 0)
                {
                    InputString.text = InputString.text.Replace("＿", "");
                    InputString.text = InputString.text.Remove(textLength - 1);
                    textLength--;
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Cancel");
                    }
                }
                break;

            case ResultHiraganaData.EHiraganaType.TYPE_END:
                //テキスト入力終了(１文字以上ある場合)
                if (textLength > 0)
                {
                    InputString.text = InputString.text.Replace("＿", "");
                    SaveName();
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                    }
                    return true;
                }
                else
                {
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Cancel");
                    }
                }
                break;
            }
        }

        return false;
        
    }

    private void SaveName()
    {
        saveDataManager.SaveData(rank, InputString.text);
    }

}
