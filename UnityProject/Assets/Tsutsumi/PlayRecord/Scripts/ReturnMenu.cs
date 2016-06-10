using UnityEngine;
using System.Collections;

public class ReturnMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	    if(Input.GetKeyDown(KeyCode.Backspace)){
            Fade.ChangeScene("Menu");

            BGMManager.Instance.PlaySE("Cursor_Cancel");
        }
	}
}
