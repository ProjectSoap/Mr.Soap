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
        controller.State = CreditMrSoapController.CreditMrSoapState.STOP
            ;
    }
}
