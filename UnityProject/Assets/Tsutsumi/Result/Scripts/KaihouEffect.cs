using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class KaihouEffect : MonoBehaviour {

    //外部からセット
    public List<Image> effList;

    private List<float> timeList = new List<float>();

    private float ON_TIME = 0.7f;
    private float OFF_TIME = 0.0f;

	// Use this for initialization
	void Start () {
        for (int i = 0; i < effList.Count; ++i)
        {
            timeList.Add(Random.Range(0.0f, ON_TIME + OFF_TIME));
        }
	}
	
	// Update is called once per frame
	void Update () {
        float alpha;
        for (int i = 0; i < effList.Count; ++i)
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
        Image img = effList[no];
        if (img != null)
        {
            img.gameObject.SetActive(activeFlg);
        }
    }
    private void SetEffectAlpha(int no, float alpha)
    {
        Image img = effList[no];
        Color color;
        if (img != null)
        {
            color = img.color;
            color.a = alpha;
            img.color = color;
        }
    }
}
