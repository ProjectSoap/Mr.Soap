/**********************************************************************************************//**
 * @file    Assets\MainScene\MainSceneStateHoldr.cs
 *
 * @brief   メイン画面の状態を保持するクラス 数も少ないのでシンプルにswitchで管理.
 **************************************************************************************************/

using UnityEngine;  /* The unity engine */
using System.Collections;
using UnityEngine.UI;

/**********************************************************************************************//**
 * @class   MainSceneStateHolder
 *
 * @brief   A main scene state holdr.
 *
 * @author  Kazuyuki
 *
 * @sa  UnityEngine.MonoBehaviour
 **************************************************************************************************/

///
/// <summary>   A main scene state holder.  </summary>
///
/// <remarks>   Kazuyuki,.  </remarks>
///
/// <seealso cref="T:UnityEngine.MonoBehaviour"/>
///

public class MainSceneStateHolder : MonoBehaviour
{
	/**********************************************************************************************//**
	 * @enum    MainState
	 *
	 * @brief   メインの状態.
	 **************************************************************************************************/

	///
	/// <summary>   Values that represent main states.  </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	enum MainState
	{
		/** <summary>    An enum constant representing the start option. </summary> */

		/// <summary>   An enum constant representing the start option. </summary>
		/** <summary>    An enum constant representing the start option. </summary> */
		START,


		/// <summary>   An enum constant representing the play option.  </summary>
		/** <summary>    An enum constant representing the play option.  </summary> */
		PLAY,   /* An enum constant representing the play option */


		/// <summary>   An enum constant representing the pause option. </summary>
		/** <summary>    An enum constant representing the pause option. </summary> */
		PAUSE,  /* An enum constant representing the pause option */

		/// <summary>   遷移確認.   </summary>
		CHECK_TRANSITION,

		/// <summary>   An enum constant representing the play record option.   </summary>
		/** <summary>    An enum constant representing the play record option.   </summary> */
		PLAY_RECORD,    /* An enum constant representing the play record option */


		/// <summary>   An enum constant representing the end option.   </summary>
		/** <summary>    An enum constant representing the end option.   </summary> */
		END /* An enum constant representing the end option */
	}

	///
	/// <summary>   Values that represent mode states.  </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	public enum ModeState
	{
		/// <summary>   An enum constant representing the normal play option.   </summary>
		/** <summary>    An enum constant representing the normal play option.   </summary> */
		NORMAL_PLAY,    /* An enum constant representing the normal play option */


		/// <summary>   An enum constant representing the free play option. </summary>
		/** <summary>    An enum constant representing the free play option. </summary> */
		FREE_PLAY   /* An enum constant representing the free play option */
	}

	///
	/// <summary>   ポーズ状態の子の状態（カーソルどれ指してるか）.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	public enum SelectMainPauseScene
	{
		/// <summary>   An enum constant representing the select back option.   </summary>
		SELECT_BACK,

		/// <summary>   An enum constant representing the select transition menu option.    </summary>
		SELECT_TRANSITION_MENU,

		/// <summary>   An enum constant representing the select play record option.    </summary>
		SELECT_PLAY_RECORD
	}

	///
	/// <summary>   遷移確認状態の子の状態（カーソルどれ指してるか）.   </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	public enum SelectMainTransitionMenu
	{
		/// <summary>   An enum constant representing the select yes option.    </summary>
		SELECT_YES,

		/// <summary>   An enum constant representing the select no option. </summary>
		SELECT_NO
	}

	///
	/// <summary>   State of the main.  </summary>
	///

	[SerializeField]
	MainState m_mainState;    /* The state */

	///
	/// <summary>   State of the mode.  </summary>
	///

	[SerializeField]
	public ModeState m_modeState;  /* The mode */

	///
	/// <summary>   The select icon in pause.   </summary>
	///

	public SelectMainPauseScene m_selectIconInPause;

