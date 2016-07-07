using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RecoverySoapPointMarker : MonoBehaviour {

	public enum EState
	{
		NONE_APPEARANCE,	// 非出現
		APPEARANCE,	// 出現時点滅
		HIDE_BUILDING,  // 建物に隠れた 表示 
		VISIBLE_MAIN_CAMERA,	// メインカメラで見える
		GET_RECOVERY_SOAP		// 回復アイテム取得
	}
	// 発光状態
	public enum EEmissionState
	{
		ATTENUATION,    // 減衰
		AMPLIFICATION	// 増幅
	}

	EState m_mainState;         // 状態
	EEmissionState m_emissionState;
	public RecoverySoapPointMarker.EState State
	{
		get { return m_mainState; }
		set { m_mainState = value; }
	}
	static Camera m_mainCamera;	// メインカメラ
	[SerializeField]
	GameObject m_markPoint;		// マーカー対象オブジェクト
	public UnityEngine.GameObject MarkPoint
	{
		get { return m_markPoint; }
		set { m_markPoint = value; }
	}
	Image m_myImage;	// スプライト

	[SerializeField]
	Vector3 m_offset;	// マーカー表示のオフセット
	int m_layerMask;	// レイヤーマスク


	[SerializeField]
	Color m_emissionColor;  // 発光色
	[SerializeField]
	uint m_numberOfEmission;    // 発光回数
	[SerializeField]
	float m_flashingTime = 1.0f;    // 点滅時間
	float m_oneOperationTime;	//発光の減衰増幅1回の動作時間
	float m_controlTime;	
	uint m_countOfEmission;    // 現在の発光回数
	float m_distance;	// 対象との位置

	GameObject m_player;	// プレイヤー
	public UnityEngine.GameObject Player
	{
		get { return m_player; }
		set { m_player = value; }
	}
	bool m_isEnableUI;
	public bool EnableUI
	{
		get
		{
			return m_isEnableUI;
		}
		set
		{
			m_isEnableUI = value;
			m_distanceText.enabled = m_isEnableUI;
			m_myImage.enabled = m_isEnableUI;
		}
	}

	bool m_isDrawDistanceText = false;  // 距離表示のフラグ
	public bool DrawDistanceText
	{
		get
		{
			return m_isDrawDistanceText;
		}
		set
		{
			m_isDrawDistanceText = value;
			if (m_distanceTextUI)
			{
				m_distanceTextUI.enabled = m_isDrawDistanceText;
			}
		}
	}
	DistanceTextUI m_distanceTextUI;
	Text m_distanceText;

	GameObject m_distanceTargetA;
	public UnityEngine.GameObject DistanceTargetA
	{
		get { return m_distanceTargetA; }
		set
		{
			m_distanceTargetA = value;
			m_distanceTextUI.TargetA = m_distanceTargetA;
		}
	}
	GameObject m_distanceTargetB;
	public UnityEngine.GameObject DistanceTargetB
	{
		get { return m_distanceTargetB; }
		set
		{
			m_distanceTargetB = value;
			m_distanceTextUI.TargetB = m_distanceTargetB;
				
		}
	}

	bool m_isAppearance = false;    // 出現中フラグ
	public bool AppearanceRecaverySoap
	{
		get { return m_isAppearance; }
		set { m_isAppearance = value; }
	}
	bool m_isGetRecoverySoap = false;    // 出現中フラグ
	public bool GetRecoverySoap
	{
		get { return m_isGetRecoverySoap; }
		set { m_isGetRecoverySoap = value; }
	}
	
	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	void Start () {
		m_mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
		m_myImage = GetComponent<Image>();

		m_layerMask =
			(1 << LayerMask.NameToLayer("Terrain")) +
			(1 << LayerMask.NameToLayer("Building")) +
			(1 << LayerMask.NameToLayer("Car"));
		m_distanceTextUI = transform.FindChild("DistanceText").GetComponent<DistanceTextUI>();
		m_distanceText = m_distanceTextUI.gameObject.GetComponent<Text>();
		m_oneOperationTime = m_flashingTime / m_numberOfEmission;
	}

	// Update is called once per frame
	void Update()
	{
		UpdateState();
		StateProcess();
	}

	// ワールド座標をスクリーン上の座標に変換
	Vector3 TransScreenPosition(Vector3 worldPosition)
	{
		Vector3 rate, tempPosition = new Vector3(0,0,0);
		Vector3 direction;
		if (m_markPoint)
		{
			rate = m_mainCamera.WorldToViewportPoint(worldPosition);
			direction = m_markPoint.transform.position - m_mainCamera.transform.position;

			if (rate.z < 0)
			{
				//一旦0が中心にする
				rate -= new Vector3(0, 0.5f, 0);
				// 反転
				rate = new Vector3(rate.x, -rate.y, rate.z);
				// 0.5中心に戻す
				rate += new Vector3(0, 0.5f, 0);
				//画面外なら画面縁に出す
				if (rate.y <= -0)
				{
					rate.y = -0f;
				}
				else if (rate.y > 1)
				{
					rate.y = 1f;
				}
				// 後ろでXY共に 0<1 内で画面に出るパターンがあるので強制的にYを0または1にする
				if (0 <= rate.x && rate.x <= 1)
				{
					if (rate.y <= 0.5)
					{
						rate.y = 0;
					}
					else
					{
						rate.y = 1;
					}
				}
			}

			else
			{
				if (rate.y < 0)
				{
					rate.y = 0;
				}
				else if (rate.y > 1)
				{
					rate.y = 1;
				}
			}

			// Xがビューポート内より外
			if (rate.z < 0)
			{
				//一旦0が中心にする
				rate -= new Vector3(0.5f, 0, 0);

				// 反転
				rate = new Vector3(-rate.x, rate.y, rate.z);
				if (rate.x < -0.5f)
				{
					rate.x = -0.5f;
				}
				else if (rate.x > 0.5f)
				{
					rate.x = 0.5f;
				}
				rate += new Vector3(0.5f, 0, 0);
			}
			else
			{

				if (rate.x < 0)
				{
					rate.x = 0;
				}
				else if (rate.x > 1)
				{
					rate.x = 1;
				}
			}

			if (0 < rate.x && rate.x < 1 && 0 < rate.y && rate.y < 1)
			{

				RaycastHit[] raycastHits = Physics.RaycastAll(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
					m_mainCamera.transform.position,
					direction,
					m_distance,
					m_layerMask);
				if (raycastHits.Length < 1)
				{
					EnableUI = false;
				}
				else
				{
					EnableUI = true;
				}
			}
			else if (EnableUI == false)
			{
				EnableUI = true;
			}

			tempPosition = new Vector3(rate.x * Screen.width, rate.y * Screen.height, 0);
			if (tempPosition.x < m_myImage.rectTransform.sizeDelta.x * 0.5f)
			{
				tempPosition = new Vector3(m_myImage.rectTransform.sizeDelta.x * 0.5f + m_offset.x, tempPosition.y, 0);
			}
			if (tempPosition.y < m_myImage.rectTransform.sizeDelta.y * 0.5f)
			{
				tempPosition = new Vector3(tempPosition.x, m_myImage.rectTransform.sizeDelta.y * 0.5f + m_offset.y, 0);
			}
			if (tempPosition.x > Screen.width - m_myImage.rectTransform.sizeDelta.x * 0.5f)
			{
				tempPosition = new Vector3(Screen.width - m_myImage.rectTransform.sizeDelta.x * 0.5f - m_offset.x, tempPosition.y, 0);
			}
			if (tempPosition.y > Screen.height - m_myImage.rectTransform.sizeDelta.y * 0.5f)
			{
				tempPosition = new Vector3(tempPosition.x, Screen.height - m_myImage.rectTransform.sizeDelta.y * 0.5f - m_offset.y, 0);
			}

		}
		return tempPosition;
	}
	// 画面外チェック
	bool IsOutScreen(Vector3 worldPosition)
	{
		Vector3 viewportRate = m_mainCamera.WorldToViewportPoint(worldPosition);
		if (0 < viewportRate.x && viewportRate.x <= 1)
		{
			if (0 < viewportRate.y && viewportRate.y <= 1)
			{
				if (viewportRate.z > 0)
				{
					return false;
				}
			}
		}
		return true;
	}

	bool IsHideObstacle(Vector3 markWorldPosition,Vector3 cameraWorldPosition)
	{

		Vector3 direction = markWorldPosition - cameraWorldPosition;
		RaycastHit[] raycastHits = Physics.RaycastAll(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
			m_mainCamera.transform.position,
			direction,
			m_distance,
			m_layerMask);
		if (raycastHits.Length < 1)
		{
			// 一個でもレイが衝突したら隠れてる
			return true;
		}
		return false;
	}

	void UpdateState()
	{
		switch (m_mainState)
		{
			case EState.NONE_APPEARANCE:
				if (AppearanceRecaverySoap)
				{
					ExitStateProcess();
					m_mainState = EState.APPEARANCE;
					EnterStateProcess();
				}
				break;
			case EState.APPEARANCE:
				if (m_numberOfEmission <= m_countOfEmission)
				{
					if (IsOutScreen(m_markPoint.transform.position) || IsHideObstacle(m_markPoint.transform.position, m_mainCamera.transform.position))
					{
						ExitStateProcess();
						m_mainState = EState.HIDE_BUILDING;
						EnterStateProcess();
					}
					else
					{
						ExitStateProcess();
						m_mainState = EState.VISIBLE_MAIN_CAMERA;   // メインカメラで見えるはず
						EnterStateProcess();
					}
				}
				// 取得した
				if (GetRecoverySoap)
				{
					ExitStateProcess();
					m_mainState = EState.GET_RECOVERY_SOAP;
					EnterStateProcess();
				}
				break;
			case EState.HIDE_BUILDING:
				if (IsOutScreen(m_markPoint.transform.position) || IsHideObstacle(m_markPoint.transform.position, m_mainCamera.transform.position))
				{
					ExitStateProcess();
					m_mainState = EState.HIDE_BUILDING;
					EnterStateProcess();
				}
				else
				{
					ExitStateProcess();
					m_mainState = EState.VISIBLE_MAIN_CAMERA;   // メインカメラで見えるはず
					EnterStateProcess();
				}

				// 取得した
				if (GetRecoverySoap)
				{
					ExitStateProcess();
					m_mainState = EState.GET_RECOVERY_SOAP;
					EnterStateProcess();
				}
				break;
			case EState.VISIBLE_MAIN_CAMERA:
				if (IsOutScreen(m_markPoint.transform.position) || IsHideObstacle(m_markPoint.transform.position, m_mainCamera.transform.position))
				{
					ExitStateProcess();
					m_mainState = EState.HIDE_BUILDING;
					EnterStateProcess();
				}
				else
				{
					ExitStateProcess();
					m_mainState = EState.VISIBLE_MAIN_CAMERA;   // メインカメラで見えるはず
					EnterStateProcess();
				}

				// 取得した
				if (GetRecoverySoap)
				{
					ExitStateProcess();
					m_mainState = EState.GET_RECOVERY_SOAP;
					EnterStateProcess();
				}
				break;
			case EState.GET_RECOVERY_SOAP:
				ExitStateProcess();
				m_mainState = EState.NONE_APPEARANCE;
				EnterStateProcess();
				break;
			default:
				break;

		}
	}

	void EnterStateProcess()
	{
		switch (m_mainState)
		{
			case EState.NONE_APPEARANCE:
				EnableUI = false;
				break;
			case EState.APPEARANCE:
				EnableUI = true;
				break;
			case EState.HIDE_BUILDING:
				EnableUI = true;
				break;
			case EState.VISIBLE_MAIN_CAMERA:
				EnableUI = false;
				break;
			case EState.GET_RECOVERY_SOAP:
				EnableUI = false;
				break;
			default:
				break;
		}
	}

	void ExitStateProcess()
	{
		switch (m_mainState)
		{
			case EState.NONE_APPEARANCE:
				AppearanceRecaverySoap = false;
				break;
			case EState.APPEARANCE:
				m_myImage.color = new Color(1, 1, 1, 1);
				m_countOfEmission = 0;
				break;
			case EState.HIDE_BUILDING:
				break;
			case EState.VISIBLE_MAIN_CAMERA:
				break;
			case EState.GET_RECOVERY_SOAP:
				GetRecoverySoap = false;
				break;
			default:
				break;
		}
	}

	void StateProcess()
	{
		switch (m_mainState)
		{
			case EState.NONE_APPEARANCE:
				break;
			case EState.APPEARANCE:
				if (m_markPoint)
				{
					m_distance = Vector3.Distance(m_mainCamera.transform.position, m_markPoint.transform.position);

					m_myImage.rectTransform.position = TransScreenPosition(m_markPoint.transform.position);
					m_controlTime += Time.deltaTime;
					// 発光処理
					switch (m_emissionState)
					{
						case EEmissionState.ATTENUATION:
							m_myImage.color = Color.Lerp(new Color(1, 1, 1, 1), m_emissionColor, m_controlTime/m_oneOperationTime);
							if (m_oneOperationTime < m_controlTime)
							{
								m_controlTime = 0;
								m_emissionState = EEmissionState.AMPLIFICATION;
							}
							break;
						case EEmissionState.AMPLIFICATION:
							m_myImage.color = Color.Lerp(m_emissionColor, new Color(1, 1, 1, 1), m_controlTime / m_oneOperationTime);
							if (m_oneOperationTime < m_controlTime)
							{
								m_controlTime = 0;

								m_emissionState = EEmissionState.ATTENUATION;
								m_countOfEmission++;

							}
							break;
						default:
							break;

					}
				}

				break;
			case EState.HIDE_BUILDING:
				if (m_markPoint)
				{
					m_distance = Vector3.Distance(m_mainCamera.transform.position, m_markPoint.transform.position);

					m_myImage.rectTransform.position = TransScreenPosition(m_markPoint.transform.position);
				}
				break;
			case EState.VISIBLE_MAIN_CAMERA:
				break;
			case EState.GET_RECOVERY_SOAP:
				break;
			default:
				break;
		}
	}
}
