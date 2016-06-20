using UnityEngine;
using System.Collections;

public class CreditSceanHolder : MonoBehaviour
{
    CreditCameraController m_camera;

	// Use this for initialization
	void Start ()
    {
        m_camera = GameObject.Find("Main Camera").GetComponent<CreditCameraController>();
<<<<<<< HEAD
        //BGMManager.Instance.PlayBGM("Credit",0);
=======
        BGMManager.Instance.PlayBGM("Credit",0);
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Escape) || m_camera.isEnd)
        {
            Fade.ChangeScene("Menu");
        }
	}
}
