using UnityEngine;
using System.Collections;

public class SectionJudge : MonoBehaviour {

	PlayerCharacterController m_player;
	[SerializeField]
	uint m_manageSection;
	// Use this for initialization
	void Start () {
		m_player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
	void OnTriggerStay(Collider col) 
	{
		if(col != null)
		{
			m_player.inSection = m_manageSection;

		}
	}
}
