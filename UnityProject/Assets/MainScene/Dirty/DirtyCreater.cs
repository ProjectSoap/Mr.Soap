using UnityEngine;
using System.Collections;
/*
#define CREATE_MAX 10   // 念の為に
*/
public class DirtyCreater : MonoBehaviour {
	
	DirtySystem parntDirtySystem;   // 管理用
	public DirtySystem ParntDirtySystem
	{
		get { return parntDirtySystem; }
		set { parntDirtySystem = value; }
	}
	[SerializeField,Header("出現場所(DirtyAppearancePoint)の管理配列")]
	GameObject[] appearancePoints;   // 出現位置管理

	bool isMyDityDestroy;  // 消されたかどうか

	bool isRangeOut;   // マップ範囲外であるか

	char rangeOutCountPar10Seconds; // ミニマップ範囲外に出て何毎10秒いたか

	float deltaTime;    // 消されてからの経過時間
	float oldDeltaTime;    // 前の経過時間

	float countStartTime;
	bool isCountPar10Seconds; // 毎10秒経過したかのフラグ
	uint affiliationArea;   // 所属区画

	bool isAdhereCar;   // 車に付着しているか
	public bool IsAdhereCar
	{
		get { return isAdhereCar; }
		set { isAdhereCar = value; }
	}

	bool isReality;	//レア判定

	public bool IsReality
	{
		get { return isReality; }
		set { isReality = value; }
	}

	public uint AffiliationArea
	{
		get { return affiliationArea; }
		set { affiliationArea = value; }
	}
	// Use this for initialization
	void Start ()
	{
	}


	void Awake()
	{
		countStartTime = Time.deltaTime;
		isMyDityDestroy =false;

		for (int i = 0; i < appearancePoints.Length; i++)
		{

			appearancePoints[i].GetComponent<DityApparancePosition>().MyCreater = this;

		}
		if (0 < appearancePoints.Length)
		{
			appearancePoints[0].GetComponent<DityApparancePosition>().IsCreate = true;
		}

#if DEBUG
		if (GetComponent<MeshRenderer>() != null)
		{
			Destroy(GetComponent<MeshRenderer>());
		}
#endif

		// リリースビルド時エディット用のメッシュがついてたらエラー！
#if DEBUG
		if (GetComponent<MeshRenderer>() != null)
		{
			Debug.Log(this);
			Destroy(GetComponent<MeshRenderer>());
		}
#endif
	}
	

	public void NoticeDestroy()
	{
		ParntDirtySystem.NoticeDestroyToSystem(this);
		isMyDityDestroy = true;
	}

	// Update is called once per frame
	void Update ()
	{
		bool isCreateFlag = false;
		if (isMyDityDestroy)
		{
			// どれか消されてんなら生成フラグ立てる
			isCreateFlag = true;
		}

		// 汚れが一個でも消されてんなら生成するためのカウントを行う
		if (isCreateFlag)
		{
			CountTime();
		}
		// 生成カウントが毎10秒経過
		if (isCountPar10Seconds)
		{
			CreateDirty();
		}
	}
	

	public void CheckDistance(Vector3 playerPosition)
	{
		float distance = Vector3.Distance(playerPosition, transform.position);
		if (distance >= 15.0f)
		{
			isRangeOut = true;
		}
		else
		{
			isRangeOut = false;
		}
	}

	public void CreateDirty()
	{

		if (isCountPar10Seconds)
		{
			bool isCreate = false;
			// 毎10秒経過回数で確率アップ
			switch (rangeOutCountPar10Seconds)
			{
				case (char)1:
					if (Random.Range(0, 100) <= 20)
					{
						isCreate = true;
						deltaTime = 0;
						rangeOutCountPar10Seconds = (char)0;
					}
					break;
				case (char)2:
					if (Random.Range(0, 100) <= 30)
					{
						isCreate = true;
						deltaTime = 0;
						rangeOutCountPar10Seconds = (char)0;
					}
					break;
				case (char)3:
					if (Random.Range(0, 100) <= 60)
					{
						isCreate = true;
						deltaTime = 0;
						rangeOutCountPar10Seconds = (char)0;
					}
					break;
				case (char)4:
					if (true)
					{
						isCreate = true;
						deltaTime = 0;
						rangeOutCountPar10Seconds = (char)0;
					}
					break;
				default:
					isCreate = false;
					break;
			}

			// フラグが立ってるなら汚れ作る
			if (isCreate)
			{
				int num = appearancePoints.Length;
				num = (int)Random.Range((int)0, (int)num);
				appearancePoints[num].GetComponent<DityApparancePosition>().IsCreate = true;
			}
		}
		isCountPar10Seconds = false;

	} 

	void CountTime()
	{

		// 範囲外ならカウント
		if (isRangeOut)
		{
			oldDeltaTime = deltaTime;
			deltaTime += Time.deltaTime;

			// 毎10秒経過したか
			if (((int)deltaTime - 10 * rangeOutCountPar10Seconds)  >= 10)
			{
				isCountPar10Seconds = true;
				rangeOutCountPar10Seconds++;
			}
			else
			{
				isCountPar10Seconds = false;
			}
		}

	}
}
