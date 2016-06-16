using UnityEngine;
using System.Collections;

public class CircleShadow : MonoBehaviour {

    GameObject m_playerCharacter;
    PlayerCharacterController m_playerCharacterController;
    Vector3 m_defaultScale;

	// Use this for initialization
	void Start ()
    {
        transform.rotation = Quaternion.Euler(90, 0, 0);

        m_playerCharacter           = GameObject.Find("PlayerCharacter");
        m_playerCharacterController = m_playerCharacter.GetComponent<PlayerCharacterController>();

        m_defaultScale = transform.localScale;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (m_playerCharacterController.state == PlayerCharacterController.DriveState.Start)
            transform.localPosition = new Vector3(0, 0, 0);
        else
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

        float height = m_playerCharacter.transform.position.y;

        transform.localScale = m_defaultScale * (1 + (1 - Mathf.Clamp((1.0f / height), 0.25f, 1.0f)));
	}
}