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

	float m_countStartTime;
	bool isCountPar10Seconds; // 毎10秒経過したかのフラグ
	uint affiliationArea;   // 所属区画
	
	[SerializeField,Header("10秒毎の生成確率(%)")]
	uint[] m_probabilityPar10Seconds = new uint[6];
	
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

	PlayerCharacterController m_player;
	public PlayerCharacterController Player
	{
		get { return m_player; }
		set { m_player = value; }
	}
	// Use this for initialization
	void Start ()
	{
		m_countStartTime = Time.deltaTime;
		isMyDityDestroy =false;

		for (int i = 0; i < appearancePoints.Length; i++)
		{

			appearancePoints[i].GetComponent<DirtyApparancePosition>().MyCreater = this;
			appearancePoints[i].GetComponent<DirtyApparancePosition>().Player = m_player;
		}
		if (0 < appearancePoints.Length)
		{
			appearancePoints[0].GetComponent<DirtyApparancePosition>().IsCreate = true;
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


	void Awake()
	{
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
			//switch (rangeOutCountPar10Seconds)
			//{
			//	case (char)1:
			//		if (Random.Range(0, 100) <= 20)
			//		{
			//			isCreate = true;
			//			deltaTime = 0;
			//			rangeOutCountPar10Seconds = (char)0;
			//		}
			//		break;
			//	case (char)2:
			//		if (Random.Range(0, 100) <= 30)
			//		{
			//			isCreate = true;
			//			deltaTime = 0;
			//			rangeOutCountPar10Seconds = (char)0;
			//		}
			//		break;
			//	case (char)3:
			//		if (Random.Range(0, 100) <= 60)
			//		{
			//			isCreate = true;
			//			deltaTime = 0;
			//			rangeOutCountPar10Seconds = (char)0;
			//		}
			//		break;
			//	case (char)4:
			//		if (true)
			//		{
			//			isCreate = true;
			//			deltaTime = 0;
			//			rangeOutCountPar10Seconds = (char)0;
			//		}
			//		break;
			//	default:
			//		isCreate = false;
			//		break;
			//}

			for (int  i = 0;  i < m_probabilityPar10Seconds.Length;i++)
			{

				if (Random.Range(0, 100) <= m_probabilityPar10Seconds[i])
				{
					isCreate = true;
					deltaTime = 0;
					rangeOutCountPar10Seconds = (char)0;
				}
			}

			// フラグが立ってるなら汚れ作る
			if (isCreate)
			{
				int num = appearancePoints.Length;
				bool isCreated = false;	// 今回生成したか
				for (int i = 0;i < num;i++)
				{
					// 一つでもオブジェクトが残っているかチェック
					if (appearancePoints[i].GetComponent<DirtyApparancePosition>().IsHaveObject)
					{
						// 残っていたらそこで生成フラグを立てる
						appearancePoints[i].GetComponent<DirtyApparancePosition>().IsCreate = true;
						isCreated = true;
						break;
					}
				}
				if (isCreated == false)
				{
					// どの出現ポイントも一つもオブジェクトを保持していない
					num = (int)Random.Range((int)0, (int)num);
					appearancePoints[num].GetComponent<DirtyApparancePosition>().IsCreate = true;
					isCreated = true;
				}

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
