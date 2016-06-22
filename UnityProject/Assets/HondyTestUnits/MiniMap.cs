using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {

    [SerializeField]
    WeatherSystem m_wStytem;


	[SerializeField]
	Image m_mistFrame;

	float m_transTimeNow = 0;

	enum State
	{
		NOT_FOG,
		TRANS_FOG,
		NOW_FOG,
		TRANS_NOT_FOG
	}



	State m_state;
	void Start () {
		m_wStytem = GameObject.Find("WeatherSystem").GetComponent<WeatherSystem>();
		m_mistFrame.enabled = false;

	}
	
	// Update is called once per frame
	void Update ()
	{

		switch (m_state)
		{
			case State.NOT_FOG:
				if (m_wStytem.NowWeather == Weather.FOG)
				{
					m_state = State.TRANS_FOG;
				}
					break;
			case State.TRANS_FOG:

				m_mistFrame.enabled = true;
					m_transTimeNow += Time.deltaTime;
					m_mistFrame.color = new UnityEngine.Color(1, 1, 1, 1 * m_transTimeNow);
				
				if (m_transTimeNow>=1)
				{
					m_state = State.NOW_FOG;
					m_transTimeNow = 1;
				}
				break;
			case State.NOW_FOG:
				if (m_wStytem.NowWeather != Weather.FOG)
				{
					m_state = State.TRANS_NOT_FOG;
					m_transTimeNow = 0;
				}
				break;
			case State.TRANS_NOT_FOG:
				
					m_transTimeNow += Time.deltaTime;
					m_mistFrame.color = new UnityEngine.Color(1, 1, 1, 1 - 1 * m_transTimeNow);
				
				if (m_transTimeNow >= 1)
				{
					m_state = State.NOT_FOG;
					m_mistFrame.enabled = false;
					m_transTimeNow = 0;
				}
				break;
			default:
				break;

		}
		
    }
}
