using UnityEngine;
using System.Collections;
using UnityEngine.UI;
public class HalLogoSceneManager : MonoBehaviour {

    public UnityEngine.UI.Image m_logoImage;
    public UnityEngine.UI.Image m_logoBackImage;
    public float m_logoTime;
    // Use this for initialization
    void Start ()
    {

        m_logoImage = GameObject.Find("logoImage").GetComponent<Image>();
        m_logoBackImage = GameObject.Find("Image").GetComponent<Image>();
        m_logoImage.color = new Color(1,1,0,0);
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_logoTime += Time.deltaTime;
        if (m_logoTime <= 3.0f)
        {
            m_logoImage.color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(0, 1, m_logoTime / 3.0f));
        }
        else if (3.5f < m_logoTime && m_logoTime <= 5.5f)
        {
            m_logoImage.color = new UnityEngine.Color(1.0f, 1.0f, 1.0f, Mathf.Lerp(1, 0, (m_logoTime - 3.5f) / 2.0f));
        }
        else if (5.5f < m_logoTime)
        {
            m_logoBackImage.color = new UnityEngine.Color(0, 0, 0, 0);
            m_logoImage.color = new UnityEngine.Color(0, 0, 0, 0);
            Application.LoadLevel("sekTitle");
        }

    }
}
