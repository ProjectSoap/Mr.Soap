using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ResultCanvasKaihouCon : MonoBehaviour {

    //実績名テクスチャリスト
    [SerializeField]
    List<Sprite> sprites;
    [SerializeField]
    Image recordNameImage;

    [SerializeField]
    ResultRewardTexCon rewardTexCon;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public bool SetRecordImage(int recordNo)
    {
        if (recordNo < 0 || recordNo >= 30) return false;

        recordNameImage.sprite = sprites[recordNo];
        rewardTexCon.SetRecordImage(recordNo);
        return true;
    }

}
