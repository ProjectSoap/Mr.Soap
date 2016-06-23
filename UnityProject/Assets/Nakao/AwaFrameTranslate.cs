using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class AwaFrameTranslate : MonoBehaviour {
    [SerializeField]
    List<Image> frames;
    public Vector2 size;
    public Vector3 scale;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
        frames[0].rectTransform.localPosition = new Vector3(+size.x * scale.x, -size.y * scale.y,0.0f);
        frames[1].rectTransform.localPosition = new Vector3(+size.x * scale.x, +size.y * scale.y, 0.0f);
        frames[2].rectTransform.localPosition = new Vector3(-size.x * scale.x, +size.y * scale.y, 0.0f);
        frames[3].rectTransform.localPosition = new Vector3(-size.x * scale.x, -size.y * scale.y, 0.0f);
        for(int i = 0; i < frames.Count; i++)
        {
            frames[i].rectTransform.localScale = scale;
        }
       
	}
    public void ResetAnimation()
    {
        for (int i = 0; i < frames.Count; i++)
        {
            frames[i].GetComponent<Animator>().Play("play",0,0.0f);
        }
    }

}