	///
	/// <summary>   The select icon in transition menu. </summary>
	///

	public SelectMainTransitionMenu m_selectIconInTransitionMenu;

	///
	/// <summary>   The before main stete.  </summary>
	///

	[SerializeField]
	MainState m_beforeMainStete;

	///
	/// <summary>   ポーズに入る前の状態. </summary>
	///

	[SerializeField]
	MainState m_beforeEnterPauseMainStete;

	///
	/// <summary>   The player. </summary>
	///

	[SerializeField]
	GameObject player;  /* The player */

	///
	/// <summary>   The dirty system.   </summary>
	///

	DirtySystem dirtySystem;    /* The dirty system */

	///
	/// <summary>   The dirty system object.    </summary>
	///

	GameObject m_dirtySystemObject; /* The dirty system object */

	///
	/// <summary>   Manager for recovery SOAP creaters. </summary>
	///

	RecoverySoapCreatersManager recoverySoapCreatersManager;    /* Manager for recovery SOAP creaters */

	///
	/// <summary>   決定キーを押してね.  </summary>
	///

	GameObject pushKey;

	///
	/// <summary>   The pose menu.  </summary>
	///

	PoseMenu poseMenu;  /* ポーズ中のメニュー管理 */
	

	///
	/// <summary>   The game play user interface.   </summary>
	///

	[SerializeField]
	PauseObject m_gamePlayUI;   /* ゲームプレイ時のUI */
	

	///
	/// <summary>   ポーズ中停止させるオブジェクト.    </summary>
	///

	[SerializeField]
	PauseObject m_pauseObjects;

	///
	/// <summary>   ポーズオブジェクトとしての実績画面.  </summary>
	///

	public PauseObject m_playRecordAsPauseObject;

	///
	/// <summary>   遷移確認画面. </summary>
	///

	public PauseObject m_checkTransitionMenuScreeenUI;

	///
	/// <summary>   ポーズ画面.  </summary>
	///

	public PauseObject m_pauseScreenUI;

	///
	/// <summary>   Use this for initialization.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	void Start()
	{
		m_mainState = MainState.START;//スタートから開始
		player = GameObject.Find("PlayerCharacter");
		m_dirtySystemObject = GameObject.Find("DirtySystem");
		dirtySystem = m_dirtySystemObject.GetComponent<DirtySystem>();
		recoverySoapCreatersManager = GameObject.Find("RecoverySoapCreatersManager").GetComponent<RecoverySoapCreatersManager>();
		pushKey = GameObject.Find("PushKey");
		poseMenu = GameObject.Find("PauseCur").GetComponent<PoseMenu>();
		
		m_playRecordAsPauseObject.pausing = true;

		ActionRecordManager.sActionRecord.Reset();

		if (m_modeState == ModeState.FREE_PLAY)
		{
		}
		ExecuteStateEnterProcesss(m_mainState);
		BGMManager.Instance.PlayBGM("GameMain_BGM",0);

	}


	/**
	* <summary>Updates this object.</summary>
	*
	* <remarks>Kazuyuki,.</remarks>
	**/

	///
	/// <summary>   Updates this object.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	void Update()
	{
		UpdateState();
		ExecuteStateMainProcesss();
	}

