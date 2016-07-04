using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DistanceTextUI : MonoBehaviour {

	Text m_myText;

	[SerializeField]
	GameObject m_targetA;
	public UnityEngine.GameObject TargetA
	{
		get { return m_targetA; }
		set { m_targetA = value; }
	}
	[SerializeField]
	GameObject m_targetB;
	public UnityEngine.GameObject TargetB
	{
		get { return m_targetB; }
		set { m_targetB = value; }
	}
	float m_distance;
	// Use this for initialization
	void Start () {
		m_myText = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (TargetA && TargetB)
		{
			m_distance = (int)Vector3.Distance(TargetA.transform.position, TargetB.transform.position);
			m_myText.text = m_distance.ToString() + "メートル";

		}
	}
}
