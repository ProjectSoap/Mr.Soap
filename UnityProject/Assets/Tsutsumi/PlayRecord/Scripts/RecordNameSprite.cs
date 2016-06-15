using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecordNameSprite : MonoBehaviour {

    public List<Sprite> recordNameSprites;

    private Image thisImage;

	// Use this for initialization
	void Awake () {
        thisImage = transform.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void SetImageSprite(int recordNo)
    {
        if (recordNameSprites[recordNo] != null)
        {
            thisImage.sprite = recordNameSprites[recordNo];
        }
        else
        {
            thisImage.sprite = null;
        }
    }
}
