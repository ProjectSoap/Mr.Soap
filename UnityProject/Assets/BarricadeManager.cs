using UnityEngine;
using System.Collections;

public class BarricadeManager : MonoBehaviour {

	public BarricadeObject[] m_barricade;

	// Use this for initialization
	void Start () {
		
		if (PlayerPrefs.GetInt("C1WashCount", 0) >= 100)
		{
			for (int i= 0;i < m_barricade.Length;i++)
			{
				if (m_barricade[i].m_lockArea <= 2)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}

		if (PlayerPrefs.GetInt("C2WashCount", 0) >= 150)
		{
			for (int i = 0; i < m_barricade.Length; i++)
			{
				if (m_barricade[i].m_lockArea <= 3)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}

		if (PlayerPrefs.GetInt("C2WashCount", 0) >= 150)
		{
			for (int i = 0; i < m_barricade.Length; i++)
			{
				if (m_barricade[i].m_lockArea <= 4)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
