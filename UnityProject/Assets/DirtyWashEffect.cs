using UnityEngine;
using System.Collections;

public class DirtyWashEffect : TrackingObject {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		m_goalRot = Quaternion.FromToRotation(new Vector3(0,1,0), m_goalObject.transform.position - this.transform.position);
		s += add;
		if (s > 1)
		{
			s = 1;
		}
		m_velocity += m_addVelocity;
		if (m_velocity > m_velocityMax)
		{
			m_velocity = m_velocityMax;
		}
		m_rotNow = Quaternion.Slerp(m_firstRot, m_goalRot, s);

	}
	void FixedUpdate()
	{

		Rigidbody rb = GetComponent<Rigidbody>(); ;
		rb.velocity = (m_rotNow * new Vector3(0, 1, 0) * m_velocity);
		rb.AddForce(rb.velocity);
	}

	void OnTriggerEnter(Collider collisionObject)
	{
		if (collisionObject.gameObject.layer == LayerMask.NameToLayer("Player"))
		{
			GameObject.Destroy(gameObject);
		}
	}
	
}
