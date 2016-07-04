using UnityEngine;
using System.Collections;

public class CreditSceanHolder : MonoBehaviour
{
    CreditCameraController m_camera;

	// Use this for initialization
	void Start ()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<CreditCameraController>();
        BGMManager.Instance.PlayBGM("Credit",0);
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown(KeyCode.Escape) || m_camera.isEnd)
		{
			m_camera.isEnd = true;
			Time.timeScale = 1.0f;
			Fade.ChangeScene("Menu");
        }
		else
		{

			if (Input.GetKey(KeyCode.Space))
			{
				Time.timeScale = 3.0f;
			}
			else
			{
				Time.timeScale = 1.0f;
			}
		}
	}
}
