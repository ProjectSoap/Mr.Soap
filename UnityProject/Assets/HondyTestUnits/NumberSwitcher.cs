using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NumberSwitcher : MonoBehaviour {
    [SerializeField]
    Sprite[] m_sprite = new Sprite[10];

    //数字の設定
    public void SetNumber(int num)
    {
        Image sr = gameObject.GetComponent<Image>();
        if (0 <= num && num < 10)
        {

            sr.sprite = m_sprite[num];
        }
    }
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
