using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerCharacterController : MonoBehaviour
{
    public enum DriveState
    {
        Normal,
        Drift,
        Breake,
        BreakeAfter,
        Jump,
        JumpAfter,
        Damage,
        DamageAfter,
    }   

    [SerializeField, TooltipAttribute("最大速度")]
    float m_maxVelocity = 10.0f;

    [SerializeField, TooltipAttribute("加速度")]
    float m_acceleration = 0.05f;

    [SerializeField, Range(0.0f, 1.0f), TooltipAttribute("回転を元に戻す力 0に行くほど強い")]
    float m_rotationDecrementionRate = 0.9f;

    [SerializeField, TooltipAttribute("通常時回転力")]
    float m_rotationPowerNormal = 10.0f;

    [SerializeField, TooltipAttribute("ドリフト時回転力")]
    float m_rotationPowerDrift = 25.0f;

    [SerializeField, TooltipAttribute("通常時最大回転度")]
    float m_maxRotationNormal = 45.0f;

    [SerializeField, TooltipAttribute("ドリフト時最大回転度")]
    float m_maxRotationDrift = 60.0f;

    [SerializeField, TooltipAttribute("ドリフトが開始されるまでの入力時間")]
    float m_driftStartInputTime = 1.0f;

    [SerializeField, TooltipAttribute("ブレーキが開始されるまでの入力受付時間")]
    float m_breakeStartInputTime = 1.0f;

    [SerializeField, Range(0.0f, 1.0f), TooltipAttribute("ブレーキ力 0に行くほど強い")]
    float m_breakePower = 0.9f;

    [SerializeField, TooltipAttribute("ブレーキ後の硬直に遷移する速度の値")]
    float m_breakeVelocity = 0.5f;

    [SerializeField, TooltipAttribute("ブレーキ後の硬直時間")]
    float m_breakeAfterEndTime = 1.0f;

    [SerializeField, TooltipAttribute("ジャンプ力")]
    float m_jumpPower = 40.0f;

    [SerializeField, TooltipAttribute("ジャンプ後硬直終了時間")]
    float m_jumpAfterEndTime = 1.0f;

    [SerializeField, TooltipAttribute("地形判定のレイの長さ")]
    float m_groundCheckLayLength = 0.4f;

    [SerializeField, TooltipAttribute("1秒間に減る大きさの値")]
    float m_sizeDecrementionRate = 2.5f;

    [SerializeField, TooltipAttribute("ダメージ判定になる速度")]
    float m_damageVelocity = 5.0f;

    // Components
    Rigidbody m_rigidbody;

    // Animation
    PlayerCharacterAnimationBehaviour m_stateMachineBehaviour;
    Animator m_animator;

    // States
    [SerializeField]
    DriveState  m_driveState = DriveState.Normal;

    // Accel
    float       m_velocity = 0.0f;

    // Rotation
    float       m_rotation      = 0.0f;
    float       m_maxRotation   = 45.0f;
    float       m_rotationPower = 10.0f;

    float       m_pushRotationKeyTime = 0.0f;

    // Breake
    float       m_pushProgressBreakeKeyTime = 0.0f;      // 前回ブレーキキーが押されてからの時間

    // BreakeAfter
    float       m_breakeAfterTime = 0.0f;

    // Jump
    bool        m_isGround = true;

    // jumpAfter
    float m_jumpAfterTime = 0.0f;

    // Scale
    float m_size                = 100;
    float m_scaleMagnification  = 1.0f;     // 大きさ倍率
    Vector3 m_defaultScale;

    // Input
    bool        m_isPushJump;
    float       m_horizontal;

    // Property
    public PlayerCharacterAnimationBehaviour stateMachineBehaviour
    {
        get
        {
            return m_stateMachineBehaviour;
        }
        set
        {
            m_stateMachineBehaviour = value;
        }
    }

    public float size
    {
        get
        {
            return m_size;
        }
    }

    public DriveState state
    {
        get
        {
            return m_driveState;
        }
        set
        {
            m_driveState = value;
        }
    }



    void Start () {
        m_rigidbody = GetComponent<Rigidbody>();
        m_animator  = GetComponent<Animator>();

        m_animator.Play("Start");

        m_maxRotation       = m_maxRotationNormal;
        m_breakeAfterTime   = m_breakeAfterEndTime;

        m_defaultScale = transform.localScale;
    }
    
	void Update ()
    {
        m_isPushJump    = Input.GetKey(KeyCode.Space);
        m_horizontal    = Input.GetAxis("Horizontal");

        // ブレーキ入力チェック
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (m_pushProgressBreakeKeyTime <= m_breakeStartInputTime)
            {
                if(m_driveState == DriveState.Normal || m_driveState == DriveState.Drift)
                    m_driveState = DriveState.Breake;
            }

            m_pushProgressBreakeKeyTime = 0.0f;
        }

        // ブレーキ硬直解除
        if(m_driveState == DriveState.BreakeAfter)
        {
            m_breakeAfterTime += Time.deltaTime;
            if (m_breakeAfterTime > m_breakeAfterEndTime)
            { 
                m_driveState = DriveState.Normal;
            }
        }

        // ジャンプ硬直解除
        if(m_driveState == DriveState.JumpAfter)
        {
            m_jumpAfterTime += Time.deltaTime;

            if (m_jumpAfterTime > m_jumpAfterEndTime)
                m_driveState = DriveState.Normal;
        }

        // サイズ減少
        if(m_driveState == DriveState.Normal || 
            m_driveState == DriveState.Drift ||
            m_driveState == DriveState.JumpAfter)
        { 
            m_size -= m_sizeDecrementionRate * Time.deltaTime;
        }

        if (m_size <= 0.0f)
        {
            Debug.Log("Death");
        }


        float scale = ((m_size / 100) + 1) * 0.5f;

        transform.localScale = m_defaultScale * scale;

        // アニメーション更新
        m_animator.SetBool("isGround", m_isGround);
        m_animator.SetFloat("pushRotationKeyTime", m_pushRotationKeyTime);
        m_animator.SetFloat("rotation", m_rotation);

        if(m_driveState == DriveState.Breake ||
            m_driveState == DriveState.BreakeAfter)
        {
            m_animator.SetBool("isBreake", true);
        }
        else
            m_animator.SetBool("isBreake", false);
    }

    void FixedUpdate()
    {
        switch (m_driveState)
        {
            case DriveState.Normal:
                Accel();
                Rotate();
                Breake();
                Jump();
                break;

            case DriveState.Drift:
                Accel();
                Rotate();
                Breake();
                Jump();
                break;

            case DriveState.Breake:
                Breake();
                Rotate();
                break;

            case DriveState.BreakeAfter:
                Rotate();
                break;

            case DriveState.Jump:
                Accel();
                if (m_isGround && m_rigidbody.velocity.y <= 0.0f)
                {
                    m_driveState    = DriveState.JumpAfter;
                    m_jumpAfterTime = 0.0f;
                }
                break;

            case DriveState.JumpAfter:
                Accel();
                break;

            case DriveState.Damage:
                break;

            default:
                break;
        }

        int layerMask = (1 << LayerMask.NameToLayer("Terrain"));

        m_isGround = Physics.Raycast(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
            transform.position,
            transform.up * -1,
            m_groundCheckLayLength,         // 要調整
            layerMask);
    }

    void Accel()
    {
        m_velocity += m_acceleration * Time.fixedDeltaTime;

        if (m_velocity > m_maxVelocity)
        {
            m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_maxVelocity);
        }

        if (Mathf.Abs(m_velocity) > .0f)
            m_rigidbody.AddRelativeForce((Vector3.forward * m_velocity));

        // 速度制限を掛ける
        if (m_rigidbody.velocity.magnitude > m_maxVelocity)
        {
            m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_maxVelocity);
        }
    }

    void Breake()
    {
        if(m_driveState == DriveState.Breake)
        {
            m_velocity -= (m_velocity * m_breakePower * Time.fixedDeltaTime);

            if (m_velocity <= m_breakeVelocity)
            {
                m_driveState = DriveState.BreakeAfter;

                m_breakeAfterTime = 0.0f;
                m_velocity = 0.0f;
            }                
        }

        m_pushProgressBreakeKeyTime += Time.deltaTime;
    }

    void Rotate()
    {
        if (m_horizontal < 0.0f)
        {
            m_rotation -= m_rotationPower * Time.fixedDeltaTime;

            m_pushRotationKeyTime += Time.fixedDeltaTime;
        }

        if (m_horizontal > 0.0f)
        {
            m_rotation += m_rotationPower * Time.fixedDeltaTime;

            m_pushRotationKeyTime += Time.fixedDeltaTime;
        }

        if (m_horizontal == 0.0f)
        {
            m_rotation -= (m_rotation * m_rotationDecrementionRate * Time.fixedDeltaTime);

            m_pushRotationKeyTime = 0;

            if (m_driveState == DriveState.Drift)
            {
                m_driveState = DriveState.Normal;

                m_maxRotation   = m_maxRotationNormal;
                m_rotationPower = m_rotationPowerNormal;
            }
        }

        if(m_pushRotationKeyTime > m_driftStartInputTime)
        {
            if(m_driveState == DriveState.Normal)
            {
                m_driveState = DriveState.Drift;

                m_maxRotation   = m_maxRotationDrift;
                m_rotationPower = m_rotationPowerDrift;
            }
        }

        float rotationAbs = Mathf.Abs(m_rotation);

        if(rotationAbs > m_maxRotation)
        {
            m_rotation = Mathf.Clamp(m_rotation, -m_maxRotation, m_maxRotation);
        }

        if (rotationAbs > .0f)
        {
            transform.Rotate(.0f, Mathf.Deg2Rad * m_rotation, .0f);
            //m_rigidbody.AddRelativeTorque(Vector3.up * m_rotation * Time.deltaTime);
        }

        // Rigidbodyの回転を切る
        m_rigidbody.angularVelocity = new Vector3(.0f, .0f, .0f);
    }

    void Jump()
    {
        if (m_isPushJump)
        {
            if (m_isGround)      // 地形と接触していた
            {
                m_rigidbody.AddForce(Vector3.up * m_jumpPower);       // 上に飛ばす
                m_rotation = 0.0f;
                m_driveState = DriveState.Jump;
                m_animator.Play("Jump");
            }

            m_isPushJump = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            if(m_velocity >= m_damageVelocity)
            { 
                Damage();
            }
        }
    }

    void Damage()
    {
        m_driveState = DriveState.Damage;
        m_animator.Play("Damage");

        m_rotation = 0.0f;
        m_velocity = 0.0f;
    }
}
 