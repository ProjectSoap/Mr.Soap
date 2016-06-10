using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class KaihouEffect : MonoBehaviour {

    //外部からセット
    public Image eff0;
    public Image eff1;
    public Image eff2;
    public Image eff3;
    public Image eff4;
    public Image eff5;
    public Image eff6;
    public Image eff7;

    private float[] timeList = new float[8];

    private float ON_TIME = 0.7f;
    private float OFF_TIME = 0.0f;

	// Use this for initialization
	void Start () {
        
        for (int i = 0; i < 8; ++i)
        {
            timeList[i] = Random.Range(0.0f, ON_TIME + OFF_TIME);
        }
	}
	
	// Update is called once per frame
	void Update () {
        float alpha;
        for (int i = 0; i < 8; ++i)
        {
            timeList[i] += Time.deltaTime;
            if (timeList[i] > ON_TIME + OFF_TIME)
            {
                timeList[i] -= ON_TIME + OFF_TIME + Random.Range(0.0f, 0.1f);
            }

            if (timeList[i] > 0.0f && timeList[i] < ON_TIME)
            {
                SetEffectActiveFlg(i, true);
                //透過計算
                if (timeList[i] > ON_TIME / 4.0f)
                {
                    alpha = 1.0f - ((timeList[i] - (ON_TIME / 4.0f)) / (ON_TIME - ON_TIME / 4.0f));
                }
                else
                {
                    alpha = 1.0f;
                }
                SetEffectAlpha(i, alpha);

            }
            else
            {
                SetEffectActiveFlg(i, false);
            }
        }
	}

    private void SetEffectActiveFlg(int no, bool activeFlg)
    {
        Image img = GetNoImage(no);
        if (img != null)
        {
            img.gameObject.SetActive(activeFlg);
        }
    }
    private void SetEffectAlpha(int no, float alpha)
    {
        Image img = GetNoImage(no);
        Color color;
        if (img != null)
        {
            color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }

    private Image GetNoImage(int no)
    {
        switch (no)
        {
            case 0:
                return eff0;
            case 1:
                return eff1;
            case 2:
                return eff2;
            case 3:
                return eff3;
            case 4:
                return eff4;
            case 5:
                return eff5;
            case 6:
                return eff6;
            case 7:
                return eff7;
            default:
                return null;
        }
    }
}
