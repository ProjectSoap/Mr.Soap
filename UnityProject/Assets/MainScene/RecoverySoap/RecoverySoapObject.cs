using UnityEngine;
using System.Collections;

public class RecoverySoapObject : MonoBehaviour {

    RecoverySoapCreater parent;
    public RecoverySoapCreater Parent
    {
        get { return parent; }
        set { parent = value; }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}


    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.tag == "Player")
        {
            Parent.IsHaveRevoverySoap = false;
            Destroy(gameObject);
        }
    }}
