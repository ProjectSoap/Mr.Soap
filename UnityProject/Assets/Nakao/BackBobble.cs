using UnityEngine;
//using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class BackBobble : MonoBehaviour {
    [SerializeField]
    List<RectTransform> bobble;
    int nowbobble;
    [SerializeField]
    float emitRate;
    [SerializeField]
    float nowtime;
    [SerializeField]
    float eltime;
    [SerializeField]
    float limittime;
    float[] limittimeBobbles;
    Vector3[] trans;
    enum bobbleState
    {
        NO_USE,
        USE
    };
    bobbleState[] state;
	// Use this for initialization
	void Start () {
        RectTransform sp;
        for(int i = 0 ; i < 39; ++i)
        {
            sp = RectTransform.Instantiate(bobble[0]);
            sp.gameObject.layer = bobble[0].gameObject.layer;
            bobble.Add(sp);
        }
        state = new bobbleState[bobble.Count];
        limittimeBobbles = new float[bobble.Count];
        trans = new Vector3[bobble.Count];
	}
	
	// Update is called once per frame
	void Update () {
        nowtime += Time.deltaTime;
        eltime += Time.deltaTime;
        if(nowtime > emitRate)
        {
            state[nowbobble] = bobbleState.USE;
            float x = ((float)(Random.Range(-10,10)));
            bobble[nowbobble].transform.position = new Vector3(x - 2.0f, -6.1f, this.gameObject.transform.position.z);
            x = Random.Range(this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.x + 0.49f);
            bobble[nowbobble].transform.localScale = new Vector3(x,x, 0);
            nowtime = Random.Range(0.0f,0.4f);
            trans[nowbobble] = new Vector3(Random.Range(0.01f,0.02f), Random.Range(0.04f,0.05f), 0);
            nowbobble++;
            if(nowbobble > bobble.Count - 2)
            {
                nowbobble = 0;
            }
        }
        for(int i = 0 ; i < bobble.Count - 1 ; ++i)
        {
            if(state[i] == bobbleState.USE)
            {
                Vector3 tran = bobble[i].transform.position + trans[i];
                tran.x += Mathf.Sin(eltime) * 0.01f;
                bobble[i].transform.position = tran;
                limittimeBobbles[i] += Time.deltaTime;
                if(limittimeBobbles[i]>limittime)
                {
                    state[i] = bobbleState.NO_USE;
                    tran.x = 100;
                    bobble[i].transform.position = tran;
                    limittimeBobbles[i] = 0;
                }
            }
        }
	}
    void OnTriggerEnter2D(Collider2D c)
    {
        string layerName = LayerMask.LayerToName(c.gameObject.layer);
        if(layerName == "DummyBubble")
        {
//            Destroy(c.gameObject);
        }
        if (layerName == "Bubble")
        {
            //this.gameObject.transform.position = (this.gameObject.transform.position + c.gameObject.transform.position) / 2.0f;
           // Destroy(c.gameObject);
        }
    }
}
