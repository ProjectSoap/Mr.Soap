using UnityEngine;
using System.Collections;

public class ResolutionSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {

        int width = 960;
        int height = 540;

        bool fullscreen = true;

        int preferredRefreshRate = 60;

        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
