using UnityEngine;
using System.Collections;

public class CircleShadow : MonoBehaviour {

    GameObject                  m_playerCharacter;
    PlayerCharacterController   m_playerCharacterController;
    Vector3                     m_defaultScale;
    int                         m_layerMask;

	// Use this for initialization
	void Start ()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);

        m_playerCharacter           = GameObject.Find("PlayerCharacter");
        m_playerCharacterController = m_playerCharacter.GetComponent<PlayerCharacterController>();

        m_defaultScale = transform.localScale;

        m_layerMask = 
            (1 << LayerMask.NameToLayer("Terrain")) +
            (1 << LayerMask.NameToLayer("Building")) +
            (1 << LayerMask.NameToLayer("Car"));
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);
        transform.localPosition = Vector3.zero;

        RaycastHit[] raycastHits = Physics.RaycastAll(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
            transform.position,
            Vector3.down,
            30.0f,
            m_layerMask);

        if (raycastHits.Length > 0)
        {
            transform.position = new Vector3(transform.position.x, raycastHits[0].point.y + 0.1f, transform.position.z);
        }

        float height = m_playerCharacter.transform.position.y;

        transform.localScale = m_defaultScale * (1 + (1 - Mathf.Clamp((1.0f / height), 0.25f, 1.0f)));  // ここ計算適当
    }
}