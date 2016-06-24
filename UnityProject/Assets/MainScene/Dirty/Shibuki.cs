using UnityEngine;
using System.Collections;

public class Shibuki : MonoBehaviour {

    ParticleSystem m_subParticle;
    ParticleSystem m_particle;
	// Use this for initialization
	void Start () {
		m_particle = transform.FindChild("main").gameObject.GetComponent<ParticleSystem>();
	}
	
    public void  SetColor(Color color)
    {
        //if (m_particle)
        {
            m_particle = transform.FindChild("main").gameObject.GetComponent<ParticleSystem>();

            m_particle.startColor = color;
            m_subParticle = transform.FindChild("main").gameObject.transform.FindChild("tail").gameObject.GetComponent<ParticleSystem>();
            m_subParticle.startColor = color;
        }
    }

	// Update is called once per frame
	void Update () {
		if (m_particle.time >= 0.5f)
		{
			Destroy(gameObject);
		}
	}
}
