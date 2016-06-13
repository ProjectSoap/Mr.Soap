using UnityEngine;
using System.Collections;

public class ResultCanvasSceneSelectCon : MonoBehaviour {

    public GameObject Waku1;
    public GameObject Waku2;

	// Use this for initialization
	void Start () {
        WakuSelect(true);
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    public void WakuSelect(bool waku1Flg)
    {
        if (waku1Flg == true)
        {
            Waku1.SetActive(true);
            Waku2.SetActive(false);
        }
        else
        {
            Waku2.SetActive(true);
            Waku1.SetActive(false);
        }
    }
}
