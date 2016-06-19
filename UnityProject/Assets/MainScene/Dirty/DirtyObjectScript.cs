using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DirtyObjectScript : MonoBehaviour
{
	[SerializeField]
	GameObject m_effect0;
	[SerializeField]
	GameObject m_effect1;
	[SerializeField]
    Material[] dirtyMaterials = new Material[8];
    DirtyApparancePosition myPoint;
    [SerializeField]
    GameObject dirtyIcon;
    bool isDestory = false;
	PlayerCharacterController m_player;
	public PlayerCharacterController Player
	{
		get { return m_player; }
		set { m_player = value; }
	}
	public DirtyApparancePosition MyPoint
    {
        get { return myPoint; }
        set { myPoint = value; }
    }

	enum State
	{
		ALIVE,
		WASH,
		DEAD

	}

	State m_state = State.ALIVE;

	GameObject m_shibukiEffect;
    // Use this for initialization
    void Start()
    {
        GameObject obj= Instantiate(dirtyIcon, new Vector3(transform.position.x, dirtyIcon.transform.position.y, transform.position.z),dirtyIcon.transform.rotation) as GameObject;
        obj.transform.parent = transform;

	}


    public void SwitchMaterial(int num)
    {
        if (0 <= num && num < dirtyMaterials.Length)
        {
            GetComponent<MeshRenderer>().material = dirtyMaterials[num];
        }
    }

    // Update is called once per frame
    void Update()
    {
		switch (m_state)
		{
			case State.ALIVE:
				if (isDestory == true)
				{
					m_state = State.WASH;
				}
				break;
			case State.WASH:

				GetComponent<MeshRenderer>().material.color = GetComponent<MeshRenderer>().material.color - new Color(0, 0, 0, 2.0f * Time.deltaTime);
				if (GetComponent<MeshRenderer>().material.color.a  <=  0)
				{
					m_state = State.DEAD;
				}
				break;
			case State.DEAD:
				break;
			default:
				break;

		}
		if (m_state == State.DEAD)
		{
			Destroy(gameObject);
		}

	}
    void OnTriggerEnter(Collider collision)
    {
        if (
			(collision.gameObject.tag == "Bubble" ||
			(collision.gameObject.layer == LayerMask.NameToLayer("Player")) ||
			(collision.gameObject.layer == LayerMask.NameToLayer("Bubbble"))) && 
			m_state == State.ALIVE
			)
        {
			m_shibukiEffect = Instantiate(m_effect1, this.transform.position, this.transform.rotation * Quaternion.AngleAxis(-90,new Vector3(1,0,0)))as GameObject;
			m_shibukiEffect.transform.parent = transform.parent;
			if (isDestory == false)
            {
                myPoint.NoticeDestroy();
				isDestory = true;
				DirtyWashEffect effect = (Instantiate(m_effect0, this.transform.position, this.transform.rotation)as GameObject).GetComponent<DirtyWashEffect>();
				effect.m_goalObject = Player.gameObject;
				effect.transform.parent = transform.parent;
				BGMManager.Instance.PlaySE("Wash_Out");
            }
        }
    }
}
