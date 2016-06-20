using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/*********************************************************
 * RecordObject(実績の内容一つ一つを定義)
 * 
 * 実績画面にて1つの実績のパラメータなどを保持する。
 * 
 *********************************************************/

public class RecordObject : MonoBehaviour {

    public Sprite hatena;
    public Sprite bronze;
    public Sprite silver;
    public Sprite gold;

    public int statusLevel;             //未取得0 ブロンズ1 シルバー2 ゴールド3

    private Image image;

    void Awake()
    {
        image = transform.GetComponentInChildren<Image>();
    }

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        /*
        switch(statusLevel){
            case 0:
                transform.GetComponent<SpriteRenderer>().sprite = hatena;
                break;
            case 1:
                transform.GetComponent<SpriteRenderer>().sprite = bronze;
                break;
            case 2:
                transform.GetComponent<SpriteRenderer>().sprite = silver;
                break;
            case 3:
                transform.GetComponent<SpriteRenderer>().sprite = gold;
                break;
            default:
                transform.GetComponent<SpriteRenderer>().sprite = hatena;
                break;
        }
         * */
	}

    public bool SetImageTextureAndLevel(int no)
    {
        if (no < 0 || no >= 4)
        {
            return false;
        }

        //セッケンのレア度設定
        statusLevel = no;

        //画像を設定。
        switch (statusLevel)
        {
            case 0:
                image.sprite = hatena;
                break;
            case 1:
                image.sprite = bronze;
                break;
            case 2:
                image.sprite = silver;
                break;
            case 3:
                image.sprite = gold;
                break;
            default:
                image.sprite = hatena;
                break;
        }
        
        return true;
    }
    int GetStatusLevel()
    {
        return statusLevel;
    }
}
