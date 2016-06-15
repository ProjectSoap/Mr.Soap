using UnityEngine;
using System.Collections;

public class CreditSceanHolder : MonoBehaviour
{
    CreditCameraController m_camera;

	// Use this for initialization
	void Start ()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<CreditCameraController>();
        //BGMManager.Instance.PlayBGM("Credit",0);
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || m_camera.isEnd)
        {
            Fade.ChangeScene("Menu");
        }
	}
}
