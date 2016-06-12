using UnityEngine;
using System.Collections;

public class WarpPoint : MonoBehaviour {

    public GameObject point;
    CreditMrSoapController controller;
	// Use this for initialization
	void Start ()
    {
        controller = GameObject.Find("MrSoap").GetComponent<CreditMrSoapController>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider collisionObject)
    {
        collisionObject.gameObject.transform.position = point.transform.position;
        collisionObject.gameObject.transform.rotation = point.transform.rotation * collisionObject.gameObject.transform.rotation;
        controller.State = CreditMrSoapController.CreditMrSoapState.STOP
            ;
    }
}
