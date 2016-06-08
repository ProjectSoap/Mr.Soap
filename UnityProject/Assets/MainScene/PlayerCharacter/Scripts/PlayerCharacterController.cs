using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Animator))]

public class PlayerCharacterController : MonoBehaviour
{
    public enum DriveState
    {
        Start,
        Normal,
        Drift,
        Breake,
        BreakeAfter,
        Jump,
        JumpAfter,
        Damage,
        DamageAfter,
        End,
    }

    public enum WeatherState
    {
        Sunny,
        Rain,
        Wind,
        Fog,
    }

    enum WindState
    {
        Head,       // 向かい風
        Tail,       // 追い風
        Right,
        Left,
    }

    [SerializeField, Tooltip("最大速度"), Header("速度")]
    float m_maxVelocity = 10.0f;

    [SerializeField, Tooltip("加速度")]
    float m_acceleration = 0.05f;

    [SerializeField, Range(0.0f, 1.0f), Tooltip("回転を元に戻す力 0に行くほど強い"), Header("回転")]
    float m_rotationDecrementionRate = 0.9f;

    [SerializeField, Tooltip("通常時回転力")]
    float m_rotationPowerNormal = 10.0f;

    [SerializeField, Tooltip("ドリフト時回転力")]
    float m_rotationPowerDrift = 25.0f;

    [SerializeField, Tooltip("通常時最大回転度")]
    float m_maxRotationNormal = 45.0f;

    [SerializeField, Tooltip("ドリフト時最大回転度")]
    float m_maxRotationDrift = 60.0f;

    [SerializeField, Tooltip("ドリフトが開始されるまでの入力時間")]
    float m_driftStartInputTime = 1.0f;

    [SerializeField, Range(1.0f, 0.0f), Tooltip("ドリフトがキャンセルされるまでのHorizontalの値")]
    float m_driftCancelHorizontal = 0.0f;

    [SerializeField, Tooltip("ブレーキが開始されるまでの入力受付時間"), Header("ブレーキ")]
    float m_breakeStartInputTime = 1.0f;

    [SerializeField, Range(0.0f, 1.0f), Tooltip("ブレーキ力 0に行くほど強い")]
    float m_breakePower = 0.9f;

    [SerializeField, Tooltip("ブレーキ可能速度")]
    float m_breakeStartVelocity = 0.5f;

    [SerializeField, Tooltip("ブレーキ後の硬直に遷移する速度の値")]
    float m_breakeStopVelocity = 0.5f;

    [SerializeField, Tooltip("ブレーキ後の硬直時間")]
    float m_breakeAfterEndTime = 1.0f;

    [SerializeField, Tooltip("ジャンプ力"), Header("ジャンプ")]
    float m_jumpPower = 40.0f;

    [SerializeField, Tooltip("ジャンプ後硬直終了時間")]
    float m_jumpAfterEndTime = 1.0f;

    [SerializeField, Tooltip("地形判定のレイの長さ")]
    float m_groundCheckLayLength = 0.4f;

    [SerializeField, Tooltip("1秒間に減る大きさの値"), Header("大きさ")]
    float m_sizeDecrementionRate = 2.5f;

    [SerializeField, Tooltip("ウォッシュチェインで回復する大きさ")]
    float m_healSizeWashChain = 40.0f;

    [SerializeField, Tooltip("最大サイズ")]
    float m_maxSize = 100.0f;

    [SerializeField, Tooltip("ダメージ判定になる速度"), Header("ダメージ")]
    float m_damageVelocity = 5.0f;

    [SerializeField, Range(0.0f, 1.0f), Tooltip("ダメージ判定でないときビルに接触すると速度に掛ける")]
    float m_stayBuildingVelocityRate = 0.5f;

    [SerializeField, Tooltip("ダメージ判定でないときの最高速度")]
    float m_stayBuildingMaxVelocity = 5.0f;

    [SerializeField, Tooltip("ダメージ後硬直時間")]
    float m_damageAfterStopTime = 1.0f;

    [SerializeField, Tooltip("ドリフト泡"), Header("泡")]
    GameObject m_driftBubble;

    [SerializeField, Tooltip("ブレーキ泡")]
    GameObject m_breakeBubble;

    [SerializeField, Tooltip("ジャンプ泡")]
    GameObject m_jumpBubble;

    [SerializeField, Tooltip("雨天時最高速度加算値"), Header("天候")]
    float m_rainAddMaxVelocity;

    [SerializeField, Tooltip("雨天時加速度加算値")]
    float m_rainAddAcceleration;

    [SerializeField, Tooltip("横向きの風と判定する角度")]
    float m_windSideAngle;

    [SerializeField, Tooltip("向かい風と判定する角度")]
    float m_windHeadAngle;

