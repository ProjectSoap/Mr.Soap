using UnityEngine;
using UnityEditor;

public class Test : Editor
{

	[MenuItem("Project Soap/汚れポイントの編集用メッシュを削除")]
	public static void CreateNewSomething()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj.activeInHierarchy && obj.tag == "DirtyPoint")
			{
				MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

				if (renderer != null)
				{
					DestroyImmediate(renderer);
				}

				MeshFilter filter = obj.GetComponent<MeshFilter>();

				if (filter != null)
				{
					DestroyImmediate(filter);
				}

				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/汚れポイントの編集用メッシュを追加")]
	public static void AddEditMeshForDirtyPoint()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj.activeInHierarchy && obj.tag == "DirtyPoint")
			{
				MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

				if (renderer == null)
				{
					renderer = obj.AddComponent<MeshRenderer>();
					renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
					renderer.sharedMaterial = new Material(Shader.Find("Unlit/Transparent"));

				}

				MeshFilter filter = obj.GetComponent<MeshFilter>();

				if (filter == null)
				{
					filter = obj.AddComponent<MeshFilter>();
					// create -Z direction Billboard
					Mesh mesh = new Mesh();
					mesh.name = "Plane(-Z)";

					mesh.vertices = new Vector3[] {
					new Vector3( -0.5f, -0.5f, 0.0f ),
					new Vector3( -0.5f, 0.5f, 0.0f ),
					new Vector3( 0.5f, -0.5f, 0.0f ),
					new Vector3( 0.5f, 0.5f, 0.0f ),
					  };

					mesh.triangles = new int[] {
					0, 1, 2, 1, 3, 2
					};

					mesh.uv = new Vector2[] {
					new Vector2( 0.0f, 1.0f ),
					  new Vector2( 0.0f, 0.0f ),
					 new Vector2( 1.0f, 1.0f ),
					new Vector2( 1.0f, 0.0f ),
					};

					mesh.RecalculateNormals();

					// attack mesh to filter
					filter.mesh = mesh;
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/ミニマップ用クアッドを追加")]
	public static void AddMiniMapQuadForBuilding()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj != null && obj.activeInHierarchy && obj.layer == LayerMask.NameToLayer( "Building"))
			{
				// オブジェクト生成
				BoxCollider collider = obj.GetComponent<BoxCollider>();

				if (obj.tag == "StaticCar" || collider == null)
				{
					continue;
				}
				if (obj.transform.FindChild("MiniMapQuad") == null)
				{
					GameObject newObject = new GameObject("MiniMapQuad"); ;
					newObject.transform.parent = obj.transform; // 親子付
					newObject.transform.position = new Vector3(obj.transform.position.x,0, obj.transform.position.z);
					newObject.transform.Translate(new Vector3(0,-490,0));
					newObject.transform.rotation = new Quaternion();
					MeshRenderer renderer = newObject.AddComponent<MeshRenderer>();
					renderer = newObject.GetComponent<MeshRenderer>();
					if (renderer)
					{
						renderer.sharedMaterial = Resources.Load("Materials/MiniMapBuildingIconMaterial") as Material;
					}

				MeshFilter filter =  newObject.AddComponent<MeshFilter>();
				filter = newObject.GetComponent<MeshFilter>();
				if (filter)
				{
					// create -Z direction Billboard
					Mesh mesh = new Mesh();
					mesh.name = "Plane(-Z)";

					mesh.vertices = new Vector3[] {
					new Vector3( -0.5f, 0.0f, -0.5f ),
					new Vector3( -0.5f, 0.0f, 0.5f ),
					new Vector3( 0.5f, 0.0f, -0.5f ),
					new Vector3( 0.5f, 0.0f, 0.5f ),
					  };
					if (collider)
					{

							if (obj.tag == "Barricade")
							{

								newObject.transform.localScale =
									new Vector3
									(
										50,
										 newObject.transform.localScale.y * obj.transform.localScale.y * collider.size.y,
										 newObject.transform.localScale.z * obj.transform.localScale.z * collider.size.z);
							}
							else
							{
								newObject.transform.localScale =
								new Vector3
								(
									 newObject.transform.localScale.x * obj.transform.localScale.x * collider.size.x,
									 newObject.transform.localScale.y * obj.transform.localScale.y * collider.size.y,
									 newObject.transform.localScale.z * obj.transform.localScale.z * collider.size.z);
							}


							

							if (obj.transform.parent)
							{

								Transform pearent = obj.transform.parent;

								if (obj.tag == "Barricade")
								{

									newObject.transform.localScale =
										new Vector3
										(
										pearent.lossyScale.x / pearent.localScale.x * 150,
										pearent.lossyScale.y / pearent.localScale.y * collider.size.y,
										pearent.lossyScale.z / pearent.localScale.z * collider.size.z);
								}
								else
								{
									newObject.transform.localScale =
									new Vector3
									(
										pearent.lossyScale.x / pearent.localScale.x * collider.size.x,
										pearent.lossyScale.y / pearent.localScale.y * collider.size.y,
										pearent.lossyScale.z / pearent.localScale.z * collider.size.z);
								}
								newObject.transform.Translate(new Vector3
									(
										pearent.lossyScale.x * obj.transform.localScale.x * collider.center.x,
										pearent.lossyScale.y * obj.transform.localScale.y * collider.center.y,
										pearent.lossyScale.z * obj.transform.localScale.z * collider.center.z));

							}

							mesh.triangles = new int[] {
						0, 1, 2, 1, 3, 2
						};

						mesh.uv = new Vector2[] {
						new Vector2( 0.0f, 1.0f ),
						  new Vector2( 0.0f, 0.0f ),
						 new Vector2( 1.0f, 1.0f ),
						new Vector2( 1.0f, 0.0f ),
						};

						mesh.RecalculateNormals();

						// attack mesh to filter
						filter.mesh = mesh;

					}
				}
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/ミニマップ用クアッドを削除（テスト版）")]
	public static void DeleteMiniMapQuadForBuilding()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if ( obj != null && obj.layer == LayerMask.NameToLayer("Building"))
			{
				// オブジェクト検索

				if (obj.transform.FindChild("MiniMapQuad") != null)
				{
					GameObject quad = obj.transform.FindChild("MiniMapQuad").gameObject;
					DestroyImmediate(quad);
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/回復石鹸出現場所の編集用メッシュを追加")]
	public static void AddEditMeshForRecoverySoapAppearancePoint()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj.activeInHierarchy && obj.tag == "RecoverySoapAppearancePoint")
			{
				MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

				if (renderer == null)
				{
					renderer = obj.AddComponent<MeshRenderer>();
					renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
					renderer.sharedMaterial = new Material(Shader.Find("Unlit/Transparent"));

				}

				MeshFilter filter = obj.GetComponent<MeshFilter>();

				if (filter == null)
				{
					filter = obj.AddComponent<MeshFilter>();
					// create -Z direction Billboard
					Mesh mesh = new Mesh();
					mesh.name = "Plane(-Z)";

					mesh.vertices = new Vector3[] {
					new Vector3( -0.5f, 0.06f, -0.5f ),
					new Vector3( -0.5f, 0.06f, 0.5f ),
					new Vector3( 0.5f, 0.06f, -0.5f ),
					new Vector3( 0.5f, 0.06f, 0.5f ),
					  };

					mesh.triangles = new int[] {
					0, 1, 2, 1, 3, 2
					};

					mesh.uv = new Vector2[] {
					new Vector2( 0.0f, 1.0f ),
					  new Vector2( 0.0f, 0.0f ),
					 new Vector2( 1.0f, 1.0f ),
					new Vector2( 1.0f, 0.0f ),
					};

					mesh.RecalculateNormals();

					// attack mesh to filter
					filter.mesh = mesh;
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}


	[MenuItem("Project Soap/回復石鹸出現場所の編集用メッシュを削除")]
	public static void DeleteQuadForRecoverySoapAppearancePoint()
	{
		// シーン上に存在するオブジェクトならば処理.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			if (obj.activeInHierarchy && obj.tag == "RecoverySoapAppearancePoint")
			{
				MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

				if (renderer != null)
				{
					DestroyImmediate(renderer);
				}

				MeshFilter filter = obj.GetComponent<MeshFilter>();

				if (filter != null)
				{
					DestroyImmediate(filter);
				}

				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/車用のミニマップアイコン設置")]
	public static void AddCarMiniMapIcon()
	{ // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj != null && obj.activeInHierarchy && (obj.tag == "StaticCar" || obj.tag == "MoveCar"))
			{
				// オブジェクト生成

				if (obj.transform.FindChild("CarMiniMapIcon") == null)
				{
					GameObject newObject = new GameObject("CarMiniMapIcon");
					newObject.transform.rotation = obj.transform.rotation;
					newObject.transform.parent = obj.transform; // 親子付
					newObject.transform.position = new Vector3(obj.transform.position.x, 0, obj.transform.position.z);
					newObject.transform.Translate(new Vector3(0, -490, 0));
					MeshRenderer renderer = newObject.AddComponent<MeshRenderer>();
					renderer = newObject.GetComponent<MeshRenderer>();
					if (renderer)
					{
						renderer.sharedMaterial = Resources.Load("Materials/MiniMapCarIconMaterial") as Material;
					}

					MeshFilter filter = newObject.AddComponent<MeshFilter>();
					filter = newObject.GetComponent<MeshFilter>();
					if (filter)
					{
						// create -Z direction Billboard
						Mesh mesh = new Mesh();
						mesh.name = "Plane(-Z)";

						mesh.vertices = new Vector3[] {
					new Vector3( -0.5f, 0.0f, -0.5f ),
					new Vector3( -0.5f, 0.0f, 0.5f ),
					new Vector3( 0.5f, 0.0f, -0.5f ),
					new Vector3( 0.5f, 0.0f, 0.5f ),
					  };
						{
							newObject.transform.localScale =
								new Vector3
								(
									 newObject.transform.localScale.x * obj.transform.localScale.x,
									 newObject.transform.localScale.y * obj.transform.localScale.y,
									 newObject.transform.localScale.z * obj.transform.localScale.z);



							if (obj.transform.parent)
							{
								Transform pearent = obj.transform.parent;
								newObject.transform.localScale =
									new Vector3
									(
										pearent.lossyScale.x / pearent.localScale.x *2,
										pearent.lossyScale.y / pearent.localScale.y *2,
										pearent.lossyScale.z / pearent.localScale.z *2);

							}

							mesh.triangles = new int[] {
						0, 1, 2, 1, 3, 2
						};

							mesh.uv = new Vector2[] {
							new Vector2( 1.0f, 1.0f ),
							new Vector2( 0.0f, 1.0f ),
							new Vector2( 1.0f, 0.0f ),
							new Vector2( 0.0f, 0.0f ),
						};

							mesh.RecalculateNormals();

							// attack mesh to filter
							filter.mesh = mesh;

						}
					}
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}

	[MenuItem("Project Soap/ミニマップ用車アイコン削除")]
	public static void DeleteMiniMapQuadForCar()
	{

		// typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
		foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
		{
			// シーン上に存在するオブジェクトならば処理.
			if (obj != null && obj.activeInHierarchy && (obj.tag == "StaticCar" || obj.tag == "MoveCar"))
			{
				// オブジェクト検索

				if (obj.transform.FindChild("CarMiniMapIcon") != null)
				{
					GameObject quad = obj.transform.FindChild("CarMiniMapIcon").gameObject;
					DestroyImmediate(quad);
				}
				// GameObjectの名前を表示.
				Debug.Log(obj.name);
			}
		}
	}

	static bool m_isDrawGizmo;
	[MenuItem("Project Soap/汚れギズモON")]
	public static void DrawDirtyGizmo()
	{
		m_isDrawGizmo = true;
	}
	[MenuItem("Project Soap/汚れギズモOFF")]
	public static void UnDrawDirtyGizmo()
	{
		m_isDrawGizmo = false;
	}


	[DrawGizmo(GizmoType.NonSelected | GizmoType.Active)]
	static void DrawCreaterGizmos(DirtyCreater example, GizmoType gizmoType)
	{

		if (m_isDrawGizmo)
		{

			var transform = example.transform;
			Gizmos.color = new Color32(145, 244, 139, 210);

			//GizmoType.Active の時は赤色にする
			if ((gizmoType & GizmoType.Active) == GizmoType.Active)
				Gizmos.color = Color.red;

			Gizmos.DrawWireCube(transform.position, transform.lossyScale);

			for (int i = 0; i < example.appearancePoints.Length; i++)
			{

				var appearanceTransform = example.appearancePoints[i].transform;
				DirtyApparancePosition appearancePosition = example.appearancePoints[i].GetComponent<DirtyApparancePosition>();
				Gizmos.color = new Color32(145, 244, 139, 210);

				//GizmoType.Active の時は赤色にする
				if ((gizmoType & GizmoType.Active) == GizmoType.Active)
					Gizmos.color = Color.red;

				Gizmos.DrawWireCube(appearanceTransform.position, appearanceTransform.lossyScale * 0.5f);

				switch (i)
				{
					case 0:
						Gizmos.color = Color.cyan;
						break;
					case 1:
						Gizmos.color = Color.magenta;
						break;
					case 2:
						Gizmos.color = Color.yellow;
						break;
					case 3:
						Gizmos.color = Color.blue;
						break;
				}


				for (int j = 0; j < appearancePosition.CreateNumber; j++)
				{
					Vector3 pos = new Vector3();
					switch (appearancePosition.m_type)
					{
						case DirtyApparancePosition.AppearanceType.CIRCLE:
							pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * i * 360.0f / (float)appearancePosition.CreateNumber), Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)appearancePosition.CreateNumber), 0);
							pos.x *= appearancePosition.createRange.x;
							pos.y *= appearancePosition.createRange.y;
							pos = appearancePosition.transform.rotation * pos;
							break;
						case DirtyApparancePosition.AppearanceType.LINE:
							pos = new Vector3(0, 0, 0);
							pos.y = appearancePosition.createRange.y * j;
							pos = appearancePosition.transform.rotation * pos;
							break;
						default:
							break;
					}

					Gizmos.DrawSphere(appearancePosition.transform.position + pos, 0.5f * appearancePosition.dirtyObject.transform.localScale.x);

				}
			}
		}

	}

}