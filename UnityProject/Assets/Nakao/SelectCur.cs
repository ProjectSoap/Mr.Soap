using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectCur : MonoBehaviour {
    public List<Image> Key;
	// Use this for initialization
	void Start () {
        Key[0].enabled = true;
        Key[1].enabled = true;
        Key[2].enabled = false;
        Key[3].enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
        Color r;
        r.r = r.a = 1.0f;
        r.b = 0.8f;
        r.g = 0.0f;
        Key[0].color = new Color(1, 1, 1, 1);
        Key[1].color = new Color(1, 1, 1, 1);
	    if (Input.GetKey(KeyCode.LeftArrow)){
            Key[0].enabled = false;
            Key[2].enabled = true;
            Key[3].enabled = false;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Key[1].enabled = false;
            Key[3].enabled = true;
            Key[2].enabled = false;
        }
        else
        {
            Key[0].enabled = true;
            Key[1].enabled = true;
            Key[2].enabled = false;
            Key[3].enabled = false;
        }
	}
}
