using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Marker : MonoBehaviour {

	static Camera m_mainCamera;
	[SerializeField]
	GameObject m_markPoint;
	public UnityEngine.GameObject MarkPoint
	{
		get { return m_markPoint; }
		set { m_markPoint = value; }
	}
	Image m_myImage;
	[SerializeField]
	Vector3 m_offset;
	int m_layerMask;

	bool m_isDrawDistanceText = false;
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

	GameObject m_player;
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
	// Use this for initialization
	void Start () {
		m_mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
		m_myImage = GetComponent<Image>();

		m_layerMask =
			(1 << LayerMask.NameToLayer("Terrain")) +
			(1 << LayerMask.NameToLayer("Building")) +
			(1 << LayerMask.NameToLayer("Car"));
		m_distanceTextUI = transform.FindChild("DistanceText").GetComponent<DistanceTextUI>();
		m_distanceText = m_distanceTextUI.gameObject.GetComponent<Text>();
	}

	// Update is called once per frame
	void Update()
	{
		if(m_markPoint)
		{
			Vector3 rate, tempPosition;
			Vector3 direction;
			float distance = Vector3.Distance(m_mainCamera.transform.position, m_markPoint.transform.position);
			rate = m_mainCamera.WorldToViewportPoint(m_markPoint.transform.position);
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
					distance,
					m_layerMask);
				if (raycastHits.Length < 1)
				{
					EnableUI= false;
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

			m_myImage.rectTransform.position = tempPosition;
		}
	}
}
