using UnityEngine;
using System.Collections;

public class CreditCameraStopper : MonoBehaviour {
    CreditCameraController m_camera;
    // Use this for initialization
    void Start () {
        m_camera = GameObject.Find("Main Camera").GetComponent<CreditCameraController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
    void OnTriggerEnter(Collider coll)
    {
        m_camera.CamraVelocity = 0;
    }
}
