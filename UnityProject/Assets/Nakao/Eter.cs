using UnityEngine;
using System.Collections;

public class Eter : MonoBehaviour {

    [SerializeField]
    RectTransform size;
    [SerializeField]
    float waittime;
    [SerializeField]
    float delay;
    [SerializeField]
    float waittimelimitMin;
    [SerializeField]
    float waittimelimitMax;
    [SerializeField]
    float delaylimit;


    bool scale = false;
	// Use this for initialization
	void Start () {
        size = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        if(delay<=delaylimit || delay>=0)
        {
            waittime += Time.deltaTime ;
            if (waittime >= waittimelimitMax)
            {
                //delay=0;
                waittime = 0;
                scale = !scale;
            }

        }
        if(scale)
        {
            delay-= Time.deltaTime;
        }
        else
        {
            delay+= Time.deltaTime;
            //size.localScale = new Vector3(60.0f * (1 + ((delay / delaylimit) * 0.05f)), 50.0f * (1 + ((delay / delaylimit) * 0.025f)), 1);
        }
        size.localScale = new Vector3(1.0f * (1.0f + ((delay / delaylimit) * 0.1f)), 1.0f * (1.0f + ((delay / delaylimit) * 0.1f)), 1);
    }
}
