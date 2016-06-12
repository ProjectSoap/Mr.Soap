using UnityEngine;
using System.Collections;

public class CreditCameraController : MonoBehaviour {

	[SerializeField, Header("カメラ速度")]
	float m_camraVelocity = 0.05f;
    public bool isEnd = false;
    float stopTime;
    public float CamraVelocity
    {
        get { return m_camraVelocity; }
        set { m_camraVelocity = value; }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		transform.position += new Vector3(0, 0, CamraVelocity);
        if (m_camraVelocity == 0.0f)
        {
            stopTime += Time.deltaTime;
            if (stopTime > 3)
            {
                isEnd = true;
            }
        }
	}

    void OnTriggerEnter(Collider collisionObject)
    {

    }
}
