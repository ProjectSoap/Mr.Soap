using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class SpriteSwitcher : MonoBehaviour {
    [SerializeField]
    Sprite[] m_sprite = new Sprite[10];
	public Sprite[] Sprite
	{
		get { return m_sprite; }
		set { m_sprite = value; }
	}
	[SerializeField]
	Image m_image;
	public UnityEngine.UI.Image Image
	{
		get { return m_image; }
		set { m_image = value; }
	}
	//数字の設定
	public void SetNumber(int num)
	{
		if (Image)
		{
			if (Sprite != null)
			{

				if (0 <= num && num < Sprite.Length)
				{

					Image.sprite = Sprite[num];
				}
			}

		}
	}
    
    // Use this for initialization
    void Start ()
	{
		if (Image == null)
		{
			Image = gameObject.GetComponent<Image>();
		}

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
