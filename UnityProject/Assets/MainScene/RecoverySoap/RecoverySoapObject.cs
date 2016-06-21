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
    float m_lifeTime;
	float m_lifeTimeMax;

	[SerializeField,Header("点滅始める時間")]
	float m_flashStartTime = 10;
	[SerializeField, Header("最小点滅間隔")]
	float m_flashingIntervalMinTime = 0.1f;

	[SerializeField, Header("最大点滅間隔")]
	float m_flashingIntervalMaxTime = 0.5f;

	// フラッシュ中の計測時間
	float m_flashTime;
	[SerializeField, Header("回転角")]
    float m_rotation = 60.0f;

    [SerializeField]
    float m_moveHeightMax = 1.0f;

    [SerializeField]
    float m_moveHeightMin = 0.5f;
    [SerializeField,Header("上下の速さ")]
    float m_moveSpeed = 0.2f;

    bool isUpMove = true;

	enum FlashState
	{
		ON,
		OFF
	}

	FlashState m_flashState;
    // Use this for initialization
    void Start () {
		m_lifeTimeMax = m_lifeTime;
		m_flashTime = m_flashingIntervalMaxTime;
	}
	
	// Update is called once per frame
	void Update ()
    {
        m_lifeTime -= Time.deltaTime;
		FlashProcesss();

		if (m_lifeTime <= 0)
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

	void FlashProcesss()
	{
		if (m_lifeTime <= m_flashStartTime)
		{
			m_flashTime -= Time.deltaTime;
			switch (m_flashState)
			{
				case FlashState.ON:
					if (m_flashTime <= 0)
					{
						m_flashState = FlashState.OFF;
						GetComponent<MeshRenderer>().enabled = false;
						m_flashTime = Mathf.Lerp(m_flashingIntervalMinTime, m_flashingIntervalMaxTime, (m_lifeTime)/(m_lifeTimeMax-m_flashStartTime));
					}
						break;
				case FlashState.OFF:
					if (m_flashTime <= 0)
					{
						m_flashState = FlashState.ON;
						GetComponent<MeshRenderer>().enabled = true;
						m_flashTime = Mathf.Lerp(m_flashingIntervalMinTime, m_flashingIntervalMaxTime, (m_lifeTime) / (m_lifeTimeMax - m_flashStartTime));
					}
					break;
				default:
					break;

			}

		}
	}

    void OnTriggerEnter(Collider collisionObject)
    {
        if (collisionObject.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            Parent.IsHaveRevoverySoap = false;
            PlayerCharacterController player = collisionObject.gameObject.GetComponent<PlayerCharacterController>();
			player.Heal();
			ActionRecordManager.sActionRecord.ChachSopeCount++;
			
            Destroy(gameObject);
        }
    }}
