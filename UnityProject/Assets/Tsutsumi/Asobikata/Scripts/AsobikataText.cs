using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AsobikataText : MonoBehaviour {

    [SerializeField]
    private List<Sprite> TextSpriteList;

    private SpriteRenderer thisRender;

	// Use this for initialization
	void Start () {
        thisRender = transform.GetComponent<SpriteRenderer>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void TextSwitch(int no)
    {
        if (no < 0 || no >= TextSpriteList.Count)
            return;

        //sprite切り替え
        thisRender.sprite = TextSpriteList[no];
    }


}
