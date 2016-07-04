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


	RecoverySoapCreatersManager m_myManager;

    [SerializeField]
    bool isHaveRevoverySoap; // 生成したものを所持しているかのフラグ

    [SerializeField,TooltipAttribute("生成を許可するプレイヤーとの距離")]
    float distanceForParmitTheCreate;

    CharacterWordsUI sayUI;

    [SerializeField]
    bool isRangeOut;
    public bool IsRangeOut
    {
        get { return isRangeOut; }
        set { isRangeOut = value; }
    }

    public bool IsHaveRevoverySoap
    {
        get { return isHaveRevoverySoap; }
        set { isHaveRevoverySoap = value; }
    }
    
    
    // Use this for initialization
    void Start ()
    {
		m_myManager = GameObject.Find("RecoverySoapCreatersManager").GetComponent<RecoverySoapCreatersManager>();
		IsHaveRevoverySoap = false;
        sayUI = GameObject.Find("SekkenSayUI").GetComponent<CharacterWordsUI>();
    }
	
	// Update is called once per frame
	void Update ()
    {
	}
	

	public void CreateSoap()
	{
		soapInstance = (GameObject)Instantiate(recoverySoap, transform.position, recoverySoap.transform.rotation) as GameObject;
		RecoverySoapObject script = soapInstance.GetComponent<RecoverySoapObject>();
		script.Parent = this;
		IsHaveRevoverySoap = true;
		sayUI.DrawSayTexture(CharacterWordsUI.ESayTexName.SEKKEN_POP);
	}

	public void NorticeDestroy()
	{
		m_myManager.NorticeDestroy();
	}
}