	///
	/// <summary>   ステートを更新.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	protected void UpdateState()
	{
		switch (m_mainState)
		{
			case MainState.START:

				// ヘルプ(F12)押したらポーズに遷移
				if (Input.GetKeyDown(KeyCode.F12))
				{
					ExecuteStateExitProcesss(m_mainState);
					if (m_modeState == ModeState.NORMAL_PLAY)
					{
						m_beforeMainStete = m_mainState;
						m_beforeEnterPauseMainStete = m_mainState;
						m_mainState = MainState.PAUSE;

					}
					else
					{
						m_beforeMainStete = m_mainState;
						m_beforeEnterPauseMainStete = m_mainState;
						m_mainState = MainState.CHECK_TRANSITION;
					}
					ExecuteStateEnterProcesss(m_mainState);
				}
				// 決定キー押されたらプレイ状態
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
				{
					ExecuteStateExitProcesss(m_mainState);
					m_beforeMainStete = m_mainState;
					m_mainState = MainState.PLAY;
					ExecuteStateEnterProcesss(m_mainState);
				}
				break;
			case MainState.PLAY:


				// ヘルプ(F12)押したらポーズに遷移
				if (Input.GetKeyDown(KeyCode.F12))
				{
					ExecuteStateExitProcesss(m_mainState);
					if (m_modeState == ModeState.NORMAL_PLAY)
					{
						m_beforeMainStete = m_mainState;
						m_beforeEnterPauseMainStete = m_mainState;
						m_mainState = MainState.PAUSE;

					}
					else
					{
						m_beforeMainStete = m_mainState;
						m_beforeEnterPauseMainStete = m_mainState;
						m_mainState = MainState.CHECK_TRANSITION;
					}
					ExecuteStateEnterProcesss(m_mainState);
				}
				// プレイヤーが死んだらエンド状態
				else if (player.GetComponent<PlayerCharacterController>().size <= 0.0f)
				{
					ExecuteStateExitProcesss(m_mainState);
					m_beforeMainStete = m_mainState;
					m_mainState = MainState.END;
					ExecuteStateEnterProcesss(m_mainState);
				}
				break;
			case MainState.PAUSE:
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
				{
					if (m_selectIconInPause == SelectMainPauseScene.SELECT_BACK)
					{
						ExecuteStateExitProcesss(m_mainState);
						MainState temp = m_beforeEnterPauseMainStete;//前の状態に戻る
						m_beforeMainStete = m_mainState;
						m_mainState = temp;
						ExecuteStateEnterProcesss(m_mainState);
					}
					else if (m_selectIconInPause == SelectMainPauseScene.SELECT_TRANSITION_MENU)
					{
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.CHECK_TRANSITION;
						ExecuteStateEnterProcesss(m_mainState);
					}
					else if (m_selectIconInPause == SelectMainPauseScene.SELECT_PLAY_RECORD)
					{
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.PLAY_RECORD;
						ExecuteStateEnterProcesss(m_mainState);
					}
				}
				break;
			case MainState.CHECK_TRANSITION:

				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
				{
					if (m_selectIconInTransitionMenu == SelectMainTransitionMenu.SELECT_NO)
					{
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.PAUSE;
						ExecuteStateEnterProcesss(m_mainState);
					}
				}
				break;
			case MainState.PLAY_RECORD:
				if (Input.GetKeyDown(KeyCode.Escape))
				{
					ExecuteStateExitProcesss(m_mainState);
					m_beforeMainStete = m_mainState;
					m_mainState = MainState.PAUSE;
					ExecuteStateEnterProcesss(m_mainState);
				}
				break;
			case MainState.END:

				break;
			default:
				break;
		}
	}

	///
	/// <summary>   ステートを入る時の処理.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///
	/// <param name="enterState">   メインの状態. </param>
	///

