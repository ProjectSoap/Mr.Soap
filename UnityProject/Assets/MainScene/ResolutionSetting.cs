using UnityEngine;
using System.Collections;

public class ResolutionSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {

        int width = 1280;
        int height = 720;

        bool fullscreen = true;

        int preferredRefreshRate = 60;

        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
