using UnityEngine;
using System.Collections;

public class DirtySystem : MonoBehaviour {

    int myTime;
    [SerializeField]
    int emitTime;
    [SerializeField]
    Object myCube;


    // Use this for initialization
    void Start () {

        myTime = 0;
	}
	
	// Update is called once per frame
	void Update ()
    {

        if (++myTime > emitTime)
        {
            Instantiate(myCube, transform.position, transform.rotation);
            myTime = 0;
        }

    }
}
