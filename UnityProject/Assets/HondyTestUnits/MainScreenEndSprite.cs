using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MainScreenEndSprite : MonoBehaviour {
	
	float m_time;
	float m_addScale = 2f;
	bool m_isAdd = true;
	// Use this for initialization
<<<<<<< HEAD
	void Start () {
=======
	void Start ()
	{
		
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
<<<<<<< HEAD
=======
				m_addScale = 1.0f;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
