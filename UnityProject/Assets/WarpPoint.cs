using UnityEngine;
using System.Collections;

public class WarpPoint : MonoBehaviour {

    public GameObject point;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    void OnTriggerEnter( Collider collisionObject)
    {
        collisionObject.gameObject.transform.position = point.transform.position;
    }
}
