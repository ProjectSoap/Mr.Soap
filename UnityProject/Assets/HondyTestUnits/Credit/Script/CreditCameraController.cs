using UnityEngine;
using System.Collections;

public class CreditCameraController : MonoBehaviour {

	[SerializeField, Header("カメラ速度")]
	float m_camraVelocity = 0.05f;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0, 0, m_camraVelocity);
	}

    void OnTriggerEnter(Collider collisionObject)
    {

    }
}
