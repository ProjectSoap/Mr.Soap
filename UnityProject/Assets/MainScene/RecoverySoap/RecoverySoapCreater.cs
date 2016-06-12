using UnityEngine;
using System.Collections;

public class RecoverySoapCreater : MonoBehaviour {

    [SerializeField]
    GameObject recoverySoap;

    GameObject soapInstance;
    public UnityEngine.GameObject RecoverySoap
    {
        get { return soapInstance; }
    }
    [SerializeField]
    bool isInstance;    // 生成するかのフラグ

    [SerializeField]
    bool isHaveRevoverySoap; // 生成したものを所持しているかのフラグ

    [SerializeField,TooltipAttribute("生成を許可するプレイヤーとの距離")]
    float distanceForParmitTheCreate;



    [SerializeField]
    bool isRangeOut;
    public bool IsRangeOut
    {
        get { return isRangeOut; }
        set { isRangeOut = value; }
    }
    public bool IsInstance
    {
        get { return isInstance; }
        set { isInstance = value; }
    }

    public bool IsHaveRevoverySoap
    {
        get { return isHaveRevoverySoap; }
        set { isHaveRevoverySoap = value; }
    }
    
    
    // Use this for initialization
    void Start ()
    {
        IsHaveRevoverySoap = false;
        isInstance = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
	    if (isInstance)
	    {
             soapInstance =  (GameObject)Instantiate(recoverySoap, transform.position,recoverySoap.transform.rotation) as GameObject;
            RecoverySoapObject script = soapInstance.GetComponent<RecoverySoapObject>();
            script.Parent = this;
            isInstance = false;
            IsHaveRevoverySoap = true;

        }
	}

    public void CheckDistance(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        if (distance >= 15.0f)
        {
            IsRangeOut = true;
        }
        else
        {
            IsRangeOut = false;
        }
    }
}
