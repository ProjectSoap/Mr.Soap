using UnityEngine;
using System.Collections;

public class FogGenerator : MonoBehaviour {

    [SerializeField]
    bool create;
    [SerializeField]
    Color fogcolor;
    [SerializeField]
    float fognear = 2;
    [SerializeField]
    float fogfar = 10;


	// Use this for initialization
	void Start () {
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogStartDistance = fognear;
        RenderSettings.fogEndDistance = fogfar;
        
	}
	
	// Update is called once per frame
	void Update () {
	    if(create)
		{
			RenderSettings.fog = true;
			RenderSettings.fogColor = fogcolor;
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogStartDistance = fognear;
			RenderSettings.fogEndDistance = fogfar;
		}
        else
        {
            RenderSettings.fog = false;
        }
	}

    public void ChangeCreateMode()
    {
        create = !create;
        if (create)
        {
            RenderSettings.fog = true;
            RenderSettings.fogColor = fogcolor;
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogStartDistance = fognear;
			RenderSettings.fogEndDistance = fogfar;
		}
        else
        {
            RenderSettings.fog = false;
        }
    }

    public void End()
    {
        create = false;
        
        RenderSettings.fog = false;
        
    }

}
