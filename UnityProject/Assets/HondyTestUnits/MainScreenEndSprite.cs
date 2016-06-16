using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreenEndSprite : MonoBehaviour {
	

	float m_time;
	float m_addScale = 2f;
	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		if (transform.localScale.x < 1)
		{
			transform.localScale += new Vector3(m_addScale, m_addScale, m_addScale) * Time.deltaTime;
		}
		if (transform.localScale.x > 1)
		{
			transform.localScale = new Vector3(1, 1, 1);
		}
	}
}
