using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreenEndSprite : MonoBehaviour {
	
	float m_time;
	float m_addScale = 2f;
	bool m_isAdd = true;
	// Use this for initialization
	void Start ()
	{
		
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_isAdd)
		{

			transform.localScale += new Vector3(m_addScale, m_addScale, m_addScale) * Time.deltaTime;
		
			if (transform.localScale.x > 1.2)
			{
				m_isAdd = false;
				m_addScale = 1.0f;
			}
		}
		else
		{
			
			transform.localScale -= new Vector3(m_addScale, m_addScale, m_addScale) * Time.deltaTime;
			
			if (transform.localScale.x < 0.9f)
			{
				m_isAdd = true;
			}
		}
	}
}
