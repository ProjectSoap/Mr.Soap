using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class LoadingText : MonoBehaviour
{
    [SerializeField]
    Text size;
    [SerializeField]
    float waittime;
    [SerializeField]
    float waittimelimit;
    [SerializeField]
    int count;


    bool scale = false;
    // Use this for initialization
    void Start()
    {
        size = this.GetComponent<Text>();
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
                count++;
                if (count > 3)
                {
                    count = 0;
                }
            }
        }

        switch (count)
        {
            case 0:
                {
                    size.text = "Now      Loading";
                    break;
                }
            case 1:
                {
                    size.text = "Now      Loading .";
                    break;
                }
            case 2:
                {
                    size.text = "Now      Loading ..";
                    break;
                }
            case 3:
                {
                    size.text = "Now      Loading ...";
                    break;
                }
        }
    }
}