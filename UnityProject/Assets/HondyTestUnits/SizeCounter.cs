using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SizeCounter : MonoBehaviour {

    [SerializeField]
    int count;
    public int Count
    {
        get { return count; }
        set
        {
            if (value > 101)
            {
                value = 100;
            }
            else if (value < 0)
            {
                value = 0;
            }
            count = value;
        }
    }
    [SerializeField]
    int centerPosition;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        GameObject player = GameObject.Find("PlayerCharacter");
        count = (int)player.GetComponent<PlayerCharacterController>().size;
        int temp = count;
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
                rect.localPosition = new Vector3(centerPosition + rect.sizeDelta.x * 0.5f, rect.localPosition.y, rect.localPosition.z);
            }
            else if (count < 10000)
            {
                rect.localPosition = new Vector3(centerPosition + rect.sizeDelta.x * 0.75f, rect.localPosition.y, rect.localPosition.z);
            }
            obj.GetComponent<NumberSwitcher>().SetNumber(temp % 10);
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
                rect.localPosition = new Vector3(centerPosition - rect.sizeDelta.x * 0.25f, rect.localPosition.y, rect.localPosition.z);
            }
            else if (count < 1000)
            {
                rect.localPosition = new Vector3(centerPosition, rect.localPosition.y, rect.localPosition.z);
            }
            else if (count < 10000)
            {
                rect.localPosition = new Vector3(centerPosition + rect.sizeDelta.x * 0.25f, rect.localPosition.y, rect.localPosition.z);
            }


            temp /= 10;
            obj.GetComponent<NumberSwitcher>().SetNumber(temp % 10);
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
            obj.GetComponent<NumberSwitcher>().SetNumber(temp % 10);
        }
    }
}
