using UnityEngine;
using System.Collections;

public class BarricadeObject : MonoBehaviour {

	public uint m_lockArea = 2;
	public bool m_isUnlock = false;
    int playerLayer;
    SekkenSayUI sayUI;
	// Use this for initialization
	void Start () 
    {
        GameObject obj = GameObject.Find("SekkenSayUI");
        if (obj)
        {
            sayUI = obj.GetComponent<SekkenSayUI>();
        }
        playerLayer = LayerMask.NameToLayer("Player");
#if DEBUG
		if (m_lockArea < 2 || 4 < m_lockArea)
		{
			Debug.Log("Out-of-range values" + this);
		}
#endif 
	}
	
	// Update is called once per frame
	void Update () {
		if (m_isUnlock)
		{
			Destroy(gameObject);
		}
	}
    void OnCollisionEnter(Collision col) 
    {
        if (col.gameObject.layer == playerLayer) 
        {
            if (sayUI)
            {
               sayUI.DrawSayTexture(SekkenSayUI.ESayTexName.BARRICADE);
            }
        }
    }
}
