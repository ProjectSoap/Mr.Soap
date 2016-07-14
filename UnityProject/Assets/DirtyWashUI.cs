using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class DirtyWashUI
	:
	MonoBehaviour
{
	DirtyWashUIImage[] m_imageArray;
	Camera m_mainCamera;
	PlayerCharacterController m_player;
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

		m_player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
	}

	// Update is called once per frame
	void Update()
	{

	}

	public void NorticeDirtyWash(DirtyObjectScript dirty)
	{
		//if (IsOutScreen(dirty.transform.position))
		{
			Vector3 screenPosition = TransVewportPosition(dirty.transform.position);
			float x = Random.Range(0.0f, 2.0f);
			float y = Random.Range(0.0f, 2.0f);
			float z = Random.Range(0.0f, 2.0f);
			m_image.color = dirty.MyColor;
			GameObject newImage = Instantiate(m_imageSample, screenPosition, new Quaternion(0,0,z,1)) as GameObject;
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
		Vector3 tempRate = rate;
		if (0 < rate.x && rate.x < 1)
		{
			if (0 < rate.y && rate.y < 1)
			{
				// 方向ベクトルをとる
				Vector3 pos =  new Vector3(m_player.transform.position.x, 0, m_player.transform.position.z) - new Vector3(worldPosition.x, 0, worldPosition.z);
				pos = Vector3.Normalize(pos);
				// キャラ前方基準に設定
				Vector3 tempPos = m_player.transform.rotation * pos;
				// ビューポート用に変換
				tempRate = new Vector3((tempPos.x + 1.0f) / 2.0f, (tempPos.z + 1.0f)/2.0f);
				

				float deffZeroX = tempRate.x;
				float deffOneX = tempRate.x - 1.0f;
				float deffZeroY = tempRate.y;
				float deffOneY = tempRate.y - 1.0f;

				float[] deff = new float[4] { deffZeroX, deffOneX, deffZeroY, deffOneY};
				float minDeff = deff[0];

				for (int i = 0; i < 3; i++)
				{
					if (minDeff < deff[i + 1]) { }
				}

				// xyどちらが01に近いか比較
				// xが01に近い
				if (Mathf.Abs(deffZeroX) < Mathf.Abs(deffZeroY) || Mathf.Abs(deffOneX) < Mathf.Abs(deffOneY))
				{
					// rate.xは0のほうが近い
					if (tempRate.x < 0.5)
					{
						float ratio = 1 / tempRate.x;
						tempRate.x *= ratio;
						rate.x = tempRate.x - 1;
					}
					// rate.xは1のほうが近い
					else
					{

						float ratio = 1 / tempRate.x;
						tempRate.x *= ratio;
						rate.x = tempRate.x ;
					}
				}
				// yが01に近い
				else
				{
					// rate.xは0のほうが近い
					if (tempRate.y < 0.5)
					{

						float ratio = 1 / tempRate.y;
						tempRate.y *= ratio;
						rate.y = tempRate.y - 1;
					}
					// rate.xは1のほうが近い
					else
					{
						float ratio = 1 / tempRate.y;
						tempRate.y *= ratio;
						rate.y = tempRate.y;
					}
				}
			}
		}


		tempPosition = new Vector3(rate.x * Screen.width, rate.y * Screen.height, 0);
		
		if (tempPosition.x < m_image.rectTransform.sizeDelta.x * 0.5f)
		{
			tempPosition = new Vector3(0, tempPosition.y, 0);
		}
		if (tempPosition.y < m_image.rectTransform.sizeDelta.y * 0.5f)
		{
			tempPosition = new Vector3(tempPosition.x,0, 0);
		}
		if (tempPosition.x > Screen.width - m_image.rectTransform.sizeDelta.x * 0.5f)
		{
			tempPosition = new Vector3(Screen.width , tempPosition.y, 0);
		}
		if (tempPosition.y > Screen.height - m_image.rectTransform.sizeDelta.y * 0.5f)
		{
			tempPosition = new Vector3(tempPosition.x, Screen.height, 0);
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