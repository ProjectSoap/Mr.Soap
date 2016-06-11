using UnityEngine;
using System.Collections;

public class CreditMrSoapController : MonoBehaviour {


    [SerializeField, Header("移動速度")]
    float m_velocity = 0.05f;
	Animator m_animator;

	// Use this for initialization
	void Start ()
    {

	}
	
	// Update is called once per frame
	void Update () {
        transform.position +=  transform.forward * -m_velocity;
	}
}
