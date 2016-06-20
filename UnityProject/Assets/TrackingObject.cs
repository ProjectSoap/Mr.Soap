using UnityEngine;
using System.Collections;

public class TrackingObject : MonoBehaviour {

	public GameObject m_goalObject;
	public float m_velocity = 2;
	public float m_velocityMax = 8;
	public float m_addVelocity = 0.005f;
	protected Quaternion m_goalRot;
	protected Quaternion m_rotNow;
	protected Quaternion m_firstRot;
	public float s = 0;
	public float add = 0;
	// Use this for initialization
	void Start ()
	{
		m_goalRot = Quaternion.FromToRotation(this.transform.forward, m_goalObject.transform.position  - this.transform.position);


	}
	
	// Update is called once per frame
	void Update ()
	{
		m_goalRot = Quaternion.FromToRotation(this.transform.forward, m_goalObject.transform.position - this.transform.position);
		s += add;
		if (s > 1)
		{
			s = 1;
		}
		m_rotNow = Quaternion.Slerp(m_firstRot, m_goalRot,s);


	}
	void FixedUpdate()
	{
		Rigidbody rb = GetComponent<Rigidbody>();;
		rb.velocity = (m_rotNow * transform.forward * m_velocity);
		rb.AddForce(rb.velocity);
		
	}
}
