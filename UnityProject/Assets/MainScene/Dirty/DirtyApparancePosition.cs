using UnityEngine;
using System.Collections;

public class DirtyApparancePosition : MonoBehaviour {

	[SerializeField]
	GameObject dirtyObject;   // 汚れオブジェクト

	[SerializeField,Header("広範囲の汚れが広がる半径")]
	Vector2 createRange; // 生成範囲

	[SerializeField, Header("生成される個数"),Tooltip("複数個に設定すると広範囲の汚れとして円形に展開されます")]
	uint createNumber; // 生成個数


	DirtyCreater myCreater;   // 管理用
	public DirtyCreater MyCreater
	{
		get { return myCreater; }
		set { myCreater = value; }
	}
	DirtyObjectScript[] dirtyObjectInstance;   // 管理用
	
	bool[] isMyDityDestroy;  // 消されたかどうか
	
	public bool IsHaveObject
	{
		get
		{
			for (int i = 0;i < dirtyObjectInstance.Length; i++)
			{
				if (dirtyObjectInstance[i] != null)
				{
					return true;
				}
			}
			return false;
		}
	}
	bool isRangeOut;   // マップ範囲外であるか

	///
	/// <summary>   レア判定.   </summary>
	///
	[SerializeField]
	public bool m_isReality;
	public bool Reality
	{
		get { return m_isReality; }
		set { m_isReality = value; }
	}
	public uint affiliationArea;
	public PlayerCharacterController m_player;
	public PlayerCharacterController Player
	{
		get { return m_player; }
		set { m_player = value; }
	}
	// Use this for initialization
	void Start ()
	{
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
			Destroy(GetComponent<MeshRenderer>());
		}
#endif
		dirtyObjectInstance = new DirtyObjectScript[createNumber];
		isMyDityDestroy = new bool[createNumber];
	}


	bool isCreate;
	public bool IsCreate
	{
		get { return isCreate; }
		set { isCreate = value; }
	}
	
	public uint CreateNumber
	{
		get { return createNumber; }
		set { createNumber = value; }
	}
	

	// Update is called once per frame
	void Update () {

		// フラグが立ってるなら汚れ作る
		if (IsCreate)
		{
			for (int i = 0; i < CreateNumber; i++)
			{
				Vector3 pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * i * 360.0f / (float)CreateNumber), Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)CreateNumber), 0);
				pos.x *= createRange.x;
				pos.y *= createRange.y;
				pos = transform.rotation * pos;

				// 消されたオブジェクトは新たに作る
				if (dirtyObjectInstance[i] == null)
				{

					dirtyObjectInstance[i] = ((GameObject)Instantiate(dirtyObject, pos + transform.position, transform.rotation)).GetComponent<DirtyObjectScript>();

					// 親子関係的なものを構築
					DirtyObjectScript obj = dirtyObjectInstance[i].GetComponent<DirtyObjectScript>();
					dirtyObjectInstance[i].transform.parent = this.transform;
					obj.MyPoint = this;   // 汚れスクリプトに自分を伝える
					isMyDityDestroy[i] = false;
					obj.Player = Player;
					if (m_isReality)
					{
						obj.Reality = true;
					}
					obj.SwitchMaterial((int)Random.Range(0,7), affiliationArea);
				}

			}
			isCreate = false;
		}
	}

	public void NoticeDestroy(DirtyObjectScript dirty)
	{
		MyCreater.NoticeDestroy(dirty);
	}

}
