using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DirtyWashUI
	:
	MonoBehaviour
{
	DirtyWashUIImage[] m_imageArray;
	Camera m_mainCamera;
	int m_layerMask;
	[SerializeField]
	GameObject m_imageSample;
	Image m_image;
	// Use this for initialization
	void Start()
	{
		m_mainCamera = GameObject.Find("MainCamera").GetComponent<Camera>();
		if (m_imageSample)
		{
			m_image = m_imageSample.GetComponent<Image>();
		}
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void NorticeDirtyWash(DirtyObjectScript dirty)
	{
		if (IsOutScreen(dirty.transform.position))
		{
			Vector3 screenPosition = TransVewportPosition(dirty.transform.position);
			GameObject newImage = Instantiate(m_imageSample, screenPosition, Quaternion.identity) as GameObject;
			newImage.transform.parent = this.transform;

		}
	}

	Vector3 TransVewportPosition(Vector3 worldPosition)
	{
		Vector3 rate, tempPosition = new Vector3(0, 0, 0);
		Vector3 direction;

		rate = m_mainCamera.WorldToViewportPoint(worldPosition);
		direction = worldPosition - m_mainCamera.transform.position;

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


		tempPosition = new Vector3(rate.x * Screen.width, rate.y * Screen.height, 0);
		
		if (tempPosition.x < m_image.rectTransform.sizeDelta.x * 0.5f)
		{
			tempPosition = new Vector3(m_image.rectTransform.sizeDelta.x * 0.5f, tempPosition.y, 0);
		}
		if (tempPosition.y < m_image.rectTransform.sizeDelta.y * 0.5f)
		{
			tempPosition = new Vector3(tempPosition.x, m_image.rectTransform.sizeDelta.y * 0.5f , 0);
		}
		if (tempPosition.x > Screen.width - m_image.rectTransform.sizeDelta.x * 0.5f)
		{
			tempPosition = new Vector3(Screen.width - m_image.rectTransform.sizeDelta.x * 0.5f , tempPosition.y, 0);
		}
		if (tempPosition.y > Screen.height - m_image.rectTransform.sizeDelta.y * 0.5f)
		{
			tempPosition = new Vector3(tempPosition.x, Screen.height - m_image.rectTransform.sizeDelta.y * 0.5f, 0);
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
}