using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SoapTitle : MonoBehaviour {

    public Image Eter;

    public Image thisimage;
    [SerializeField]
    float time;

    [SerializeField]
    float start;
    [SerializeField]
    float end;

	// Use this for initialization
	void Start () {
        time = 0.0f;
        Eter.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        time += Time.deltaTime;
        thisimage.transform.position = new Vector3(Screen.width/2,Mathf.Lerp(Screen.height*1.3f, Screen.height*0.75f, time),0);
        if(time > 1.0f)
        {
            Eter.enabled = true;
        }
        else
        {

        }
        
	}
}
