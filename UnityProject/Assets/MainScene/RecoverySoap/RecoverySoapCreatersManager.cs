﻿using UnityEngine;
using System.Collections;

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

	[SerializeField]
	float countSecond;

	[SerializeField]
	uint minTimeForInstance;

	[SerializeField]
	uint maxTimeForInstance;

	[SerializeField]
	GameObject[] RecoverySoapCreaters1;
	[SerializeField]
	GameObject[] RecoverySoapCreaters2;
	[SerializeField]
	GameObject[] RecoverySoapCreaters3;
	[SerializeField]
	GameObject[] RecoverySoapCreaters4;

	[SerializeField]
	GameObject player;

	NorticeDirectionRecaverySoap arrow;
	NorticeUIOfAppearanceRecoverySoap sprite;

	public float m_decisionSecond = 2;  // 発生時間内での判定時間(秒)
	public float m_decisionSecondNow;

    bool m_isApparance;
	// Use this for initialization
	void Start ()
	{
		m_decisionSecondNow = m_decisionSecond;
		isRunning = false;

		arrow = GameObject.Find("NorticeRecoveryDirection").GetComponent<NorticeDirectionRecaverySoap>();
		sprite = GameObject.Find("NorticeRecoverySoapSprite").GetComponent<NorticeUIOfAppearanceRecoverySoap>();
		player = GameObject.Find("PlayerCharacter");

		CheckRecordCondition saveData = GameObject.Find("CheckRecordCondition").GetComponent<CheckRecordCondition>();
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMinarai))
		{
			isUnlockArea2 = true;
		}
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiItininnmae))
		{
			isUnlockArea2 = true;
		}
		if (saveData.CheckRecordConditionClear(CheckRecordCondition.ERecordName.OtosiMeizin))
		{
			isUnlockArea2 = true;
		}
	}
	
	// Update is called once per frame
	void Update ()
	{
		countSecond += Time.deltaTime;


		CheckDistanceFromPlayer();
		// 指定した秒数内
		if (minTimeForInstance <= countSecond && countSecond <= maxTimeForInstance)
		{
			//判定時間であるか
			m_decisionSecondNow -= Time.deltaTime;
			if (m_decisionSecondNow <= 0) 
			{
				m_decisionSecondNow = m_decisionSecond - m_decisionSecondNow;

				DecisionCreate();
			}
		}
		if (maxTimeForInstance < countSecond)
		{
			countSecond = 0; // 最大時間を超えたらリセット
			DecisionCreate();

		}
}

	void DecisionCreate()
	{
		float difference = maxTimeForInstance - minTimeForInstance;	// 最大と最小の差分
		float d = ((float)maxTimeForInstance - countSecond) / (float)difference;
		if (d < Random.value)
		{
			countSecond = 0;    // カウントリセット
			uint elemMax;
			elemMax = (uint)RecoverySoapCreaters1.Length;
			if (elemMax > 0)
			{
				uint randElem;
				RecoverySoapCreater script;

				// 区画1
				randElem = (uint)Random.Range(0.0f, (float)elemMax);
				script = RecoverySoapCreaters1[randElem].GetComponent<RecoverySoapCreater>();
				if (script)
				{
					if (!script.IsHaveRevoverySoap && script.IsRangeOut)
					{
						script.IsInstance = true;
						sprite.IsAppearance = true;
					}

				}
			}

			if (isUnlockArea2)
			{

				// 区画2		
				elemMax = (uint)RecoverySoapCreaters2.Length;
				if (elemMax > 0)
				{
					uint randElem;
					RecoverySoapCreater script;

					randElem = (uint)Random.Range(0.0f, (float)elemMax);
					script = RecoverySoapCreaters2[randElem].GetComponent<RecoverySoapCreater>();
					if (script)
					{
						if (!script.IsHaveRevoverySoap && script.IsRangeOut)
						{
							script.IsInstance = true;
							sprite.IsAppearance = true;
						}

					}
				}
			}
			if (isUnlockArea3)
			{

				// 区画3
				elemMax = (uint)RecoverySoapCreaters3.Length;
				if (elemMax > 0)
				{
					uint randElem;
					RecoverySoapCreater script;

					randElem = (uint)Random.Range(0.0f, (float)elemMax);
					script = RecoverySoapCreaters3[randElem].GetComponent<RecoverySoapCreater>();
					if (script)
					{
						if (!script.IsHaveRevoverySoap && script.IsRangeOut)
						{
							script.IsInstance = true;
							sprite.IsAppearance = true;
						}

					}
				}
			}
			if (isUnlockArea4)
			{

				// 区画4
				elemMax = (uint)RecoverySoapCreaters4.Length;
				if (elemMax > 0)
				{
					uint randElem;
					RecoverySoapCreater script;

					randElem = (uint)Random.Range(0.0f, (float)elemMax);
					script = RecoverySoapCreaters4[randElem].GetComponent<RecoverySoapCreater>();
					if (script)
					{
						if (!script.IsHaveRevoverySoap && script.IsRangeOut)
						{
							script.IsInstance = true;
							sprite.IsAppearance = true;
						}

					}
				}
			}
		}
	}


