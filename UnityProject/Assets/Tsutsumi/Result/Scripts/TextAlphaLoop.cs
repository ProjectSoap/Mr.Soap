using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class TextAlphaLoop : MonoBehaviour {

    public float LoopTime = 2.0f;

    private Text text;
    private float loopTimeCount;

    // Use this for initialization
    void Start()
    {
        loopTimeCount = 0.0f;
        text = transform.GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
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

        Color col = text.color;
        col.a = percent;
        text.color = col;
    }
}
