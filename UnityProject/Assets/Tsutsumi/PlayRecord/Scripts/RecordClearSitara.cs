using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class RecordClearSitara : MonoBehaviour {

    [SerializeField]
    private List<Sprite> clearSitaraList;
    [SerializeField]
    private GameObject modoru;

    private Image thisImage;

	// Use this for initialization
	void Awake () {
        thisImage = transform.GetComponent<Image>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void ChangeClearSitara(int no)
    {
        if (no < 0 || no >= clearSitaraList.Count)
        {
            modoru.SetActive(true);
        }

        //テクスチャがあれば
        if (clearSitaraList[no] != null)
        {
            thisImage.sprite = clearSitaraList[no];
            modoru.SetActive(false);
            gameObject.SetActive(true);
        }
        else
        {
            modoru.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
