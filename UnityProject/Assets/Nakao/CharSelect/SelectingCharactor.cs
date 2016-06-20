using UnityEngine;
using System.Collections;

public enum SelectingCharactorNo
{
    SOAP,
    SOAP0,
    SOAPTYAN
}


public enum PlayModeState
{
	NORMAL,
	FREE
}

public class SelectingCharactor : MonoBehaviour {
    
    public bool loaded;
    SelectingCharactorNo no;

	PlayModeState m_playMode;
	public PlayModeState PlayMode
	{
		get { return m_playMode; }
		set { m_playMode = value; }
	}
	// Use this for initialization
	void Start () {
        loaded = false;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	    if(loaded)
        {
         //   Destroy(this.gameObject);
        }
	}

    public void SetCharNo(int _no)
    {
        no = (SelectingCharactorNo)_no;
    }

    public SelectingCharactorNo GetCharNo()
    {
        loaded = true;
        return no;
    }

}
