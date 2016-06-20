using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class MiniMap : MonoBehaviour {

    [SerializeField]
    WeatherSystem m_wStytem;


	[SerializeField]
	Image m_mistFrame; 
	void Start () {
		m_wStytem = GameObject.Find("WeatherSystem").GetComponent<WeatherSystem>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (m_wStytem.NowWeather == Weather.FOG)
		{
			m_mistFrame.enabled = true;
		}
		else
		{

			m_mistFrame.enabled = false;
		}

    }
}
