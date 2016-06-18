using UnityEngine;
using System.Collections;

public class Shibuki : MonoBehaviour {

	ParticleSystem m_particle;
	// Use this for initialization
	void Start () {
		m_particle = transform.FindChild("main").gameObject.GetComponent<ParticleSystem>();
	}
	
	// Update is called once per frame
	void Update () {
		if (m_particle.time >= 0.5f)
		{
			Destroy(gameObject);
		}
	}
}
