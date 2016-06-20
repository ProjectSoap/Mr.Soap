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
<<<<<<< HEAD

=======
    public float time; 
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

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
<<<<<<< HEAD
=======
            time += Time.deltaTime;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
            /*
			RenderSettings.fog = true;
			RenderSettings.fogColor = fogcolor;
			RenderSettings.fogMode = FogMode.Linear;
			RenderSettings.fogStartDistance = fognear;
			RenderSettings.fogEndDistance = fogfar;
             * */
<<<<<<< HEAD
=======
            RenderSettings.fogEndDistance = Mathf.Lerp(1000,15,time);
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
<<<<<<< HEAD
			RenderSettings.fogEndDistance = fogfar;
=======
            time = 0.0f;
			//RenderSettings.fogEndDistance = fogfar;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
