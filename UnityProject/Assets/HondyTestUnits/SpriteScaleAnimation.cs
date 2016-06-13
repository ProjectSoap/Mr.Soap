using UnityEngine;
using System.Collections;

public class SpriteScaleAnimation : MonoBehaviour {

    bool isUp  = true;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (isUp)
        {
            transform.localScale += new Vector3(Time.deltaTime * 0.25f, Time.deltaTime * 0.25f, Time.deltaTime * 0.25f);
            if (transform.localScale.x > 1.25f)
            {
                isUp = false;
            }
        }
        else
        {
            transform.localScale -= new Vector3(Time.deltaTime * 0.25f, Time.deltaTime * 0.25f, Time.deltaTime * 0.25f);
            if (transform.localScale.x < 0.9f)
            {
                isUp = true;
            }
        }
    }
}
