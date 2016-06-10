using UnityEngine;
using System.Collections;

public enum SelectingCharactorNo
{
    SOAP,
    SOAP0,
    SOAPTYAN
}

public class SelectingCharactor : MonoBehaviour {
    
    public bool loaded;
    SelectingCharactorNo no;

	// Use this for initialization
	void Start () {
        loaded = false;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	    if(loaded)
        {
            Destroy(this.gameObject);
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
