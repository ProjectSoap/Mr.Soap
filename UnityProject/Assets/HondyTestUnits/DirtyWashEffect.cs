using UnityEngine;
using System.Collections;

public class DirtyWashEffect : TrackingObject {


	enum State
	{
		RISE,
		WAIT,
		TRACK
	}
	State m_state = State.RISE;

	float m_time;
	public float m_waitTime = 0.5f;
	public float m_riseTime = 0.5f;
	public float m_riseVelocity = 1;
	/**
	 * <summary>The wash chain.</summary>
	 */

	WashChain washChain;
    Wash_Gauge washgauge;

	// Use this for initialization
	void Start () {

		washChain = GameObject.Find("WashChain").GetComponent<WashChain>();
        washgauge = GameObject.Find("WashGauge").GetComponent<Wash_Gauge>();
	}


	void UpdateState()
	{
		switch (m_state)
		{
			case State.RISE:
				if (m_time >= m_riseTime)
				{
					m_state = State.WAIT;
				}
				break;
			case State.WAIT:
				if (m_time >= m_riseTime + m_waitTime)
				{
					m_state = State.TRACK;
				}
				break;
			case State.TRACK:
				break;
			default:
				break;

		}
	}

	// Update is called once per frame
	void Update () {
		m_time += Time.deltaTime;
		UpdateState();

		switch (m_state)
		{
			case State.RISE:
				break;
			case State.WAIT:
				break;
			case State.TRACK:
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
				break;
			default:
				break;

		}
	}
	void FixedUpdate()
	{
		Rigidbody rb = GetComponent<Rigidbody>(); ;

		switch (m_state)
		{
			case State.RISE:
				rb.velocity = ( -transform.forward * m_riseVelocity);
				rb.AddForce(rb.velocity);
				break;
			case State.WAIT:
				rb.velocity = (-transform.forward * 0);
				rb.AddForce(rb.velocity);
				break;
			case State.TRACK:
				rb.velocity = (m_rotNow * new Vector3(0, 1, 0) * m_velocity);
				rb.AddForce(rb.velocity);
				break;
			default:
				break;
		}
	}

	void OnTriggerEnter(Collider collisionObject)
	{
		if (collisionObject.gameObject.layer == LayerMask.NameToLayer("Player") && m_state == State.TRACK)
		{
			washChain.GetWash();
            washgauge.GetWash();
			GameObject.Destroy(gameObject);
		}
	}
	
}
