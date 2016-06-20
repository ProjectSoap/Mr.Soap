using UnityEngine;
using System.Collections;

public class RecoverySoapObject : MonoBehaviour {

    RecoverySoapCreater parent;

    public RecoverySoapCreater Parent
    {
        get { return parent; }
        set { parent = value; }
    }

    [SerializeField]
    float lifeTime;

    [SerializeField, Header("回転角")]
    float m_rotation = 60.0f;

    [SerializeField]
    float m_moveHeightMax = 1.0f;

    [SerializeField]
    float m_moveHeightMin = 0.5f;
    [SerializeField,Header("上下の速さ")]
    float m_moveSpeed = 0.2f;

    bool isUpMove = true;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        lifeTime -= Time.deltaTime;
        if (lifeTime <= 0)
        {
            parent.IsHaveRevoverySoap = false;
            Destroy(gameObject);
        }
        transform.localRotation *= Quaternion.AngleAxis(60 * Time.deltaTime, new Vector3(0,0,1));
        if (isUpMove)
        {
            transform.position += new Vector3(0, m_moveSpeed, 0);
        }
        else
        {
            transform.position -= new Vector3(0, m_moveSpeed, 0);

        }
        if (m_moveHeightMax < transform.position.y)
        {
            isUpMove = false;
        }

        else if (m_moveHeightMin > transform.position.y)
        {
            isUpMove = true;
        }
    }


    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Parent.IsHaveRevoverySoap = false;
            PlayerCharacterController player = collisionObject.gameObject.GetComponent<PlayerCharacterController>();
			player.Heal();
            Destroy(gameObject);
        }
    }}
