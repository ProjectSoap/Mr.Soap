using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class SelectCur : MonoBehaviour {
    public List<Image> Key;
	// Use this for initialization
	void Start () {
	    
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
            Key[0].color = r;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            Key[1].color = r;
        }
	}
}
