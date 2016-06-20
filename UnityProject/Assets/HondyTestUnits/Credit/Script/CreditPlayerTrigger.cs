using UnityEngine;
using System.Collections;

public class CreditPlayerTrigger : MonoBehaviour {

    CreditMrSoapController controller;
	// Use this for initialization
	void Start () {
        controller = GameObject.Find("MrSoap").GetComponent<CreditMrSoapController>();

    }
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter(Collider collisionInfo)
    {
        controller.State = CreditMrSoapController.CreditMrSoapState.MOVE;
    }

}
