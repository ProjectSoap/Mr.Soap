using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Shine : MonoBehaviour {

    public List<Image> light;
    public float time;
    public bool lightflg;
	// Use this for initialization
	void Start () {
        EndLight();
	}
	
	// Update is called once per frame
	void Update () {
	    if(lightflg)
        {
            time += Time.deltaTime;
            if(time > 0.29f)
            {
                light[2].enabled = true; 
            }
            else if(time > 0.245f)
            {
                light[1].enabled = true;
            }
            else if (time > 0.2f)
            {
                light[0].enabled = true;
            }
            if(time > 1.3f)
            {
                EndLight();
            }
        }
	}

    void EndLight()
    {
        time = 0.0f;
        lightflg = false;
        for(int i = 0 ; i < light.Count ; ++i)
        {
            light[i].enabled = false;
        }
    }

    public void StartLight()
    {
        lightflg = true;
    }

}
