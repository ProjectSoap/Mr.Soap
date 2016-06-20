using UnityEngine;
using System.Collections;

public class RecoverySoapShadow : MonoBehaviour {

	GameObject m_parent;
	Vector3 m_defaultScale;

	// Use this for initialization
	void Start()
	{
		transform.rotation = Quaternion.Euler(90, 0, 0);

		m_parent = transform.parent.gameObject;

		m_defaultScale = transform.localScale;
	}

	// Update is called once per frame
	void Update()
	{
			transform.localPosition = new Vector3(0, 0, 0);

		int layerMask =
			~(1 << LayerMask.NameToLayer("Player")) +
			(1 << LayerMask.NameToLayer("Terrain")) +
			(1 << LayerMask.NameToLayer("Building")) +
			(1 << LayerMask.NameToLayer("Car"));

		RaycastHit[] raycastHits = Physics.RaycastAll(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
			transform.position,
			Vector3.down,
			100.0f,
			layerMask);

		transform.position = new Vector3(transform.position.x, raycastHits[0].point.y + 0.1f, transform.position.z);

		transform.rotation = Quaternion.Euler(90, 0, 0);

		float height = m_parent.transform.position.y;

		transform.localScale = m_defaultScale * (1 + (1 - Mathf.Clamp((1.0f / height*2), 0.25f, 1.2f)));
	}
}

