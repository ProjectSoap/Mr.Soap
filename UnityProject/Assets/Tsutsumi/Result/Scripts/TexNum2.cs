using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TexNum2 : MonoBehaviour {
    public int num4keta;
    public Image keta1;
    public Image keta2;
    public Image keta3;
    public Image keta4;

    public Sprite num0;
    public Sprite num1;
    public Sprite num2;
    public Sprite num3;
    public Sprite num4;
    public Sprite num5;
    public Sprite num6;
    public Sprite num7;
    public Sprite num8;
    public Sprite num9;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetNum(int num4ketamade)
    {
        if (num4ketamade < 0 || num4ketamade > 9999)
        {
            return;
        }
        num4keta = num4ketamade;

        int ketaCount;
        int num;

        for (ketaCount = 1; ketaCount <= 4; ++ketaCount)
        {
            //桁オブジェクトに数字テクスチャセット。一度は必ず通る
            num = num4ketamade % 10;
            SetNumTex(ketaCount, num);

            //次の桁へ
            num4ketamade = num4ketamade / 10;

            //次の桁があるかチェック
            if (num4ketamade <= 0)
            {
                ketaCount++;
                break;
            }

        }
        //残った桁は無効化
        for (; ketaCount <= 4; ++ketaCount)
        {
            SetNumTex(ketaCount, -1);
        }


    }

    private void SetNumTex(int ketaNo, int num1keta)
    {
        Image ketaObj;

        //桁セレクト
        switch (ketaNo)
        {
            case 1:
                ketaObj = keta1;
                break;
            case 2:
                ketaObj = keta2;
                break;
            case 3:
                ketaObj = keta3;
                break;
            case 4:
                ketaObj = keta4;
                break;
            default:
                return;
        }

        //数字セレクト
        Sprite numTex;
        switch (num1keta)
        {
            case -1:
                numTex = null;
                break;
            case 0:
                numTex = num0;
                break;
            case 1:
                numTex = num1;
                break;
            case 2:
                numTex = num2;
                break;
            case 3:
                numTex = num3;
                break;
            case 4:
                numTex = num4;
                break;
            case 5:
                numTex = num5;
                break;
            case 6:
                numTex = num6;
                break;
            case 7:
                numTex = num7;
                break;
            case 8:
                numTex = num8;
                break;
            case 9:
                numTex = num9;
                break;
            default:
                return;
        }

        //Set
        ketaObj.sprite = numTex;

        //有効化or無効化
        if (numTex == null)
        {
            ketaObj.gameObject.SetActive(false);
        }
        else
        {
            ketaObj.gameObject.SetActive(true);
        }

    }
}
