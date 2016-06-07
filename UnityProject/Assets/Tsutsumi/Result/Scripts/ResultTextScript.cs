using UnityEngine;
using System.Collections;

public class ResultTextScript : MonoBehaviour {

    public float animationTime;
    public float minPercent;

    private float timeCount;
	// Use this for initialization
	void Start () {
        timeCount = 0.0f;
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
        if(upPercent > 1.0f) upPercent = 1.0f;
        scaleSize = minPercent * upPercent + 1.0f * (1.0f-upPercent);

        //スケールへ反映
        scale.x = scale.y = scaleSize;
        transform.localScale = scale;
	}
}
