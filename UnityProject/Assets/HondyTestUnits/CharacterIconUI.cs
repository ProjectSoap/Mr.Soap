using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class CharacterIconUI : MonoBehaviour {
	
	PlayerCharacterController player;
	SpriteSwitcher switcher;
	Image iconOverFrame;
	public int m_characterNum;
	public int m_characterMax = 3;

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

	float m_controlTime;	// 制御用の時間計測変数

	[SerializeField,Header("ダメージを受けた時などにアイコンを変える時間")]
	float m_changingIconTime = 0.5f;
	// キャラの状態その1 こっち最優先に切り替え
	public enum ECharacterState
	{
		NORMAL,
		DAMAGE,
		HEAL,
		WASH_CHAIN,
		DEAD
	}

	// キャラの状態その2 基本こっち
	public enum EHealthState
	{
		COMPLETE_RECOVERY,	// だいたい全快
		MINOR_INJURY,		// 軽傷
		SERIOUS_INJURY		// 重傷

	}

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
				break;
			case ECharacterState.DAMAGE:
				if (m_changingIconTime < m_controlTime)
				{
					CharacterStateExitProcess();
					m_characterState = ECharacterState.NORMAL;
					CharacterStateEnterProcess();
				}
				break;
			case ECharacterState.HEAL:
				break;
			case ECharacterState.WASH_CHAIN:
				break;
			case ECharacterState.DEAD:
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
				break;
			case ECharacterState.DAMAGE:
				break;
			case ECharacterState.HEAL:
				break;
			case ECharacterState.WASH_CHAIN:
				break;
			case ECharacterState.DEAD:
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
				break;
			case ECharacterState.DAMAGE:
				break;
			case ECharacterState.HEAL:
				break;
			case ECharacterState.WASH_CHAIN:
				break;
			case ECharacterState.DEAD:
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
				switcher.SetNumber(0 + m_characterMax * m_characterNum);
				iconOverFrame.color = m_completeRecoveryColor;
				break;
			case EHealthState.MINOR_INJURY:
				switcher.SetNumber(1 + m_characterMax * m_characterNum);
				iconOverFrame.color = m_minorInjuryColor;
				break;
			case EHealthState.SERIOUS_INJURY:
				switcher.SetNumber(2 + m_characterMax * m_characterNum);
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
				break;
			case ECharacterState.WASH_CHAIN:
				break;
			case ECharacterState.DEAD:
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