	void ExecuteStateEnterProcesss(MainState enterState)
	{
		switch (enterState)
		{
			case MainState.START:
				// スタート状態に入った
				switch (m_beforeMainStete)
				{
					case MainState.START:
						break;
					case MainState.PLAY:
						break;
					case MainState.PAUSE:
						// 前の状態はポーズ
						m_gamePlayUI.PushPose();
						m_pauseObjects.PushPose();
						m_pauseScreenUI.pausing = true;
						break;
					case MainState.PLAY_RECORD:
						break;
					case MainState.END:
						break;
					default:
						break;

				}
				break;
			case MainState.PLAY:
				// プレイ状態に入った
				switch (m_beforeMainStete)
				{
					case MainState.START:
						// 
						GameObject.Destroy(pushKey);
						break;
					case MainState.PLAY:
						break;
					case MainState.PAUSE:
						m_gamePlayUI.PushPose();
						m_pauseObjects.PushPose();
						m_pauseScreenUI.pausing = true;
						break;
					case MainState.PLAY_RECORD:
						break;
					case MainState.CHECK_TRANSITION:
						m_gamePlayUI.PushPose();
						m_pauseObjects.PushPose();
						m_checkTransitionMenuScreeenUI.pausing = true;
						break;
					case MainState.END:
						break;
					default:
						break;

				}
				//  pauseObject.pausing = false;
				break;
			case MainState.PAUSE:
				// ポーズに入る前の状態で処理わけ
				switch (m_beforeMainStete)
				{
					case MainState.START:
						m_gamePlayUI.pausing = true;
						m_pauseObjects.pausing = true;
						m_pauseScreenUI.pausing = false;
						break;
					case MainState.PLAY:
						m_gamePlayUI.pausing = true;
						m_pauseObjects.pausing = true;
						m_pauseScreenUI.pausing = false;
						break;
					case MainState.PLAY_RECORD:

						break;
					case MainState.END:
						break;
					default:
						break;

				}
				break;
			case MainState.CHECK_TRANSITION:
				// 前状態がスタートもしくはプレイ（＝フリーモード）
				if (m_beforeMainStete == MainState.START || m_beforeMainStete == MainState.PLAY)
				{
					
					m_gamePlayUI.pausing = true;
					m_pauseObjects.pausing = true;
				}
				m_checkTransitionMenuScreeenUI.pausing = false;
				m_pauseScreenUI.pausing = true;
				break;
			case MainState.PLAY_RECORD:
				m_pauseScreenUI.PushPose();
				m_playRecordAsPauseObject.PushPose();
				break;
			case MainState.END:
				break;
			default:
				break;
		}
	}

	///
	/// <summary>   そのステートで毎フレーム行う処理.   </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	protected void ExecuteStateMainProcesss()
	{
		switch (m_mainState)
		{
			case MainState.START:
				break;
			case MainState.PLAY:
				break;
			case MainState.PAUSE:
				//子の状態管理すんのめんどい
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					m_selectIconInPause = (SelectMainPauseScene)((int)(++m_selectIconInPause)%3);
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					m_selectIconInPause = (SelectMainPauseScene)((int)(--m_selectIconInPause ) % 3);
				}
				break;
			case MainState.CHECK_TRANSITION:
				//子の状態管理すんのめんどい
				if (Input.GetKeyDown(KeyCode.RightArrow))
				{
					m_selectIconInTransitionMenu = (SelectMainTransitionMenu)((int)(++m_selectIconInTransitionMenu) % 2);
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					m_selectIconInTransitionMenu = (SelectMainTransitionMenu)((int)(--m_selectIconInTransitionMenu) % 2);
				}
				break;
			case MainState.PLAY_RECORD:
				break;
			case MainState.END:
				break;
			default:
				break;
		}
	}

	///
	/// <summary>   ステートを抜ける時の処理.   </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///
	/// <param name="exitState">    State of the exit.  </param>
	///

	void ExecuteStateExitProcesss(MainState exitState)
	{
		switch (exitState)
		{
			case MainState.START:
				break;
			case MainState.PLAY:
				dirtySystem.IsRunning = false;
				recoverySoapCreatersManager.IsRunning = false;
				break;
			case MainState.PAUSE:
				break;
			case MainState.PLAY_RECORD:
				m_pauseScreenUI.PushPose();
				m_playRecordAsPauseObject.PushPose();
				break;
			case MainState.CHECK_TRANSITION:
				m_checkTransitionMenuScreeenUI.PushPose();
				m_pauseScreenUI.PushPose();
				break;
			case MainState.END:
				break;
			default:
				break;
		}
	}
}
