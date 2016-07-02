using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DistanceTextUI : MonoBehaviour {

	Text m_myText;

	[SerializeField]
	GameObject m_targetA;

	[SerializeField]
	GameObject m_targetB;

	float m_distance;
	// Use this for initialization
	void Start () {
		m_myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		m_distance = (int)Vector3.Distance(m_targetA.transform.localPosition, m_targetB.transform.localPosition); 
		m_myText.text = m_distance.ToString() + "メートル";
	}
}
