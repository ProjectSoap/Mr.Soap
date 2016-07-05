using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DirtyWashUIImage
	:
	MonoBehaviour
{

	enum EState
	{
		APPEARANCE,
		MORPH,
		MOVE,
		VANISH
	}

	EState m_state;
	float m_controlTime;
	float m_morphTime = 0.3f;
	float m_morphScale;
	Vector3 m_endPosition;
	[SerializeField]
	float m_velocity = 1000f;
	float m_offset = 50;

	Image m_dirtyImage;
	Image m_circleImage;

	// Use this for initialization
	void Start ()
	{
		m_endPosition = new Vector3(Screen.width,0);
		m_circleImage = transform.FindChild("Circle").GetComponent<Image>();
		m_dirtyImage = transform.FindChild("Dirty").GetComponent<Image>();
		m_circleImage.rectTransform.localScale = new Vector3(0, 0, 0);
		m_morphScale = 1.0f / m_morphTime;
	}
	
	void UpdateState()
	{
		switch(m_state)
		{
			case EState.APPEARANCE:
				if (m_controlTime > m_morphTime)
				{
					StateExitProcesss();
					m_state = EState.MORPH;
					StateEnterProcesss();
				}
				break;
			case EState.MORPH:
				if (m_controlTime > m_morphTime)
				{
					StateExitProcesss();
					m_state = EState.MOVE;
					StateEnterProcesss();
				}
				break;
			case EState.MOVE:
				if (
					m_endPosition.x - m_offset < transform.position.x
					&&
					 transform.position.x < m_endPosition.x + m_offset
					&&
					m_endPosition.y - m_offset < transform.position.y
					&&
					 transform.position.y < m_endPosition.y + m_offset
					)
				{
					StateExitProcesss();
					m_state = EState.VANISH;
					StateEnterProcesss();
				}
				break;
			case EState.VANISH:
				StateExitProcesss();
				StateEnterProcesss();
				break;
			default:
				break;
		}
	}

	// Update is called once per frame
	void Update ()
	{
		UpdateState();
		StateProcesss();
	}
	void StateEnterProcesss()
	{
		switch (m_state)
		{
			case EState.APPEARANCE:
				break;
			case EState.MORPH:
				break;
			case EState.MOVE:
				break;
			case EState.VANISH:
				break;
			default:
				break;
		}
	}

	void StateExitProcesss()
	{
		switch (m_state)
		{
			case EState.APPEARANCE:
				m_controlTime = 0;
				break;
			case EState.MORPH:
				m_circleImage.rectTransform.localScale = new Vector3(1, 1);
				m_dirtyImage.rectTransform.localScale = new Vector3(0,0);

				m_controlTime = 0;
				break;
			case EState.MOVE:
				break;
			case EState.VANISH:
				break;
			default:
				break;
		}
	}

	void StateProcesss()
	{
		m_controlTime += Time.deltaTime;
		switch (m_state)
		{
			case EState.APPEARANCE:
				break;
			case EState.MORPH:
				m_circleImage.rectTransform.localScale += new Vector3( Time.deltaTime * m_morphScale, Time.deltaTime * m_morphScale);
				m_dirtyImage.rectTransform.localScale -= new Vector3(Time.deltaTime * m_morphScale, Time.deltaTime * m_morphScale);

				break;
			case EState.MOVE:
				
				{
					Vector3 direction = transform.position - m_endPosition;
					direction = Vector3.Normalize(direction);
					transform.position -= direction * m_velocity * Time.deltaTime;
				}
				break;
			case EState.VANISH:
				Destroy(gameObject);
				
				break;
			default:
				break;
		}
	}
}
