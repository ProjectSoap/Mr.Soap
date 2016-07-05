using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ResultHiraganaData : MonoBehaviour {

    public enum EHiraganaType
    {
        TYPE_TEXT,      //通常のひらがな
        TYPE_END,       //終了
        TYPE_BACKSPACE  //けす
    };

    //一行分のひらがなが入る
    [SerializeField]
    private Text[] hiragana = new Text[5];

    [SerializeField]
    private float ScaleX = 1.0f;

    [SerializeField]
    private EHiraganaType type = EHiraganaType.TYPE_TEXT;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //テキスト情報
    public Text GetText(int no)
    {
        //ヌルチェック
        if (hiragana[no] == null || no < 0 || no >= 5)
            return null;
        
        //オブジェクトを渡す
        return hiragana[no];
    }

    //スケール情報取得
    public float GetScale()
    {
        return ScaleX;
    }

    //種類情報取得
    public EHiraganaType GetType()
    {
        return type;
    }
}
