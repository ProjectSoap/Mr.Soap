using UnityEngine;
using System.Collections;

//リザルトでタイトルの大きさを変える処理
public class ResultTextScript : MonoBehaviour {

    public float animationTime;
    public float minPercent;

    private float timeCount;
    private bool se1Trig;
	// Use this for initialization
	void Start () {
        timeCount = 0.0f;
        se1Trig = true;
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 scale;
        float scaleSize = 0.0f;
        float upPercent;
        scale.x = scale.y = scale.z = 1.0f;

        //縮尺の度合い計算
        timeCount += Time.deltaTime;
        upPercent = timeCount / animationTime;
        if (upPercent > 1.0f)
        {
            if (se1Trig == true)
            {
                se1Trig = false;
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Result_1");
                }
            }
            upPercent = 1.0f;
        }
        scaleSize = minPercent * upPercent + 1.0f * (1.0f-upPercent);

        //スケールへ反映
        scale.x = scale.y = scaleSize;
        transform.localScale = scale;
	}
}
