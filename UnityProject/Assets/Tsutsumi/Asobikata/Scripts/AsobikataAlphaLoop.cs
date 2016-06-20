using UnityEngine;
using System.Collections;

public class AsobikataAlphaLoop : MonoBehaviour {

    public float LoopTime = 2.0f;

    private SpriteRenderer render;
    private float loopTimeCount;

	// Use this for initialization
	void Start () {
        loopTimeCount = 0.0f;
        render = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
        float percent;

        loopTimeCount += Time.deltaTime;
        if (loopTimeCount > LoopTime)
            loopTimeCount -= LoopTime;

        percent = loopTimeCount / LoopTime;
        percent = percent * 2.0f;
        if (percent > 1.0f)
        {
            percent = 2.0f - percent;
        }

        Color col = render.color;
        col.a = percent;
        render.color = col;
	}
}
