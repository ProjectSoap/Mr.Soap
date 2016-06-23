using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class LoadingText : MonoBehaviour
{
    [SerializeField]
    float waittime;
    [SerializeField]
    float waittimelimit;
    [SerializeField]
    int count;
    [SerializeField]
    List<Image> dot;

    bool scale = false;
    // Use this for initialization
    void Start()
    {
        count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        waittime += Time.deltaTime;
        if (waittime >= waittimelimit)
        {
            waittime += Time.deltaTime;
            if (waittime >= waittimelimit)
            {
                //delay=0;
                waittime = 0;
                dot[count].enabled = true;
                count++;
                if (count > 2)
                {
                    count = 0;
                }
            }
        }

        
    }

    void Reset()
    {
        for(int i = 0 ; i < dot.Count ; ++i)
        {
            dot[count].enabled = false;
        }
    }
}