using UnityEngine;
using System.Collections;
using UniRx.Triggers;
using System.Collections.Generic;
using System.Linq;

public class SoapDrive : MonoBehaviour {

    enum ACCCEL_ENUM
    {
        NONE = 0,
        ACCCEL,
        BACK
    }
    ACCCEL_ENUM acccelState =  ACCCEL_ENUM.NONE;
    Vector3 m_direction;
    public UnityEngine.Vector3 Direction
    {
        get { return m_direction; }
        set { m_direction = value; }
    }
    float m_velocity;
    public float Velocity
    {
        get { return m_velocity; }
        set { m_velocity = value; }
    }
    //float m_maxVelocity;
    float m_minVelocity;

    Rigidbody m_rigidbody;

    [SerializeField]
    float m_jumpPower;

    [SerializeField]
    float m_maxVelocity;


    void Awake()
    {
    }


    // Use this for initialization
    void Start () {

        Direction.Set(0, 0, 1);
        //m_maxVelocity = 0.5f;
        //m_minVelocity = 0.0f;

        m_rigidbody = GetComponent<Rigidbody>();
    }

    void UpdateStatus()
    {
        switch (acccelState)
        {
            case ACCCEL_ENUM.NONE:

                if (Input.GetKey(KeyCode.W))
                {
                    acccelState = ACCCEL_ENUM.ACCCEL;
                }
                
                break;
            case ACCCEL_ENUM.ACCCEL:
                if (!Input.GetKey(KeyCode.W))
                {
                    acccelState = ACCCEL_ENUM.ACCCEL;
                }
                break;
            case ACCCEL_ENUM.BACK:
                if (!Input.GetKey(KeyCode.W))
                {
                    acccelState = ACCCEL_ENUM.ACCCEL;
                }
                break;
            default:
                break;

        }
    }
    
	// Update is called once per frame
	void Update ()
    {

        //UpdateStatus();
        //if (Input.GetKey(KeyCode.A))
        //{
        //    transform.Rotate(0, 0, Mathf.Deg2Rad * -45);
        //}

        //if (Input.GetKey(KeyCode.D))
        //{
        //    transform.Rotate(0, 0, Mathf.Deg2Rad * 45);
        //}

        //if (Input.GetKey(KeyCode.W))
        //{
        //    Velocity +=0.0007f;
        //    if (m_maxVelocity < Velocity)
        //    {
        //        Velocity = m_maxVelocity;
        //    }
        //}

        //if (Input.GetKey(KeyCode.S))
        //{
        //    Velocity -= 0.0007f;
        //    if (m_minVelocity > Velocity)
        //    {
        //        Velocity = m_minVelocity;
        //    }
        //}
        //Direction.Normalize();
        //var vec = new Vector3(0f, 0f, 1f);

        //Quaternion rot = new Quaternion();
        //rot.SetEulerAngles(Mathf.Deg2Rad * -90, Mathf.Deg2Rad * 180, 0);
        //var res1 = transform.rotation * rot* vec;
        //transform.position += res1 * Velocity ;
        //Direction = res1;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(.0f, Mathf.Deg2Rad * -45, .0f);
        }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(.0f, Mathf.Deg2Rad * 45, .0f);
        }


        if (Input.GetKey(KeyCode.UpArrow))
        {
            m_velocity += 0.05f;
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            m_velocity -= 0.05f;
        }
     //   m_velocity += Input.GetAxis("Vertical");

        if (m_maxVelocity < m_velocity)
        {
            m_velocity = m_maxVelocity;
        }
        else if (m_minVelocity > m_velocity)
        {
            m_velocity = m_minVelocity;
        }

        if(Mathf.Abs(m_velocity) > .0f )
            m_rigidbody.AddRelativeForce(Vector3.forward * m_velocity);

        // 速度制限を掛ける
        if(Mathf.Abs(m_rigidbody.velocity.z) > m_maxVelocity)
        {
            float vz = m_rigidbody.velocity.z;

            Mathf.Clamp(vz, m_maxVelocity, m_maxVelocity);
            m_rigidbody.velocity = new Vector3(m_rigidbody.velocity.x, m_rigidbody.velocity.y, vz);
        }

        if (Input.GetKeyDown((KeyCode.Space)))
            Jump();

        // tesuto
        //transform.FindChild("Line").FindChild("Cube").transform.LookAt(transform.position + m_rigidbody.velocity);
    }

    private void Jump()
    {
        bool isGrounded = Physics.Raycast(       // せっけんくんの真下にレイを飛ばして地形と接触チェック     @Todo　レイヤーマスクを行う
            transform.position,
            Vector3.down * 0.01f,       // 要調整
            1.0f);

        if(isGrounded)      // 地形と接触していた
        {
            //Debug.Log("Tyakuti now");

            m_rigidbody.AddForce(Vector3.up * m_jumpPower);       // 上に飛ばす
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.layer == LayerMask.NameToLayer("Billding"))
        {
            StartCoroutine(OnCollisionBillding());
        }
    }

    // こるーちんちん
    IEnumerator OnCollisionBillding()
    {
        // 壁ヒット時演出を書く
        yield return null;
    }
}
 