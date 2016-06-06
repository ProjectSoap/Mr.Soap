using UnityEngine;
using System.Collections;
using UnityEngine.UI;  
public class TestUI : MonoBehaviour {


	// Use this for initialization
	void Start ()
    {
    }
	
	// Update is called once per frame
	void Update () {

        GameObject go = GameObject.Find("sekkenkun");
        SoapDrive d2 = go.GetComponent<SoapDrive>();
        this.GetComponent<Text>().text = "velocity" + d2.Velocity + "\n";
        this.GetComponent<Text>().text += "directionX" + d2.Direction.x + "\n";
        this.GetComponent<Text>().text += "directionY" + d2.Direction.y + "\n";
        this.GetComponent<Text>().text += "directionZ" + d2.Direction.z + "\n";
    }
}
