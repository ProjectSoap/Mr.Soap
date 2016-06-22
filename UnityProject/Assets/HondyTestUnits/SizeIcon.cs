using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SizeIcon : MonoBehaviour {
    
    PlayerCharacterController player;
    NumberSwitcher switcher;
    Image iconOverFrame;
	public int m_characterNum;
	public int m_characterMax = 3;
	// Use this for initialization
	void Start ()
    {
        player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
        switcher = this.transform.FindChild("SizeIcon").gameObject.GetComponent<NumberSwitcher>();
        iconOverFrame = GameObject.Find("SizeIconOverFrame").GetComponent<Image>();

		m_characterNum =(int)GameObject.Find("CharNo").GetComponent<SelectingCharactor>().GetCharNo();
		

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
