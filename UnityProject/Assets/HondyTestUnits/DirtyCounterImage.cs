using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class DirtyCounterImage
	:
	MonoBehaviour
{

	enum EState
	{
		APPEARANCE,
		VANISH,
		DESTROY
	}

	EState m_state;
	float m_controlTime;
	[SerializeField]
	float m_appearanceTime = 0.5f;

	Image m_image;

	// Use this for initialization
	void Start()
	{
		m_image = GetComponent<Image>();
		m_image.material.SetColor("Tint", new Color(1, 1, 1, 0));
	}

	void UpdateState()
	{
		switch (m_state)
		{
			case EState.APPEARANCE:
				if (m_appearanceTime < m_controlTime)
				{
					StateEnterProcesss();
					m_state = EState.VANISH;
					StateExitProcesss();
				}

				break;
			case EState.VANISH:
				if (m_appearanceTime < m_controlTime)
				{
					StateEnterProcesss();
					m_state = EState.DESTROY;
					StateExitProcesss();
				}
				break;
			case EState.DESTROY:
				break;
			default:
				break;

		}
	}

	// Update is called once per frame
	void Update()
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
			case EState.VANISH:
				break;
			case EState.DESTROY:
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
			case EState.VANISH:
				m_controlTime = 0;
				break;
			case EState.DESTROY:
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
				m_image.material.SetFloat("_MaskAlpha", m_controlTime / m_appearanceTime);
				break;
			case EState.VANISH:
				//m_image.color = new Color(m_image.color.r, m_image.color.g, m_image.color.b, 1 - (m_controlTime / m_appearanceTime));

				break;
			case EState.DESTROY:
				//Destroy(gameObject);
				break;
			default:
				break;
		}
	}
}
