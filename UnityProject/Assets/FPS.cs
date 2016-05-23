using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FPS : MonoBehaviour {

    public Text obj;

	// Use this for initialization
	void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        float fps = 1.0f / Time.deltaTime;
        obj.text = "" + fps; 
	}
}