    [SerializeField, Tooltip("追い風時の最大速度")]
    float m_windHeadMaxVelocity;

    [SerializeField, Tooltip("追い風時の加速度加算値")]
    float m_windHeadAcceleration;

    [SerializeField, Tooltip("向かい風時の最大速度")]
    float m_windTailMaxVelocity;

    [SerializeField, Tooltip("向かい風時の加速度加算値")]
    float m_windTailAcceleration;

    [SerializeField, Tooltip("横向きの風時の回転力加算値")]
    float m_windSideAddRotationPower;

    [SerializeField, Tooltip("横向きの風時の最大回転力加算値")]
    float m_windSideAddMaxRotation;

    [SerializeField, Tooltip("横向きの風時のドリフト時間加算値")]
    float m_windSideAddDriftStartTime;

    [SerializeField, Tooltip("ドリフトパーティクル右"), Header("パーティクル")]
    GameObject m_driftParticleEmitterRight;

    [SerializeField, Tooltip("ドリフトパーティクル左")]
    GameObject m_driftParticleEmitterLeft;

    // Rigidbody
    Rigidbody m_rigidbody;

    // Animation
    PlayerCharacterAnimationBehaviour m_stateMachineBehaviour;
    Animator m_animator;

    // States
    [SerializeField, Header("デバッグ用")]
    DriveState m_driveState = DriveState.Normal;

    // Accel
    [SerializeField]
    float m_velocity = 0.0f;

    // Rotation
    [SerializeField]
    float m_rotation = 0.0f;
    float m_maxRotation = 45.0f;
    float m_rotationPower = 10.0f;

    [SerializeField]
    float m_pushRotationKeyTime = 0.0f;

    // Breake
    [SerializeField]
    float m_pushProgressBreakeKeyTime = 0.0f;      // 前回ブレーキキーが押されてからの時間

    // BreakeAfter
    float m_breakeAfterTime = 0.0f;

    // Jump
    [SerializeField]
    bool m_isGround = true;

    // jumpAfter
    float m_jumpAfterTime = 0.0f;

    // Damage
    float m_damageAfterTime = 0.0f;

    // Scale
    [SerializeField]
    float m_size = 100;
    //float       m_scaleMagnification  = 1.0f;     // 大きさ倍率
    Vector3 m_defaultScale = new Vector3(1, 1, 1);

    // Input
    bool m_isPushJump = false;
    [SerializeField]
    float m_horizontal = 0.0f;

    // Animation
    float m_animationTime = 0.0f;

    GameObject m_meshObject;

    // Bubble
    BubbleDriftShooter m_bubbleDriftShooter;

    // Weather
    WeatherState m_weatherState;

    Vector3 m_windDirection = new Vector3(.0f, .0f, .0f);

    float m_weatherAddMaxVelocity;
    float m_weatherAddAcceleration;
    float m_weatherAddRotationPower;
    float m_weatherAddMaxRotation;
    float m_weatherAddDriftStartTime;
    float m_weatherAddBreakePower;

    // Particle Effect
    ParticleSystem m_driftParticleSystemRight;
    ParticleSystem m_driftParticleSystemLeft;

    EndStateSystem m_endStateSystem;

    // Property
    public PlayerCharacterAnimationBehaviour stateMachineBehaviour
    {
        get { return m_stateMachineBehaviour; }
        set { m_stateMachineBehaviour = value; }
    }

    public float size
    {
        set { m_size = value; }
        get { return m_size; }
    }

    public DriveState state
    {
        get { return m_driveState; }
        set { m_driveState = value; }
    }

    public WeatherState weatherState
    {
        get { return m_weatherState; }
        set { m_weatherState = value; }
    }

    public Vector3 windDirection
    {
        get { return m_windDirection; }
        set { m_windDirection = value; }
    }

    public Animator animator
    {
        get { return m_animator; }
    }

    public GameObject meshObject
    {
        get { return m_meshObject; }
        set { m_meshObject = value; }
    }

    void Start()
    {
        m_rigidbody                 = GetComponent<Rigidbody>();
        m_animator                  = GetComponent<Animator>();
        m_bubbleDriftShooter        = GetComponentInChildren<BubbleDriftShooter>();
        m_driftParticleSystemRight  = m_driftParticleEmitterRight.GetComponent<ParticleSystem>();
        m_driftParticleSystemLeft   = m_driftParticleEmitterLeft.GetComponent<ParticleSystem>();
        m_endStateSystem            = GameObject.Find("EndStateSystem").GetComponent<EndStateSystem>();
        m_meshObject                = transform.FindChild("Mesh").gameObject;

        m_driftParticleSystemRight.enableEmission   = false;
        m_driftParticleSystemLeft.enableEmission    = false;
        m_driftParticleSystemRight.Clear();
        m_driftParticleSystemLeft.Clear();

        m_maxRotation       = m_maxRotationNormal;
        m_breakeAfterTime   = m_breakeAfterEndTime;
        m_defaultScale      = transform.localScale;
        m_driveState        = DriveState.Start;
    }