void CheckDistanceFromPlayer()
	{
		arrow.Near = 1000;
		arrow.IsAppearance = false;
		// 区画1のせっけん出現候補地とプレイヤーの距離を検証
		
		{
			for (uint i = 0; i < RecoverySoapCreaters1.Length; i++)
			{
				RecoverySoapCreater creater = RecoverySoapCreaters1[i].GetComponent<RecoverySoapCreater>();
				creater.CheckDistance(player.transform.position);
				if (creater.IsHaveRevoverySoap)
				{

					arrow.IsAppearance = true;
					if (arrow.Near > Vector3.Distance(creater.transform.position, player.transform.position))
					{
						arrow.RecoverySoap = creater.RecoverySoap;
						arrow.Near = Vector3.Distance(creater.transform.position, player.transform.position);
					}
				} 

			}
		}
		
		// 区画2のせっけん出現候補地とプレイヤーの距離を検証
		if (isUnlockArea2)
		{
			for (uint i = 0; i < RecoverySoapCreaters2.Length; i++)
			{
				RecoverySoapCreater creater = RecoverySoapCreaters2[i].GetComponent<RecoverySoapCreater>();
				creater.CheckDistance(player.transform.position);
				if (creater.IsHaveRevoverySoap)
				{

					arrow.IsAppearance = true;
					if (arrow.Near > Vector3.Distance(creater.transform.position, player.transform.position))
					{
						arrow.RecoverySoap = creater.RecoverySoap;
						arrow.Near = Vector3.Distance(creater.transform.position, player.transform.position);
					}
				}

			}
		}

		// 区画3のせっけん出現候補地とプレイヤーの距離を検証
		if (isUnlockArea3)
		{
			for (uint i = 0; i < RecoverySoapCreaters3.Length; i++)
			{
				RecoverySoapCreater creater = RecoverySoapCreaters3[i].GetComponent<RecoverySoapCreater>();
				creater.CheckDistance(player.transform.position);
				if (creater.IsHaveRevoverySoap)
				{

					arrow.IsAppearance = true;
					if (arrow.Near > Vector3.Distance(creater.transform.position, player.transform.position))
					{
						arrow.RecoverySoap = creater.RecoverySoap;
						arrow.Near = Vector3.Distance(creater.transform.position, player.transform.position);
					}
				}

			}
		}

		// 区画4のせっけん出現候補地とプレイヤーの距離を検証
		if (isUnlockArea4)
		{
			for (uint i = 0; i < RecoverySoapCreaters4.Length; i++)
			{
				RecoverySoapCreater creater = RecoverySoapCreaters4[i].GetComponent<RecoverySoapCreater>();
				creater.CheckDistance(player.transform.position);
				if (creater.IsHaveRevoverySoap)
				{

					arrow.IsAppearance = true;
					if (arrow.Near > Vector3.Distance(creater.transform.position, player.transform.position))
					{
						arrow.RecoverySoap = creater.RecoverySoap;
						arrow.Near = Vector3.Distance(creater.transform.position, player.transform.position);
					}
				}

			}
		}
	}
}
