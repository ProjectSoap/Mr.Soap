using UnityEngine;
using System.Collections;

public class RecordBGMStart : MonoBehaviour {

	// Use this for initialization
	void Start () {
        BGMManager.Instance.PlayBGM("Actual", 0);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