    void Update()
    {
        m_isPushJump = Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0);
        m_horizontal = Input.GetAxis("Horizontal");

        // ゲーム開始入力チェック
        if (m_isPushJump && m_driveState == DriveState.Start)
        {
            m_animator.Play("Start");

            m_isPushJump = false;
        }

        // ブレーキ入力チェック
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            if (m_pushProgressBreakeKeyTime <= m_breakeStartInputTime)
            {
                if (m_velocity >= m_breakeStartVelocity)
                {
                    if (m_driveState == DriveState.Normal || m_driveState == DriveState.Drift)
                    {
                        m_driveState    = DriveState.Breake;
                        m_rotation      = 0.0f;

                        GameObject.Instantiate(m_breakeBubble, transform.position, transform.rotation);
                    }
                }
            }

            m_pushProgressBreakeKeyTime = 0.0f;
        }

        // ブレーキ硬直解除
        if (m_driveState == DriveState.BreakeAfter)
        {
            m_breakeAfterTime += Time.deltaTime;

            if (m_breakeAfterTime >= m_breakeAfterEndTime)
            {
                m_driveState = DriveState.Normal;
            }
        }

        // ジャンプ硬直解除
        if (m_driveState == DriveState.JumpAfter)
        {
            m_jumpAfterTime += Time.deltaTime;

            if (m_jumpAfterTime >= m_jumpAfterEndTime)
                m_driveState = DriveState.Normal;
        }

        // ダメージ硬直解除
        if (m_driveState == DriveState.DamageAfter)
        {
            m_damageAfterTime += Time.deltaTime;

            if (m_damageAfterTime >= m_damageAfterStopTime)
            {
                m_driveState = DriveState.Normal;
            }
        }

        // サイズ減少
        if (m_driveState == DriveState.Normal ||
            m_driveState == DriveState.Drift ||
            m_driveState == DriveState.JumpAfter)
        {
            m_size -= m_sizeDecrementionRate * Time.deltaTime;
        }

        if (m_size <= 0.0f && m_driveState != DriveState.End)
        {
            Debug.Log("Sekkenkun Died");
            m_driveState = DriveState.End;
            m_endStateSystem.StartEndState();
        }

        float scaleRate = ((m_size / 100) + 1) * 0.5f;

        transform.localScale = m_defaultScale * scaleRate;

        // アニメーション更新
        m_animator.SetBool("isGround",  m_isGround);
        m_animator.SetFloat("rotation", m_rotation);

        if (m_driveState == DriveState.Breake ||
            m_driveState == DriveState.BreakeAfter)
        {
            m_animator.SetBool("isBreake", true);
        }
        else
            m_animator.SetBool("isBreake", false);

        // パーティクル更新
        if (m_driveState != DriveState.Drift)
        {
            m_driftParticleSystemRight.enableEmission   = false;
            m_driftParticleSystemLeft.enableEmission    = false;
        }

        // 天候チェック
        switch (m_weatherState)
        {
            case WeatherState.Sunny:
                m_weatherAddMaxVelocity     = .0f;
                m_weatherAddAcceleration    = .0f;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                break;
            case WeatherState.Rain:
                m_weatherAddMaxVelocity     = m_rainAddMaxVelocity;
                m_weatherAddAcceleration    = m_rainAddAcceleration;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                break;
            case WeatherState.Wind:

                float windAngle = Vector3.Angle(transform.forward, windDirection);
                float windAngleAbs = Mathf.Abs(windAngle);

                if (windAngleAbs < m_windSideAngle)
                {
                    // 追い風
                }
                else if (windAngleAbs >= m_windSideAngle)
                {
                    // 横向きの風
                }
                else if (windAngleAbs >= m_windHeadAngle)
                {
                    //　向かい風
                }

                break;
            case WeatherState.Fog:
                m_weatherAddMaxVelocity     = .0f;
                m_weatherAddAcceleration    = .0f;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                break;
            default:
                break;
        }
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
                //Rotate();
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

                    Instantiate(m_jumpBubble, transform.position, transform.rotation);
                }
                break;

            case DriveState.JumpAfter:
                Accel();
                break;

            case DriveState.Damage:
                break;

            case DriveState.DamageAfter:
                Rotate();
                break;

            default:
                break;
        }

        int layerMask = (1 << LayerMask.NameToLayer("Terrain")) + (1 << LayerMask.NameToLayer("Building"));

        m_isGround = Physics.Raycast(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
            transform.position,
            transform.up * -1,
            m_groundCheckLayLength,
            layerMask);
    }

    void Accel()
    {
        m_velocity += (m_acceleration + m_weatherAddAcceleration) * Time.fixedDeltaTime;

        if (m_velocity > m_maxVelocity + m_weatherAddMaxVelocity)
        {
            m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_maxVelocity + m_weatherAddMaxVelocity);
        }

        if (Mathf.Abs(m_velocity) > .0f)
            m_rigidbody.AddRelativeForce((Vector3.forward * m_velocity));

        // 速度制限を掛ける
        m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_maxVelocity);

        //if (m_rigidbody.velocity.magnitude > m_maxVelocity)
        //{
        //    m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_maxVelocity);
        //}
    }

    void Breake()
    {
        if (m_driveState == DriveState.Breake)
        {
            m_velocity -= (m_velocity * m_breakePower * Time.fixedDeltaTime);

            if (m_velocity <= m_breakeStopVelocity)
            {
                m_driveState = DriveState.BreakeAfter;

                m_breakeAfterTime   = 0.0f;
                m_velocity          = 0.0f;
            }
        }

        m_pushProgressBreakeKeyTime += Time.deltaTime;
    }

    void Rotate()
    {
        if (m_horizontal < 0.0f)
        {
            m_rotation -= m_rotationPower * Time.deltaTime;

            if (m_driveState == DriveState.Normal)
                m_pushRotationKeyTime += Time.deltaTime;
        }

        if (m_horizontal > 0.0f)
        {
            m_rotation += m_rotationPower * Time.deltaTime;

            if (m_driveState == DriveState.Normal)
                m_pushRotationKeyTime += Time.deltaTime;
        }

        if (m_driveState != DriveState.Normal && m_driveState != DriveState.Drift)
            m_pushRotationKeyTime = 0.0f;

        if (m_driveState == DriveState.Drift)
        {
            if (m_rotation > 0.0f)
            {
                m_driftParticleSystemRight.enableEmission   = true;
                m_driftParticleSystemLeft.enableEmission    = false;
                //m_driftParticleSystemLeft.Clear();
            }
            else
            {
                m_driftParticleSystemRight.enableEmission   = false;
                m_driftParticleSystemLeft.enableEmission    = true;
                //m_driftParticleSystemRight.Clear();
            }
        }

        if (Mathf.Abs(m_horizontal) <= m_driftCancelHorizontal)
        {
            m_rotation -= (m_rotation * m_rotationDecrementionRate * Time.deltaTime);

            m_pushRotationKeyTime = 0;

            if (m_driveState == DriveState.Drift)
            {
                m_driveState = DriveState.Normal;

                m_maxRotation   = m_maxRotationNormal;
                m_rotationPower = m_rotationPowerNormal;
            }
        }

        if (m_pushRotationKeyTime >= m_driftStartInputTime)
        {
            if (m_driveState == DriveState.Normal)
            {
                m_driveState = DriveState.Drift;

                m_maxRotation   = m_maxRotationDrift;
                m_rotationPower = m_rotationPowerDrift;

                if (m_horizontal > 0.0f)
                {
                    m_bubbleDriftShooter.ShotRight();

                    m_driftParticleSystemRight.enableEmission   = true;
                    m_driftParticleSystemLeft.enableEmission    = false;
                }
                else
                {
                    m_bubbleDriftShooter.ShotLeft();

                    m_driftParticleSystemRight.enableEmission = false;
                    m_driftParticleSystemLeft.enableEmission = true;
                }
            }
        }

        m_rotation = Mathf.Clamp(m_rotation, -m_maxRotation, m_maxRotation);

        float rotationAbs = Mathf.Abs(m_rotation);

        //if (rotationAbs > m_maxRotation)
        //{
        //    m_rotation = Mathf.Clamp(m_rotation, -m_maxRotation, m_maxRotation);
        //}

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
                m_animator.Play("Jump");

                //m_driftParticleSystemRight.Clear();
                //m_driftParticleSystemLeft.Clear();

                m_rotation      = 0.0f;
                m_driveState    = DriveState.Jump;
            }

            m_isPushJump = false;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.layer == LayerMask.NameToLayer("Building") ||
            collider.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            if (m_velocity >= m_damageVelocity)
            {
                Damage();
            }
            else
            {
                m_velocity *= m_stayBuildingVelocityRate;
                m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_stayBuildingMaxVelocity);
            }
        }
    }

    void Damage()
    {
        m_animator.Play("Damage");

        m_driveState        = DriveState.Damage;
        m_rotation          = 0.0f;
        m_velocity          = 0.0f;
        m_damageAfterTime   = 0.0f;
    }

    public void WashChain()
    {
        m_size += m_healSizeWashChain;
        m_size = Mathf.Clamp(m_size, .0f, m_maxSize);
    }
}
