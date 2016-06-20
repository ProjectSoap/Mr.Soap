using UnityEngine;
using System.Collections;

public class BarricadeObject : MonoBehaviour {

	public uint m_lockArea = 2;
	public bool m_isUnlock = false;
	// Use this for initialization
	void Start () {
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
}
