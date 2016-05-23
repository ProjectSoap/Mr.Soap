using UnityEngine;
using System.Collections;

public class DirtyObjectScript : MonoBehaviour
{

    DirtyCreater myCreater;
    public DirtyCreater MyCreater
    {
        get { return myCreater; }
        set { myCreater = value; }
    }
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bubble")
        {
            Destroy(gameObject);
        }
    }
}
