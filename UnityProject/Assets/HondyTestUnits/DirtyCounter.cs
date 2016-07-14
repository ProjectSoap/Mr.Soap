using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class DirtyCounter : MonoBehaviour {


	Queue<DirtyCounterImage> m_spriteBuffer =  new Queue <DirtyCounterImage>(); 


	[SerializeField]
	int count;
	[SerializeField]
	int centerPosition;
	public int Count
	{
		get { return count; }
		set { count = value; }
	}
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
	{
		int temp = Count;
		GameObject obj;
		obj = transform.FindChild("One").gameObject;
		if (obj)
		{
			RectTransform rect = obj.GetComponent<RectTransform>();
			// 中央揃いにする
			if (count < 10)
			{
				rect.localPosition = new Vector3(centerPosition, rect.localPosition.y, rect.localPosition.z);
			}
			else if (count < 100)
			{
				rect.localPosition = new Vector3(centerPosition + rect.sizeDelta.x * 0.25f, rect.localPosition.y, rect.localPosition.z);
			}
			else if (count < 1000)
			{
				rect.localPosition = new Vector3( centerPosition + rect.sizeDelta.x * 0.5f ,rect.localPosition.y, rect.localPosition.z);
			}
			else if (count < 10000)
			{
				rect.localPosition = new Vector3( centerPosition + rect.sizeDelta.x * 0.75f,rect.localPosition.y, rect.localPosition.z);
			}
			obj.GetComponent<SpriteSwitcher>().SetNumber(temp % 10);
		}
		obj = transform.FindChild("Ten").gameObject;
		if (obj)
		{
			if (count < 10)
			{
				obj.SetActive(false);
			}
			else
			{
				obj.SetActive(true);
			}


			RectTransform rect = obj.GetComponent<RectTransform>();
			if (count < 100)
			{
				rect.localPosition = new Vector3(centerPosition -rect.sizeDelta.x * 0.25f, rect.localPosition.y, rect.localPosition.z);
			}
			else if (count < 1000)
			{
				rect.localPosition = new Vector3( centerPosition , rect.localPosition.y,rect.localPosition.z);
			}
			else if (count < 10000)
			{
				rect.localPosition = new Vector3( centerPosition + rect.sizeDelta.x * 0.25f, rect.localPosition.y,rect.localPosition.z);
			}


			temp /= 10;
			obj.GetComponent<SpriteSwitcher>().SetNumber(temp % 10);
		}
		obj = transform.FindChild("Hundred").gameObject;
		if (obj)
		{
			if (count < 100)
			{
				obj.SetActive(false);
			}
			else
			{
				obj.SetActive(true);
			}


			RectTransform rect = obj.GetComponent<RectTransform>();
			// 中央揃いにする
			if (count < 1000)
			{
				rect.localPosition = new Vector3(centerPosition - rect.sizeDelta.x * 0.5f, rect.localPosition.y, rect.localPosition.z);
			}
			else if (count < 10000)
			{
				rect.localPosition = new Vector3(centerPosition - rect.sizeDelta.x * 0.25f, rect.localPosition.y, rect.localPosition.z);
			}

			temp /= 10;
			obj.GetComponent<SpriteSwitcher>().SetNumber(temp % 10);
		}
		obj = transform.FindChild("Thousand").gameObject;
		if (obj)
		{
			if (count < 1000)
			{
				obj.SetActive(false);
			}
			else
			{
				obj.SetActive(true);
			}


			RectTransform rect = obj.GetComponent<RectTransform>();
			// 中央揃いにする
			if (count < 10000)
			{
				rect.localPosition = new Vector3( centerPosition - rect.sizeDelta.x * 0.75f, rect.localPosition.y, rect.localPosition.z);
			}

			temp /= 10;
			obj.GetComponent<SpriteSwitcher>().SetNumber(temp % 10);
		}
	}
}
