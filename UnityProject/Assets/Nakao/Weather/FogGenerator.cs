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
    float fogfar = 15;
    public float time; 

	// Use this for initialization
	void Start () {
        RenderSettings.fogMode = FogMode.Linear;
        RenderSettings.fogColor = fogcolor;
        RenderSettings.fogStartDistance = fognear;
        RenderSettings.fogEndDistance = fogfar;
        RenderSettings.fog = true;
	}
	
	// Update is called once per frame
	void Update () {
	    if(create)
		{
            time += Time.deltaTime;
            /*
			RenderSettings.fog = true;
			RenderSettings.fogColor = fogcolor;
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogStartDistance = fognear;
			RenderSettings.fogEndDistance = fogfar;
             * */
            RenderSettings.fogEndDistance = Mathf.Lerp(1000,15,time);
		}
        else
        {
            //RenderSettings.fog = false;
        }
	}

    public void ChangeCreateMode()
    {
        create = !create;
        if (create)
        {
            time = 0.0f;
			//RenderSettings.fogEndDistance = fogfar;
		}
        else
        {
           // RenderSettings.fog = false;
            RenderSettings.fogEndDistance = 1000;
        }
    }

    public void End()
    {
        create = false;
        RenderSettings.fogEndDistance = 1000;
        //RenderSettings.fog = false;
        
    }

}
