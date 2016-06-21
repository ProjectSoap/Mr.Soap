using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsobikataSekkenObj : MonoBehaviour {

    [SerializeField]
    private List<Sprite> spriteList;

    private SpriteRenderer render;

	// Use this for initialization
	void Awake () {
        render = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    //nullじゃなかったらtrue返す
    public bool isNotNullSprite(int spriteNo)
    {
        if(spriteNo < 0 || spriteNo >= spriteList.Count)
            return false;
        if (spriteList[spriteNo] == null)
            return false;

        //スプライトが存在する
        return true;
    }
    //この関数の前に上のnullチェック関数を通すこと
    public void SetSprite(int spriteNo)
    {
        render.sprite = spriteList[spriteNo];
    }
}
