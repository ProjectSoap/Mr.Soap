using UnityEngine;
using System.Collections;

public class NorticeDirectionRecaverySoap : MonoBehaviour {
    
    GameObject player;
    [SerializeField]
    float angle;
    GameObject recoverySoap;




    [SerializeField]
    Vector3 forward;
    [SerializeField]
    Vector3 soapPosition;
    [SerializeField]
    Vector3 myPosition;
    [SerializeField]
    Vector3 distance;



    public UnityEngine.GameObject RecoverySoap
    {
        get { return recoverySoap; }
        set { recoverySoap = value; }
    }

    bool isAppearance;  /* せっけん出現フラグ */
    public bool IsAppearance
    {
        get { return isAppearance; }
        set { isAppearance = value; }
    }

    // Use this for initialization
    void Start () {
        IsAppearance = false;
        player = GameObject.Find("PlayerCharacter");

    }
	
	// Update is called once per frame
	void Update ()
    {

        if (IsAppearance == false)
        {
            GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            GetComponent<MeshRenderer>().enabled = true;
        }

        if (IsAppearance && recoverySoap)
        {
            forward = player.transform.forward;
            soapPosition = new Vector3(recoverySoap.transform.position.x, 0, recoverySoap.transform.position.z);
            myPosition = new Vector3(transform.position.x, 0, transform.position.z);
            distance = myPosition - soapPosition;
            
            angle = Mathf.Atan2 (distance.x * forward.z - distance.z * forward.x , Vector3.Dot(forward, distance));
            angle = (Mathf.Rad2Deg * (angle));
            Quaternion rot = new Quaternion();
            rot.SetAxisAngle(transform.up, angle * Mathf.Deg2Rad);
            gameObject.transform.localRotation = rot;
        }


        //transform.RotateAroundLocal(transform.up, angle );
	}
}
