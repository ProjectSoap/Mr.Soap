using UnityEngine;
using System.Collections;

public enum SelectingCharactorNo
{
    SOAP,
    SOAP0,
    SOAPTYAN
}

<<<<<<< HEAD
=======

public enum PlayModeState
{
	NORMAL,
	FREE
}

>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
public class SelectingCharactor : MonoBehaviour {
    
    public bool loaded;
    SelectingCharactorNo no;

<<<<<<< HEAD
=======
	PlayModeState m_playMode;
	public PlayModeState PlayMode
	{
		get { return m_playMode; }
		set { m_playMode = value; }
	}
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
	// Use this for initialization
	void Start () {
        loaded = false;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	    if(loaded)
        {
<<<<<<< HEAD
            Destroy(this.gameObject);
=======
         //   Destroy(this.gameObject);
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
