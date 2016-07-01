using UnityEngine;
using System.Collections;

public class AsobikataSceneManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        //戻るボタン
        if (Input.GetKeyDown(KeyCode.Escape) && Fade.FadeEnd())
        {
            //SE再生
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Cursor_Cancel");
            }

            //シーン遷移開始
            Fade.ChangeScene("Menu");
        }
	}
}
