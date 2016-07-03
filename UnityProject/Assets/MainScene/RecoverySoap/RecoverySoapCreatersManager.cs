using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RecoverySoapCreatersManager : MonoBehaviour {
	// 稼働しているか
	bool isRunning;
	public bool IsRunning
	{
		get { return isRunning; }
		set { isRunning = value; }
	}
	[SerializeField]
	bool isUnlockArea1 = true;
	[SerializeField]
	bool isUnlockArea2 = false;
	[SerializeField]
	bool isUnlockArea3 = false;
	[SerializeField]
	bool isUnlockArea4 = false;

	public float m_baseProbabilityCurrent = 30;	
	public float m_baseProbabilityOther = 70;

	[SerializeField]
	float countSecond;

	[SerializeField]
	uint minTimeForInstance;

	[SerializeField]
	uint maxTimeForInstance;
	[SerializeField]
	uint m_countUptoMaxTime = 10;
	uint m_countNowUptoMaxTime;	// 現在カウントしてる分

	float m_timeForInstance;

	[SerializeField]
	RecoverySoapCreater[] RecoverySoapCreaters;
	[SerializeField]
	GameObject[] RecoverySoapCreaters1;
	[SerializeField]
	GameObject[] RecoverySoapCreaters2;
	[SerializeField]
	GameObject[] RecoverySoapCreaters3;
	[SerializeField]
	GameObject[] RecoverySoapCreaters4;

	[SerializeField]
	PlayerCharacterController m_player;

	NorticeDirectionRecaverySoap arrow;
	

	public float m_decisionSecond = 2;  // 発生時間内での判定時間(秒)
	public float m_decisionSecondNow;

	bool m_isApparance;	// 全区間でせっけん出現しているか
	// Use this for initialization
	void Start ()
	{
		Init();
	}

	void Init()
	{
		m_decisionSecondNow = m_decisionSecond;
		isRunning = false;

		float s;
		if (m_countNowUptoMaxTime <= 0)
		{
			s = 0;
		}
		else
		{
			s = m_countNowUptoMaxTime / m_countUptoMaxTime;
		}
		m_countNowUptoMaxTime++;
		countSecond = 0;    // カウントリセット
		//次のタイム引き延ばし
		m_timeForInstance = Mathf.Lerp(minTimeForInstance, maxTimeForInstance, s);

		arrow = GameObject.Find("NorticeRecoveryDirection").GetComponent<NorticeDirectionRecaverySoap>();
		m_player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();

		CheckRecordCondition saveData = GameObject.Find("CheckRecordCondition").GetComponent<CheckRecordCondition>();
		isUnlockArea1 = true;
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMinarai))
		{
			isUnlockArea2 = true;
		}
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiItininnmae))
		{
			isUnlockArea3 = true;
		}
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMeizin))
		{
			isUnlockArea4 = true;
		}

		
	}

	// Update is called once per frame
	void Update ()
	{
		if (m_isApparance == false)
		{

			countSecond += Time.deltaTime;
			
			if (m_timeForInstance <= countSecond)
			{
				DecisionCreate();
			}
		}
	}
	uint DecisionCreateSection()
	{
		int[] area = new int[4];
		float[] probability = new float[4];
		area[0] = (int)m_player.inSectionNow - 4;	// 現在から上の位置にある小区画
		area[1] = (int)m_player.inSectionNow - 1;	// 現在から左の位置にある小区画
		area[2] = (int)m_player.inSectionNow + 1;	// 現在から右の位置にある小区画
		area[3] = (int)m_player.inSectionNow + 4;	// 現在から下の位置にある小区画

		// 候補地の同区画の数
		int currentCount = 0;
		// 候補地の別区画の数
		int otherCount = 0;
		for (int i = 0; i < 4; i++)
		{
			// 範囲外は除外
			if (area[i] < 0 || 15 < area[i])
			{
				probability[i] = 0;
			}
			else
			{
				// 区画が同じだったらカウント
				if (CheckSection(area[i]) == CheckSection((int)m_player.inSectionNow))
				{
					currentCount++;
				}
				// 別のやつもカウント
				else
				{
					// 全区間アンロック
					if (isUnlockArea4 == true)
					{
						otherCount++;
					}
					// 区間3までアンロックかつチェック結果が3以下
					else if (isUnlockArea3 == true &&  CheckSection(area[i]) <= 3)
					{
						otherCount++;
					}
					// 区間2までアンロックかつチェック結果が2以下
					else if (isUnlockArea2 == true && CheckSection(area[i]) <= 2)
					{
						otherCount++;
					}
					// 区間1までアンロックかつチェック結果が1以下
					else if (isUnlockArea1 == true && CheckSection(area[i]) <= 1)
					{
						otherCount++;
					}
				}
			}

		}
		float probabilityCurrent = 0;
		float probabilityOther = 0;
		if (otherCount > 0)
		{
			// 他区画の基本確立を他区画の候補地の個数分で割る
			probabilityOther = m_baseProbabilityOther / otherCount;
			// 現在区画の基本確立を現在区画の候補地の個数分で割る
			probabilityCurrent = m_baseProbabilityCurrent / currentCount;
		}
		else
		{
			// 他区画がすべて使えないなら現在区画の確率が100パ
			probabilityCurrent = 100.0f / currentCount;
		}

		// 確率の確定

		for (int i = 0; i < 4; i++)
		{
			// 存在しない区画は0パー
			if (area[i] < 0 || 15 < area[i])
			{
				probability[i] = 0;
			}
			else
			{
				// 同じ区画だったら
				if (CheckSection(area[i]) == CheckSection((int)m_player.inSectionNow))
				{
					probability[i] = probabilityCurrent;
				}
				else
				{
					probability[i] = probabilityOther;
				}
			}
		}
		float random = Random.value * 100;
		float probabilityStack = 0;
		for (int i = 0; i < 4; i++)
		{
			probabilityStack += probability[i];
			// 同じ区画だったら
			if (random < probabilityStack)
			{
				return (uint)area[i];
			}
		}
		return 0;
	}

	// 小区画がどの区画に属しているかチェック
	int CheckSection(int area)
	{
		if (area == 0 || area == 1 || area == 4 || area == 5)
		{
			return 1;
		}

		else if (area == 2 || area == 3 || area == 6 || area == 7)
		{
			return 2;
		}

		else if (area == 8 || area == 9 || area == 12 || area == 13)
		{
			return 3;
		}
		else 
		{
			return 4;
		}
	}

	void DecisionCreate()
	{
		//float difference = maxTimeForInstance - minTimeForInstance;	// 最大と最小の差分
		//float d = ((float)maxTimeForInstance - countSecond) / (float)difference;
		
		{
			float s = m_countNowUptoMaxTime / m_countUptoMaxTime;
			m_countNowUptoMaxTime++;
			countSecond = 0;    // カウントリセット
			//次のタイム引き延ばし
			m_timeForInstance = Mathf.Lerp(minTimeForInstance,maxTimeForInstance,s);
			RecoverySoapCreaters[DecisionCreateSection()].CreateSoap();
			m_isApparance = true;


		}
	}

	public void NorticeDestroy()
	{
		m_isApparance = false;
	}
	
}
