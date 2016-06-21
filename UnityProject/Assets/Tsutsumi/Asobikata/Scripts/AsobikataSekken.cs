using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsobikataSekken : MonoBehaviour {

    [SerializeField]
    private List<AsobikataSekkenObj> SekkenObjects;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    //せっけんくん表示切替に使用する
    public void SetSelectNo(int selectNo)
    {
        if (selectNo < 0) return;

        //せっけんオブジェクトの回数分回す
        for (int i = 0; i < SekkenObjects.Count; ++i)
        {
            //nullチェック
            if(SekkenObjects[i].isNotNullSprite(selectNo) == true)
            {
                SekkenObjects[i].gameObject.SetActive(true);
                SekkenObjects[i].SetSprite(selectNo);
            }
            else
            {
                SekkenObjects[i].gameObject.SetActive(false);
            }
        }
    }
}
