﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterIconUI : MonoBehaviour {
    
    PlayerCharacterController player;
    NumberSwitcher switcher;
    Image iconOverFrame;
	public int m_characterNum;
	public int m_characterMax = 3;

	// キャラの状態その1 こっち最優先に切り替え
	enum ECharacterState
	{
		NORMAL,
		DAMAGE,
		HEAL,
		DEAD
	}

	// キャラの状態その2 基本こっち
	enum EHealthState
	{
		COMPLETE_RECOVERY,	// だいたい全快
		MINOR_INJURY,		// 軽傷
		SERIOUS_INJURY		// 重傷

	}

	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
        switcher = this.transform.FindChild("SizeIcon").gameObject.GetComponent<NumberSwitcher>();
        iconOverFrame = GameObject.Find("SizeIconOverFrame").GetComponent<Image>();

		m_characterNum =(int)SceneData.characterSelect;
		

	}

	// Update is called once per frame
	void Update ()
    {
        float size = player.size;
        if (60.0f <= size )
        {
            switcher.SetNumber(0 + m_characterMax * m_characterNum);
            iconOverFrame.color = new UnityEngine.Color(0.5f, 0.5f, 1.0f);
        }
        else if (30 <= size && size < 60.0f)
        {
            switcher.SetNumber(1 + m_characterMax * m_characterNum);
            iconOverFrame.color = new UnityEngine.Color(0.9f, 0.9f, 0.05f);
        }
        else
        {
            switcher.SetNumber(2 + m_characterMax * m_characterNum);
            iconOverFrame.color = new UnityEngine.Color(0.9f, 0.2f, 0.2f);
        }
    }
}