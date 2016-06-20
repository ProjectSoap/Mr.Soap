using UnityEngine;
using System.Collections;

public class ResolutionSetting : MonoBehaviour {

	// Use this for initialization
	void Start () {

        int width = 1280;
        int height = 720;

<<<<<<< HEAD
        bool fullscreen = true;
=======
        bool fullscreen = false;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

        int preferredRefreshRate = 60;

        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);
    }
	
	// Update is called once per frame
	void Update () {
	
	}
}
