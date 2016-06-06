using UnityEngine;
using System.Collections;

public class FogGenerator : MonoBehaviour {

    [SerializeField]
    bool create;
    [SerializeField]
    Color fogcolor;
    [SerializeField]
    float fognear;
    [SerializeField]
    float fogfar;


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
