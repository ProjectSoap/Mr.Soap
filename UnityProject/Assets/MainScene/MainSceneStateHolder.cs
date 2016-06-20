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
	/// <summary>   メインの状態.  </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	enum MainState
	{
		/// <summary>   けっていきー待ち. </summary>
		START,


		/// <summary>   遊んでる.  </summary>
		PLAY,  

		

		/// <summary>   ポーズ画面. </summary>
		PAUSE, 

		/// <summary>   遷移確認画面.   </summary>
		CHECK_TRANSITION,


		/// <summary>   実績確認画面.   </summary>
		PLAY_RECORD,  

		

		/// <summary>   死亡時.   </summary>
		END 
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


<<<<<<< HEAD
=======
	// ゲームプレイ中のUI
	

	///
	/// <summary>   The size icon user interface.   </summary>
	///

>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
	public PauseObject m_sizeIconUI;
	public PauseObject m_sizeCounterUI;
	public PauseObject m_dirtyCounterUI;
	public PauseObject m_miniMapUI;
<<<<<<< HEAD
=======
	public PauseObject m_washChainUI;


	public PauseObject m_norticeRecoveryUI;
	public PauseObject m_pushKeyUI;


>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80


	public PauseObject m_dirtys;


	public PauseObject m_endUI;

	///
	/// <summary>   Use this for initialization.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	public PauseObject m_pauseSystems;

	void Start()
	{
<<<<<<< HEAD
=======
		
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
		m_mainState = MainState.START;//スタートから開始
		player = GameObject.Find("PlayerCharacter");
		m_dirtySystemObject = GameObject.Find("DirtySystem");
		dirtySystem = m_dirtySystemObject.GetComponent<DirtySystem>();
		recoverySoapCreatersManager = GameObject.Find("RecoverySoapCreatersManager").GetComponent<RecoverySoapCreatersManager>();
		pushKey = GameObject.Find("PushKey");
		poseMenu = GameObject.Find("PauseCur").GetComponent<PoseMenu>();
		
		m_playRecordAsPauseObject.pausing = true;
<<<<<<< HEAD
		m_dirtys = GameObject.Find("Dirtys").GetComponent<PauseObject>();
		ActionRecordManager.sActionRecord.Reset();

		if (m_modeState == ModeState.FREE_PLAY)
		{
		}
=======
		m_endUI = GameObject.Find("EndUI").GetComponent<PauseObject>();
		m_dirtys = GameObject.Find("Dirtys").GetComponent<PauseObject>();
		ActionRecordManager.sActionRecord.Reset();

		//モード確認
		SelectingCharactor no = GameObject.Find("CharNo").GetComponent<SelectingCharactor>();
		if (no != null)
		{
			if (no.PlayMode == PlayModeState.NORMAL)
			{
				m_modeState = ModeState.NORMAL_PLAY;
				m_pauseSystems.pausing = true;
			}
			else
			{

				m_modeState = ModeState.FREE_PLAY;
				m_sizeCounterUI.pausing = true;
				m_dirtyCounterUI.pausing = true;
				m_washChainUI.pausing = true;
				m_pauseSystems.pausing = true;
				m_dirtys.gameObject.SetActive(false);

			}
		}
		no.loaded = true;

		m_pauseSystems.pausing = true;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
		if (m_modeState == ModeState.FREE_PLAY)
		{
			player.GetComponent<PlayerCharacterController>().size = 100.9f;
		}
	}

	///
	/// <summary>   ステートを更新.    </summary>
	///
	/// <remarks>   Kazuyuki,.  </remarks>
	///

	protected void UpdateState()
	{
<<<<<<< HEAD
=======
		
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
		switch (m_mainState)
		{
			case MainState.START:

				// ヘルプ(F12)押したらポーズに遷移
				if (Input.GetKeyDown(KeyCode.F12) && Fade.FadeEnd())
				{
<<<<<<< HEAD
=======
                    BGMManager.Instance.PlaySE("Game_Pause");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
<<<<<<< HEAD
=======
                    BGMManager.Instance.PlaySE("Game_Start");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
				}
				break;
			case MainState.PLAY:

<<<<<<< HEAD

=======
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
<<<<<<< HEAD
=======
                    BGMManager.Instance.PlaySE("Game_Pause");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
				}
				// プレイヤーが死んだらエンド状態
				else if (player.GetComponent<PlayerCharacterController>().size <= 0.0f)
				{
					ExecuteStateExitProcesss(m_mainState);
					m_beforeMainStete = m_mainState;
					m_mainState = MainState.END;
					ExecuteStateEnterProcesss(m_mainState);
<<<<<<< HEAD
=======
					BGMManager.Instance.PlaySE("Game_Set");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
				}
				break;
			case MainState.PAUSE:
				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
				{
<<<<<<< HEAD
=======
                    
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
					if (m_selectIconInPause == SelectMainPauseScene.SELECT_BACK)
					{
						ExecuteStateExitProcesss(m_mainState);
						MainState temp = m_beforeEnterPauseMainStete;//前の状態に戻る
						m_beforeMainStete = m_mainState;
						m_mainState = temp;
						ExecuteStateEnterProcesss(m_mainState);
<<<<<<< HEAD
=======
                        BGMManager.Instance.PlaySE("Cursor_Cancel");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
					}
					else if (m_selectIconInPause == SelectMainPauseScene.SELECT_TRANSITION_MENU)
					{
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.CHECK_TRANSITION;
						ExecuteStateEnterProcesss(m_mainState);
<<<<<<< HEAD
=======
                        BGMManager.Instance.PlaySE("Cursor_Decision");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
					}
					else if (m_selectIconInPause == SelectMainPauseScene.SELECT_PLAY_RECORD)
					{
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.PLAY_RECORD;
						ExecuteStateEnterProcesss(m_mainState);
<<<<<<< HEAD
=======
                        BGMManager.Instance.PlaySE("Cursor_Decision");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
					}
				}
				break;
			case MainState.CHECK_TRANSITION:

				if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
				{
					if (m_selectIconInTransitionMenu == SelectMainTransitionMenu.SELECT_NO)
					{
<<<<<<< HEAD
=======
                        BGMManager.Instance.PlaySE("Cursor_Cancel");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
						ExecuteStateExitProcesss(m_mainState);
						m_beforeMainStete = m_mainState;
						m_mainState = MainState.PAUSE;
						if (m_modeState == ModeState.FREE_PLAY)
						{
							MainState temp = m_beforeEnterPauseMainStete;//前の状態に戻る
							m_beforeMainStete = m_mainState;
							m_mainState = temp;
						}
						ExecuteStateEnterProcesss(m_mainState);

					}
					if (m_selectIconInTransitionMenu == SelectMainTransitionMenu.SELECT_YES)
					{
<<<<<<< HEAD
=======
                        BGMManager.Instance.PlaySE("Cursor_Decision");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
						Fade.ChangeScene("Menu");
					}
				}
				break;
			case MainState.PLAY_RECORD:
				if (Input.GetKeyDown(KeyCode.Escape))
				{
<<<<<<< HEAD
=======
                    BGMManager.Instance.PlaySE("Cursor_Cancel");
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
						m_pauseSystems.pausing = true;
						break;
					case MainState.PLAY:
						m_pauseSystems.pausing = true;
						break;
					case MainState.PAUSE:
						// 前の状態はポーズ
<<<<<<< HEAD
						m_gamePlayUI.pausing = false;
=======
						m_sizeIconUI.pausing = false;
						m_sizeCounterUI.pausing = false;
						m_dirtyCounterUI.pausing = false;
						m_washChainUI.pausing = false;
						m_miniMapUI.pausing = false;

						m_norticeRecoveryUI.pausing = false;
						m_pushKeyUI.pausing = false;

						//フリープレイ時オフ
						if (m_modeState == ModeState.FREE_PLAY)
						{
							m_sizeCounterUI.pausing = true;
							m_dirtyCounterUI.pausing = true;
							m_washChainUI.pausing = true;
							m_pauseSystems.pausing = true;
						}

>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
						m_pauseObjects.pausing = false;
						m_pauseScreenUI.pausing = true;
						m_pauseSystems.pausing = true;
						break;
					case MainState.PLAY_RECORD:
						m_pauseSystems.pausing = true;
						break;
					case MainState.END:
						m_pauseSystems.pausing = true;
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
<<<<<<< HEAD
						GameObject.Destroy(pushKey);
						m_gamePlayUI.pausing = false;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						m_dirtys.pausing = false;

						m_pauseScreenUI.pausing = true;
=======
						m_sizeIconUI.pausing = false;
						m_sizeCounterUI.pausing = false;
						m_dirtyCounterUI.pausing = false;
						m_washChainUI.pausing = false;
						m_miniMapUI.pausing = false;

						m_norticeRecoveryUI.pausing = false;
						m_pushKeyUI.pausing = true;


						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						if (m_dirtys)
						{
							m_dirtys.pausing = false;
						}

						m_pauseScreenUI.pausing = true;
						//フリープレイ時オフ
						if (m_modeState == ModeState.FREE_PLAY)
						{
							m_sizeCounterUI.pausing = true;
							m_dirtyCounterUI.pausing = true;
							m_washChainUI.pausing = true;
							m_pauseSystems.pausing = true;
						}
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
						break;
					case MainState.PLAY:
						break;
					case MainState.PAUSE:
<<<<<<< HEAD
						m_gamePlayUI.pausing = false;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						m_dirtys.pausing = false;

						m_pauseScreenUI.pausing = true;
						
						break;
					case MainState.PLAY_RECORD:
						m_gamePlayUI.pausing = false;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						m_dirtys.pausing = false;

						m_pauseScreenUI.pausing = true;
						break;
					case MainState.CHECK_TRANSITION:
						m_gamePlayUI.pausing = false;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						m_dirtys.pausing = false;

						m_pauseScreenUI.pausing = true;
=======
						m_sizeIconUI.pausing = false;
						m_sizeCounterUI.pausing = false;
						m_dirtyCounterUI.pausing = false;
						m_washChainUI.pausing = false;
						m_miniMapUI.pausing = false;

						m_norticeRecoveryUI.pausing = false;
						m_pushKeyUI.pausing = true;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;

						m_pauseScreenUI.pausing = true;
						m_dirtys.pausing = false;

						//フリープレイ時オフ
						if (m_modeState == ModeState.FREE_PLAY)
						{
							m_sizeCounterUI.pausing = true;
							m_dirtyCounterUI.pausing = true;
							m_washChainUI.pausing = true;
							m_pauseSystems.pausing = true;
						}
						break;
					case MainState.PLAY_RECORD:
						m_sizeIconUI.pausing = false;
						m_sizeCounterUI.pausing = false;
						m_dirtyCounterUI.pausing = false;
						m_washChainUI.pausing = false;
						m_miniMapUI.pausing = false;

						m_norticeRecoveryUI.pausing = false;
						m_pushKeyUI.pausing = true;
						
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						if (m_dirtys)
						{
							m_dirtys.pausing = true;
						}

						m_pauseScreenUI.pausing = true;

						//フリープレイ時オフ
						if (m_modeState == ModeState.FREE_PLAY)
						{
							m_sizeCounterUI.pausing = true;
							m_dirtyCounterUI.pausing = true;
							m_washChainUI.pausing = true;
							m_pauseSystems.pausing = true;
						}
						break;
					case MainState.CHECK_TRANSITION:
						m_sizeIconUI.pausing = false;
						m_sizeCounterUI.pausing = false;
						m_dirtyCounterUI.pausing = false;
						m_washChainUI.pausing = false;
						m_miniMapUI.pausing = false;

						m_norticeRecoveryUI.pausing = false;
						m_pushKeyUI.pausing = true;
						m_pauseObjects.pausing = false;
						m_pauseSystems.pausing = false;
						if (m_dirtys)
						{
							m_dirtys.pausing = true;
						}

						m_pauseScreenUI.pausing = true;

						//フリープレイ時オフ
						if (m_modeState == ModeState.FREE_PLAY)
						{
							m_sizeCounterUI.pausing = true;
							m_dirtyCounterUI.pausing = true;
							m_washChainUI.pausing = true;
							m_pauseSystems.pausing = true;
						}
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
						break;
					case MainState.END:
						
						break;
					default:
						break;

				}

				if (m_modeState == ModeState.FREE_PLAY)
				{
					m_pauseSystems.pausing = true;
				}
				//  pauseObject.pausing = false;
				break;
			case MainState.PAUSE:
				// ポーズに入る前の状態で処理わけ
				switch (m_beforeMainStete)
				{
					case MainState.START:
<<<<<<< HEAD
						m_gamePlayUI.pausing = true;
						m_pauseObjects.pausing = true;
						m_pauseSystems.pausing = true;
						m_dirtys.pausing = true;
=======
						m_sizeIconUI.pausing = true;
						m_sizeCounterUI.pausing = true;
						m_dirtyCounterUI.pausing = true;
						m_washChainUI.pausing = true;
						m_miniMapUI.pausing = true;

						m_norticeRecoveryUI.pausing = true;
						m_pushKeyUI.pausing = true;
						
						m_pauseObjects.pausing = true;
						m_pauseSystems.pausing = true;
						if (m_dirtys)
						{
							m_dirtys.pausing = true;
						} 
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

						m_pauseScreenUI.pausing = false;
						break;
					case MainState.PLAY:
<<<<<<< HEAD
						m_gamePlayUI.pausing = true;
						m_pauseObjects.pausing = true;
						m_pauseSystems.pausing = true;
						m_dirtys.pausing = true;
=======
						m_sizeIconUI.pausing = true;
						m_sizeCounterUI.pausing = true;
						m_dirtyCounterUI.pausing = true;
						m_washChainUI.pausing = true;
						m_miniMapUI.pausing = true;

						m_norticeRecoveryUI.pausing = true;
						m_pushKeyUI.pausing = true;

						m_pauseObjects.pausing = true;
						m_pauseSystems.pausing = true;
						if (m_dirtys)
						{
							m_dirtys.pausing = true;
						}
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80

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
				if (m_modeState == ModeState.FREE_PLAY)
				{
<<<<<<< HEAD
					m_gamePlayUI.pausing = true;
=======
					m_sizeIconUI.pausing = true;
					m_sizeCounterUI.pausing = true;
					m_dirtyCounterUI.pausing = true;
					m_washChainUI.pausing = true;
					m_miniMapUI.pausing = true;

					m_norticeRecoveryUI.pausing = true;
					m_pushKeyUI.pausing = true;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
<<<<<<< HEAD
=======
				m_endUI.pausing = false;
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
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
					m_selectIconInPause = (SelectMainPauseScene)((int)(m_selectIconInPause + 1)%3);
				}
				if (Input.GetKeyDown(KeyCode.LeftArrow))
				{
					m_selectIconInPause = (SelectMainPauseScene)((int)(m_selectIconInPause-1 ) % 3);
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
