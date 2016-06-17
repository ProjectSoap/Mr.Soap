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

    [SerializeField, Tooltip("ダメージで減る大きさ")]
    float m_damageSize;

    [SerializeField, Tooltip("ウォッシュチェインで回復する大きさ")]
    float m_healSizeWashChain = 40.0f;

    [SerializeField, Tooltip("アイテムで回復する大きさ")]
    float m_healSizeItem = 40.0f;

    [SerializeField, Tooltip("最大サイズ")]
    float m_maxSize = 100.0f;

    [SerializeField, Tooltip("ダメージ判定になる速度"), Header("ダメージ")]
    float m_damageVelocity = 5.0f;

    [SerializeField, Tooltip("ダメージ判定になる接触点の角度")]
    float m_damageAngle = 45.0f;

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

    [SerializeField, Tooltip("風時の泡飛距離")]
    float m_windAddBubbleWidth;

    [SerializeField, Tooltip("風時の泡高さ")]
    float m_windAddBubbleHeight;

    [SerializeField, Tooltip("ドリフトパーティクル右"), Header("パーティクル")]
    GameObject m_driftParticleEmitterRight;

    [SerializeField, Tooltip("ドリフトパーティクル左")]
    GameObject m_driftParticleEmitterLeft;

    [SerializeField, Tooltip("移動パーティクルが出る速度")]
    float m_moveBubbleEmissionVelocity;

    [SerializeField, Tooltip("ダメージパーティクル")]
    GameObject m_damageParticleEmitter;

    [SerializeField, Tooltip("回復パーティクル")]
    GameObject m_healParticleEmitter;

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

    [SerializeField]
    bool m_isStayBuilding;

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
    [SerializeField]
    WeatherState m_weatherState;

    Vector3 m_windDirection = new Vector3(.0f, .0f, .0f);

    float m_weatherAddMaxVelocity;
    float m_weatherAddAcceleration;
    float m_weatherAddRotationPower;
    float m_weatherAddMaxRotation;
    float m_weatherAddDriftStartTime;
    float m_weatherAddBreakePower;
    float m_weatherAddBubbleWidth;
    float m_weatherAddBubbleHeight;

    // Particle Effect
    ParticleSystem m_driftParticleSystemRight;
    ParticleSystem m_driftParticleSystemLeft;

    ParticleSystem m_moveBubbleSystem;

    EndStateSystem m_endStateSystem;

    Vector3 m_reflect;

    Transform m_pauseObjectTransform;

    [SerializeField]
    Vector3 v;

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

    public float weatherAddBubbleWidth
    {
        get { return m_weatherAddBubbleWidth; }
    }

    public float weatherAddBubbleHeight
    {
        get { return m_weatherAddBubbleHeight; }
    }

    public Rigidbody rigidBody
    {
        get { return m_rigidbody; }
        set { m_rigidbody = value; }
    }

    public float velocity
    {
        get { return m_velocity; }
        set { m_velocity = value; }
    }

    void Awake()
    {
        gameObject.name = "PlayerCharacter";
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
        m_moveBubbleSystem          = transform.FindChild("MoveBubble").GetComponent<ParticleSystem>();
        m_pauseObjectTransform      = transform.parent;

        m_driftParticleSystemRight.enableEmission   = false;
        m_driftParticleSystemLeft.enableEmission    = false;
        m_driftParticleSystemRight.Clear();
        m_driftParticleSystemLeft.Clear();

        m_moveBubbleSystem.enableEmission = false;
        m_moveBubbleSystem.Clear();

        m_endStateSystem.Sekkenkun = gameObject;

        m_maxRotation       = m_maxRotationNormal;
        m_rotationPower     = m_rotationPowerNormal;
        m_breakeAfterTime   = m_breakeAfterEndTime;
        m_defaultScale      = transform.localScale;
        m_driveState        = DriveState.Start;
    }

    void Update()
    {
#if DEBUG

        if (Input.GetKeyDown(KeyCode.Backspace))
        {
            Reset();
        }

        v = m_rigidbody.velocity;

#endif

        m_isPushJump = Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Joystick1Button0);
        //m_horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.RightArrow))
            m_horizontal = 1.0f;
        else if (Input.GetKey(KeyCode.LeftArrow))
            m_horizontal = -1.0f;
        else
            m_horizontal = 0.0f;

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

                        m_animator.Play("Breake");

                        var obj = GameObject.Instantiate(m_breakeBubble, transform.position, transform.rotation) as GameObject;
                        var bubbleBullet = obj.GetComponent<BubbleBullet>();

                        bubbleBullet.MoveWidth  += m_windAddBubbleWidth;
                        bubbleBullet.MoveHeight += m_windAddBubbleHeight;

                        m_driftParticleSystemRight.enableEmission   = true;
                        m_driftParticleSystemLeft.enableEmission    = true;

                        BGMManager.Instance.PlaySE("Soap_Brake");
                        //BGMManager.Instance.PlaySE("Wash_Fly");                        
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
            m_moveBubbleSystem.enableEmission = false;

            m_velocity = 0.0f;
            m_rotation = 0.0f;

            m_endStateSystem.StartEndState();
        }

        if(m_driveState == DriveState.End)
        {
            m_meshObject.transform.Rotate(transform.up, 10.0f);
            //m_meshObject.transform.Translate(new Vector3(0, 0.0025f, 0));
        }

        float scaleRate = ((m_size / 100) + 1) * 0.5f;

        transform.localScale = m_defaultScale * scaleRate;

        // アニメーション更新
        m_animator.SetBool("isGround",  m_isGround);
        m_animator.SetFloat("rotation", m_rotation);

        //if (m_driveState == DriveState.Breake ||
        //    m_driveState == DriveState.BreakeAfter)
        //{
        //    m_animator.SetBool("isBreake", true);
        //}
        //else
        //    m_animator.SetBool("isBreake", false);

        // パーティクル更新
        if (m_driveState != DriveState.Drift && m_driveState != DriveState.Breake)
        {
            m_driftParticleSystemRight.enableEmission   = false;
            m_driftParticleSystemLeft.enableEmission    = false;
        }

        if (m_velocity >= m_moveBubbleEmissionVelocity)
            m_moveBubbleSystem.enableEmission = true;
        else
            m_moveBubbleSystem.enableEmission = false;

        // 天候チェック
        switch (m_weatherState)
        {
            case WeatherState.Sunny:
                m_weatherAddMaxVelocity     = .0f;
                m_weatherAddAcceleration    = .0f;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                m_weatherAddBubbleWidth     = .0f;
                m_weatherAddBubbleHeight    = .0f;
                break;
            case WeatherState.Rain:
                m_weatherAddMaxVelocity     = m_rainAddMaxVelocity;
                m_weatherAddAcceleration    = m_rainAddAcceleration;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                break;
            case WeatherState.Wind:
                m_weatherAddMaxVelocity     = .0f;
                m_weatherAddAcceleration    = .0f;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                m_weatherAddBubbleWidth     = m_windAddBubbleWidth;
                m_weatherAddBubbleHeight    = m_windAddBubbleHeight;
                break;
            case WeatherState.Fog:
                m_weatherAddMaxVelocity     = .0f;
                m_weatherAddAcceleration    = .0f;
                m_weatherAddMaxRotation     = .0f;
                m_weatherAddRotationPower   = .0f;
                m_weatherAddDriftStartTime  = .0f;
                m_weatherAddBubbleWidth     = .0f;
                m_weatherAddBubbleHeight    = .0f;
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

                    Instantiate(m_jumpBubble, transform.position, m_jumpBubble.transform.rotation);

                    //BGMManager.Instance.PlaySE("Wash_Fly");
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

        int layerMask = 
            (1 << LayerMask.NameToLayer("Terrain")) + 
            (1 << LayerMask.NameToLayer("Building")) + 
            (1 << LayerMask.NameToLayer("Car"));

        m_isGround = Physics.Raycast(       // せっけんくんの真下にレイを飛ばして地形と接触チェック
            transform.position,
            transform.up * -1,
            m_groundCheckLayLength,
            layerMask);
    }

    void Accel()
    {
        if(m_isGround)
            m_velocity += (m_acceleration + m_weatherAddAcceleration) * Time.fixedDeltaTime;

        m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_maxVelocity + m_weatherAddMaxVelocity);

        if (m_isStayBuilding)
        { 
            m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_stayBuildingMaxVelocity);
            m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_stayBuildingMaxVelocity);
        }

        if (Mathf.Abs(m_velocity) > .0f && !m_isStayBuilding)
            m_rigidbody.AddRelativeForce((Vector3.forward * m_velocity));

        if(m_isStayBuilding)
        {
            m_rigidbody.AddForce(m_reflect * m_velocity, ForceMode.Force);
            //m_rigidbody.velocity = m_reflect * m_velocity;
        }

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

                m_driftParticleSystemRight.enableEmission   = false;
                m_driftParticleSystemLeft.enableEmission    = false;
            }
        }

        m_pushProgressBreakeKeyTime += Time.deltaTime;
    }

    void Rotate()
    {
        if (m_horizontal < 0.0f)
        {
            if (m_rotation > 0.0f)
            { 
                m_rotation = 0.0f;
                m_pushRotationKeyTime = 0.0f;

                if (m_driveState == DriveState.Drift)
                {
                    m_driveState = DriveState.Normal;

                    m_maxRotation = m_maxRotationNormal;
                    m_rotationPower = m_rotationPowerNormal;

                    //m_bubbleDriftShooter.StopShot();
                }
            }

            m_rotation -= m_rotationPower * Time.deltaTime;

            if (m_driveState == DriveState.Normal)
                m_pushRotationKeyTime += Time.deltaTime;
        }

        if (m_horizontal > 0.0f)
        {
            if (m_rotation < 0.0f)
            {
                m_rotation = 0.0f;
                m_pushRotationKeyTime = 0.0f;

                if (m_driveState == DriveState.Drift)
                {
                    m_driveState = DriveState.Normal;

                    m_maxRotation = m_maxRotationNormal;
                    m_rotationPower = m_rotationPowerNormal;

                    //m_bubbleDriftShooter.StopShot();
                }
            }

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
            m_rotation *= m_rotationDecrementionRate;

            m_pushRotationKeyTime = 0;

            m_horizontal = 0.0f;

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

                BGMManager.Instance.PlaySE("Soap_Drift");
            }
        }

        m_rotation = Mathf.Clamp(m_rotation, -m_maxRotation, m_maxRotation);

        float rotationAbs = Mathf.Abs(m_rotation);

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

                //m_velocity      = 0.0f;
                m_rotation      = 0.0f;
                m_driveState    = DriveState.Jump;

                BGMManager.Instance.PlaySE("Soap_Jump");
            }

            m_isPushJump = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            ContactPoint contactPoint = collision.contacts[0];

            Vector3 forward = transform.forward.normalized;
            Vector3 vector  = (contactPoint.point - transform.position).normalized;

            float angle = Vector3.Angle(forward, vector);

            if( !m_isStayBuilding &&
                angle <= m_damageAngle &&
                m_velocity >= m_damageVelocity)
            {
                Damage();

                Debug.Log("HIIIIIITTTT!!!!!!!!!");
            }

            Debug.Log(angle.ToString() + " On Hit");

            Vector3 normal = collision.contacts[0].normal.normalized;
            forward = transform.forward;

            m_reflect = (forward - (Vector3.Dot(forward, normal) * normal)).normalized;
            m_reflect.y = 0.0f;


            m_isStayBuilding = true;
        }

        if(collision.gameObject.layer == LayerMask.NameToLayer("Car"))
        {
            Damage();
        }
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            Vector3 normal = collision.contacts[0].normal.normalized;
            Vector3 forward = transform.forward;

            m_reflect = (forward - (Vector3.Dot(forward, normal) * normal)).normalized;
            m_reflect.y = 0.0f;

            //m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_stayBuildingMaxVelocity);

            //m_rigidbody.AddForce(reflect * m_velocity);
            //m_rigidbody.velocity = reflect;
            //m_rigidbody.velocity = Vector3.ClampMagnitude(m_rigidbody.velocity, m_stayBuildingMaxVelocity);

            m_isStayBuilding = true;
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer("Building"))
        {
            m_isStayBuilding = false;

            Debug.Log("Exit Collision");
        }
    }

    //void OnTriggerStay(Collider collider)
    //{
    //    if (collider.gameObject.layer == LayerMask.NameToLayer("Building") ||
    //        collider.gameObject.layer == LayerMask.NameToLayer("Car"))
    //    {
    //        if (m_velocity >= m_damageVelocity)
    //        {
    //            Damage();
    //        }
    //        else
    //        {
    //            m_velocity = Mathf.Clamp(m_velocity, 0.0f, m_stayBuildingMaxVelocity);
    //            m_velocity *= m_stayBuildingVelocityRate;
    //        }
    //    }
    //}

    void Damage()
    {
        m_animator.Play("Damage");

        m_driveState        = DriveState.Damage;
        m_rotation          = 0.0f;
        m_velocity          = 0.0f;
        m_damageAfterTime   = 0.0f;

        m_size              -= m_damageSize;

        var particleEmitter = Instantiate(m_damageParticleEmitter, transform.position, transform.rotation) as GameObject;
        var particleSystem = particleEmitter.GetComponent<ParticleSystem>();

        Destroy(particleEmitter, particleSystem.duration);

        BGMManager.Instance.PlaySE("Collision");
    }

    public void WashChain()
    {
        m_size += m_healSizeWashChain;
        m_size = Mathf.Clamp(m_size, .0f, m_maxSize);

        var particleEmitter = Instantiate(m_healParticleEmitter, transform.position, transform.rotation) as GameObject;
        var particleSystem = particleEmitter.GetComponent<ParticleSystem>();

        particleEmitter.transform.parent = transform;

        Destroy(particleEmitter, particleSystem.duration);

        BGMManager.Instance.PlaySE("Wash_Chain_MAX");
    }

    public void Heal()
    {
        m_size += m_healSizeItem;
        m_size = Mathf.Clamp(m_size, .0f, m_maxSize);

        var particleEmitter = Instantiate(m_healParticleEmitter, transform.position, transform.rotation) as GameObject;
        var particleSystem = particleEmitter.GetComponent<ParticleSystem>();

        particleEmitter.transform.parent = transform;

        Destroy(particleEmitter, particleSystem.duration);

        BGMManager.Instance.PlaySE("Recovery");
    }

    public void PlayStart()
    {
        m_driveState = PlayerCharacterController.DriveState.Normal;
        //m_moveBubbleSystem.enableEmission = true;
    }

    public void Reset()
    {
# if DEBUG

        m_driftParticleSystemRight.enableEmission = false;
        m_driftParticleSystemLeft.enableEmission = false;
        m_driftParticleSystemRight.Clear();
        m_driftParticleSystemLeft.Clear();

        m_moveBubbleSystem.enableEmission = false;
        m_moveBubbleSystem.Clear();

        m_maxRotation = m_maxRotationNormal;
        m_breakeAfterTime = m_breakeAfterEndTime;
        m_driveState = DriveState.Start;

        m_velocity = 0.0f;
        m_rotation = 0.0f;

        size = 100.0f;

#endif
    }
}
