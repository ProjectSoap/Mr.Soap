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

    float loadWaitTime;
    
	PlayModeState m_playMode;
	public PlayModeState PlayMode
	{
		get { return m_playMode; }
		set { m_playMode = value; }
	}
	// Use this for initialization
	void Start () {
        loaded = false;
        loadWaitTime = 0.0f;
        DontDestroyOnLoad(this);
	}
	
	// Update is called once per frame
	void Update () {
	    if(loaded)
        {
            loadWaitTime += Time.deltaTime;
            if (loadWaitTime > 5.0f)
            {
                Destroy(this);
            }
        }
	}

    public void SetCharNo(int _no)
    {
        no = (SelectingCharactorNo)_no;

        switch (no)
        {
            case SelectingCharactorNo.SOAP:
                SceneData.characterSelect = SceneData.CharacterSelect.SekkenKun;
                break;
            case SelectingCharactorNo.SOAP0:
                SceneData.characterSelect = SceneData.CharacterSelect.SekkenHero;
                ActionRecordManager.sActionRecord.isSekkenKun0Play = true;
                break;
            case SelectingCharactorNo.SOAPTYAN:
                SceneData.characterSelect = SceneData.CharacterSelect.SekkenChan;
                ActionRecordManager.sActionRecord.isSekkenChanPlay = true;
                break;
            default:
                break;
        }
    }

    public SelectingCharactorNo GetCharNo()
    {
        loaded = true;
        return no;
    }

}
