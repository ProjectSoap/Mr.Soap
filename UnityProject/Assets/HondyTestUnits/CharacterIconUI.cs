using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterIconUI : MonoBehaviour {

	// キャラの状態その1 こっち最優先に切り替え
	public enum ECharacterState
	{
		NORMAL,
		DAMAGE,
		HEAL,
		WASH_CHAIN,
		STATE_MAX
	}

	// キャラの状態その2 基本こっち
	public enum EHealthState
	{
		COMPLETE_RECOVERY,  // だいたい全快
		MINOR_INJURY,       // 軽傷
		SERIOUS_INJURY,     // 重傷
		STATE_MAX
	}

	PlayerCharacterController player;
	SpriteSwitcher switcher;

	private bool m_isWashChain;
	public bool IsWashChain
	{
		get { return m_isWashChain; }
		set { m_isWashChain = value; }
	}
	private bool m_isHeal;
	public bool IsHeal
	{
		get { return m_isHeal; }
		set { m_isHeal = value; }
	}

	[SerializeField]
	Sprite[] m_sekken_kunHelthSpriteArray = new Sprite[(int)EHealthState.STATE_MAX];
	[SerializeField]
	Sprite[] m_sekken_kunStateSpriteArray = new Sprite[(int)ECharacterState.STATE_MAX];

	[SerializeField]
	Sprite[] m_sekken_chanHelthSpriteArray = new Sprite[(int)EHealthState.STATE_MAX];
	[SerializeField]
	Sprite[] m_sekken_chanStateSpriteArray = new Sprite[(int)ECharacterState.STATE_MAX];


	[SerializeField]
	Sprite[] m_sekken_HeroHelthSpriteArray = new Sprite[(int)EHealthState.STATE_MAX];
	[SerializeField]
	Sprite[] m_sekken_HeroStateSpriteArray = new Sprite[(int)ECharacterState.STATE_MAX];

	Image iconOverFrame;
	Image m_iconImage;
	public int m_characterNum;
	public int m_characterMax = 3;

    private float m_controlTime = 0;
    private float m_changeIconTime = 1.0f;
	[SerializeField]
	float m_helthMax = 100;
	[SerializeField,Header("軽傷と全快の境界")]
	float m_completeRecoveryBorder = 60;
	[SerializeField,Header("重傷と軽傷の境界")]
	float m_minorInjuryBorder = 30;

	[SerializeField,Header("だいたい全快状態のフレームの色")]
	Color m_completeRecoveryColor;
	[SerializeField, Header("軽傷状態のフレームの色")]
	Color m_minorInjuryColor;
	[SerializeField, Header("重傷状態のフレームの色")]
	Color m_seriousInjuryColor;


	ECharacterState m_characterState;
	public CharacterIconUI.ECharacterState CharacterState
	{
		get { return m_characterState; }
		set { m_characterState = value; }
	}
	EHealthState m_healthState;
	public CharacterIconUI.EHealthState HealthState
	{
		get { return m_healthState; }
		set { m_healthState = value; }
	}
	// Use this for initialization
	void Start ()
	{
		player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
		switcher = this.transform.FindChild("SizeIcon").gameObject.GetComponent<SpriteSwitcher>();
		iconOverFrame = GameObject.Find("SizeIconOverFrame").GetComponent<Image>();
		m_iconImage = GameObject.Find("SizeIcon").GetComponent<Image>();
		m_characterNum =(int)SceneData.characterSelect;

		HealthStateEnterProcess();

	}

	// Update is called once per frame
	void Update ()
	{
		UpdateHealthState();
		UpdateCharacterState();
		HealthStateProcess();
		CharacterStateProcess();
	}

	void UpdateCharacterState()
	{

		switch (m_characterState)
		{
			case ECharacterState.NORMAL:
				if (player.state == PlayerCharacterController.DriveState.Damage)
				{
					CharacterStateExitProcess();
					m_characterState = ECharacterState.DAMAGE;
					CharacterStateEnterProcess();
                }
                if (m_isHeal)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.HEAL;
                    CharacterStateEnterProcess();
                }

                if (m_isWashChain)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.WASH_CHAIN;
                    CharacterStateEnterProcess();
                }
                break;
			case ECharacterState.DAMAGE:
				if (player.state != PlayerCharacterController.DriveState.Damage)
				{
					CharacterStateExitProcess();
					m_characterState = ECharacterState.NORMAL;
					CharacterStateEnterProcess();
				}
		        if (m_isHeal)
		        {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.HEAL;
                    CharacterStateEnterProcess();
                }

                if (m_isWashChain)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.WASH_CHAIN;
                    CharacterStateEnterProcess();
                }
                break;
			case ECharacterState.HEAL:
		        if (m_changeIconTime < m_controlTime)
                {
                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.NORMAL;
                    CharacterStateEnterProcess();
                }
		        if (player.state == PlayerCharacterController.DriveState.Damage)
                {
                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.DAMAGE;
                    CharacterStateEnterProcess();
                }
                if (m_isHeal)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.HEAL;
                    CharacterStateEnterProcess();
                }

                if (m_isWashChain)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.WASH_CHAIN;
                    CharacterStateEnterProcess();
                }
                break;
			case ECharacterState.WASH_CHAIN:
                if (m_changeIconTime < m_controlTime)
                {
                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.NORMAL;
                    CharacterStateEnterProcess();
                }
                if (player.state == PlayerCharacterController.DriveState.Damage)
                {
                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.DAMAGE;
                    CharacterStateEnterProcess();
                }
                if (m_isHeal)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.HEAL;
                    CharacterStateEnterProcess();
                }

                if (m_isWashChain)
                {

                    CharacterStateExitProcess();
                    m_characterState = ECharacterState.WASH_CHAIN;
                    CharacterStateEnterProcess();
                }
                break;
			default:
				break;
		}
	}

	void UpdateHealthState()
	{

		float size = player.size;
		switch (m_healthState)
		{
			case EHealthState.COMPLETE_RECOVERY:
				if (m_minorInjuryBorder <= size && size < m_completeRecoveryBorder)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.MINOR_INJURY;
					HealthStateEnterProcess();
				}
				else if(size < m_minorInjuryBorder)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.SERIOUS_INJURY;
					HealthStateEnterProcess();
				}
				break;
			case EHealthState.MINOR_INJURY:
				if (m_completeRecoveryBorder <= size)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.COMPLETE_RECOVERY;
					HealthStateEnterProcess();
				}
				else if (size < m_minorInjuryBorder)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.SERIOUS_INJURY;
					HealthStateEnterProcess();
				}
				break;
			case EHealthState.SERIOUS_INJURY:
				if (m_completeRecoveryBorder <= size)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.COMPLETE_RECOVERY;
					HealthStateEnterProcess();
				}
				else if (m_minorInjuryBorder <= size && size < m_completeRecoveryBorder)
				{
					HealthStateExitProcess();
					m_healthState = EHealthState.MINOR_INJURY;
					HealthStateEnterProcess();
				}
				break;
			default:
				break;
		}
	}

	void CharacterStateExitProcess()
	{
		switch (m_characterState)
		{
			case ECharacterState.NORMAL:
                m_controlTime = 0;
                break;
			case ECharacterState.DAMAGE:
		        m_controlTime = 0;
				break;
			case ECharacterState.HEAL:
                m_controlTime = 0;
                break;
			case ECharacterState.WASH_CHAIN:
                m_controlTime = 0;
                break;
			default:
				break;
		}
	}

	void HealthStateExitProcess()
	{
		switch (m_healthState)
		{
			case EHealthState.COMPLETE_RECOVERY:
				break;
			case EHealthState.MINOR_INJURY:
				break;
			case EHealthState.SERIOUS_INJURY:
				break;
			default:
				break;
		}
	}


	void CharacterStateEnterProcess()
	{
		switch (m_characterState)
		{
			case ECharacterState.NORMAL:

				switch (m_characterNum)
				{
					case 0:
						m_iconImage.sprite = m_sekken_kunHelthSpriteArray[(int)m_healthState];
						break;
					case 1:
						m_iconImage.sprite = m_sekken_HeroHelthSpriteArray[(int)m_healthState];
						break;
					case 2:
						m_iconImage.sprite = m_sekken_chanHelthSpriteArray[(int)m_healthState];
						break;
				}
				break;
			case ECharacterState.DAMAGE:
				switch (m_characterNum)
				{
					case 0:
						m_iconImage.sprite = m_sekken_kunStateSpriteArray[(int)m_characterState];
						break;
					case 1:
						m_iconImage.sprite = m_sekken_HeroStateSpriteArray[(int)m_characterState];
						break;
					case 2:
						m_iconImage.sprite = m_sekken_chanStateSpriteArray[(int)m_characterState];
						break;
				}
				break;
			case ECharacterState.HEAL:
		        m_isHeal = false;
               
                switch (m_characterNum)
				{
					case 0:
						m_iconImage.sprite = m_sekken_kunStateSpriteArray[(int)m_characterState];
						break;
					case 1:
						m_iconImage.sprite = m_sekken_HeroStateSpriteArray[(int)m_characterState];
						break;
					case 2:
						m_iconImage.sprite = m_sekken_chanStateSpriteArray[(int)m_characterState];
						break;
				}
				break;
			case ECharacterState.WASH_CHAIN:
                m_isWashChain = false;
                switch (m_characterNum)
				{
					case 0:
						m_iconImage.sprite = m_sekken_kunStateSpriteArray[(int)m_characterState];
						break;
					case 1:
						m_iconImage.sprite = m_sekken_HeroStateSpriteArray[(int)m_characterState];
						break;
					case 2:
						m_iconImage.sprite = m_sekken_chanStateSpriteArray[(int)m_characterState];
						break;
				}
				break;
			default:
				break;
		}
	}

	void HealthStateEnterProcess()
	{
		switch (m_healthState)
		{
			case EHealthState.COMPLETE_RECOVERY:

				if (m_characterState == ECharacterState.NORMAL)
				{
					switch (m_characterNum)
					{
						case 0:
							m_iconImage.sprite = m_sekken_kunHelthSpriteArray[(int)m_healthState];
							break;
						case 1:
							m_iconImage.sprite = m_sekken_HeroHelthSpriteArray[(int)m_healthState];
							break;
						case 2:
							m_iconImage.sprite = m_sekken_chanHelthSpriteArray[(int)m_healthState];
							break;
					}
				}
				iconOverFrame.color = m_completeRecoveryColor;
				break;
			case EHealthState.MINOR_INJURY:
				if (m_characterState == ECharacterState.NORMAL)
				{
					switch (m_characterNum)
					{
						case 0:
							m_iconImage.sprite = m_sekken_kunHelthSpriteArray[(int)m_healthState];
							break;
						case 1:
							m_iconImage.sprite = m_sekken_HeroHelthSpriteArray[(int)m_healthState];
							break;
						case 2:
							m_iconImage.sprite = m_sekken_chanHelthSpriteArray[(int)m_healthState];
							break;
					}
				}
				iconOverFrame.color = m_minorInjuryColor;
				break;
			case EHealthState.SERIOUS_INJURY:
				if (m_characterState == ECharacterState.NORMAL)
				{
					switch (m_characterNum)
					{
						case 0:
							m_iconImage.sprite = m_sekken_kunHelthSpriteArray[(int)m_healthState];
							break;
						case 1:
							m_iconImage.sprite = m_sekken_HeroHelthSpriteArray[(int)m_healthState];
							break;
						case 2:
							m_iconImage.sprite = m_sekken_chanHelthSpriteArray[(int)m_healthState];
							break;
					}
				}
				iconOverFrame.color = m_seriousInjuryColor;
				break;
			default:
				break;
		}
	}

	void CharacterStateProcess()
	{
		switch (m_characterState)
		{
			case ECharacterState.NORMAL:
				break;
			case ECharacterState.DAMAGE:

				break;
			case ECharacterState.HEAL:

                m_controlTime += Time.deltaTime;
                break;
			case ECharacterState.WASH_CHAIN:
                m_controlTime += Time.deltaTime;
                break;
			default:
				break;
		}
	}
	void HealthStateProcess()
	{
		switch (m_healthState)
		{
			case EHealthState.COMPLETE_RECOVERY:
				break;
			case EHealthState.MINOR_INJURY:
				break;
			case EHealthState.SERIOUS_INJURY:
				break;
			default:
				break;
		}
	}

}
