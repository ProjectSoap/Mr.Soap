using UnityEngine;
using System.Collections;

public class BarricadeManager : MonoBehaviour {

#if DEBUG
    public bool m_isDubug = false;
#endif
	public BarricadeObject[] m_barricade;

	// Use this for initialization
	void Start () {



        if (m_isDubug)
        {
            for (int i = 0; i < m_barricade.Length; i++)
            {
                if (m_barricade[i].m_lockArea <= 4)
                {
                    m_barricade[i].m_isUnlock = true;
                }
            }
        }
		if (
        GameObject.Find("SaveDataManager").transform.FindChild("CheckRecordCondition").GetComponent<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMinarai))
		{
			for (int i= 0;i < m_barricade.Length;i++)
			{
				if (m_barricade[i].m_lockArea <= 2)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}

        if (GameObject.Find("SaveDataManager").transform.FindChild("CheckRecordCondition").GetComponent<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiItininnmae))
		{
			for (int i = 0; i < m_barricade.Length; i++)
			{
				if (m_barricade[i].m_lockArea <= 3)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}

        if (GameObject.Find("SaveDataManager").transform.FindChild("CheckRecordCondition").GetComponent<CheckRecordCondition>().CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMeizin))
		{
			for (int i = 0; i < m_barricade.Length; i++)
			{
				if (m_barricade[i].m_lockArea <= 4)
				{
					m_barricade[i].m_isUnlock = true;
				}
			}
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
