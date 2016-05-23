using UnityEngine;
using System.Collections;

public class DirtySystem : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject[] points;
    

    // Use this for initialization
    void Start ()
    {

    }
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0;i < points.Length;i++)
        {
            DirtyCreater creater = points[i].GetComponent<DirtyCreater>();
            creater.CheckDistance(player.transform.position);
        }
    }
}
