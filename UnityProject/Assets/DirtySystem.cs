using UnityEngine;
using System.Collections;

public class DirtySystem : MonoBehaviour
{
    [SerializeField]
    GameObject player;

    [SerializeField]
    GameObject points;


    // Use this for initialization
    void Start ()
    {
        
	}
	
	// Update is called once per frame
	void Update ()
    {
        DirtyCreater creater =  points.GetComponent<DirtyCreater>();
        creater.CheckDistance(player.transform.position);
    }
}
