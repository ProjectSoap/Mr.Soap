using UnityEngine;
using System.Collections;

public class CreditCameraStopper : MonoBehaviour {
    CreditCameraController m_camera;
	float m_tempVelocity;
	public float m_stayTime = 2;
	bool m_isCameraStay = false;
    // Use this for initialization
    void Start () {
        m_camera = GameObject.Find("Main Camera").GetComponent<CreditCameraController>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isCameraStay)
		{
			m_stayTime -= Time.deltaTime;
			if (m_stayTime <= 0)
			{
				m_camera.CamraVelocity = m_tempVelocity;
				m_isCameraStay = false;
			}
		}
	}
    void OnTriggerEnter(Collider coll)
    {
		if (coll.name == "Trigger")
		{

			float temp = 0;
			m_tempVelocity = m_camera.CamraVelocity;

			m_camera.CamraVelocity = 0;
			m_isCameraStay = true;
		}
	}
}
