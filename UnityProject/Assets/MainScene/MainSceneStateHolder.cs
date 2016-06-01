/**********************************************************************************************//**
 * @file    Assets\MainScene\MainSceneStateHoldr.cs
 *
 * @brief   メイン画面の状態を保持するクラス 数も少ないのでシンプルにswitchで管理.
 **************************************************************************************************/

using UnityEngine;  /* The unity engine */
using System.Collections;

/**********************************************************************************************//**
 * @class   MainSceneStateHoldr
 *
 * @brief   A main scene state holdr.
 *
 * @author  Kazuyuki
 *
 * @sa  UnityEngine.MonoBehaviour
 **************************************************************************************************/

public class MainSceneStateHolder : MonoBehaviour
{
    /**********************************************************************************************//**
     * @enum    State
     *
     * @brief   メインの状態.
     **************************************************************************************************/

    enum State
    {
        START,  /* スタート状態 */
        PLAY,   /* 遊んでる時 */
        PAUSE,  /* ポーズ中 */
        ACTUAL_RESULT,  /* 実績確認中 */
        END /* せっけんくん死んだ */
    }

    [SerializeField]
    State state;    /* The state */
    [SerializeField]
    State stateBeforEnteringPause;    /* ポーズに入る前の状態を記憶する(スタート状態かプレイ状態か)*/
    [SerializeField]
    GameObject player;  /* The player */

    /**********************************************************************************************//**
     * @fn  void Start ()
     *
     * @brief   Use this for initialization.
     *
     * @author  Kazuyuki
     **************************************************************************************************/

    void Start () {
        state = State.START;
    }

    /**********************************************************************************************//**
     * @fn  void Update ()
     *
     * @brief   Update is called once per frame.
     *
     * @author  Kazuyuki
     **************************************************************************************************/

	void Update ()
    {
        UpdateState();
        ExecuteStateMainProcesss();
	}

    /**********************************************************************************************//**
     * @fn  protected void UpdateState()
     *
     * @brief   ステートを更新.
     *
     * @author  Kazuyuki
     **************************************************************************************************/

    protected void UpdateState()
    {
        switch (state)
        {
            case State.START:

                if (Input.GetKey(KeyCode.F3))
                {
                    ExecuteStateExitProcesss(state);
                    Debug.Log("exit start state");
                    Debug.Log("enter pause state");
                    stateBeforEnteringPause = state;
                    state = State.PAUSE;
                    ExecuteStateEnterProcesss(state);
                }
                else if (Input.GetMouseButton(0))
                {
                    ExecuteStateExitProcesss(state);
                    Debug.Log("exit start state");
                    Debug.Log("enter play state");
                    state = State.PLAY;
                    ExecuteStateEnterProcesss(state);
                }
                break;
            case State.PLAY:


                if (Input.GetKey(KeyCode.F3))
                {
                    ExecuteStateExitProcesss(state);
                    stateBeforEnteringPause = state;
                    state = State.PAUSE;
                    Debug.Log("exit play state");
                    Debug.Log("enter pause state");
                    ExecuteStateEnterProcesss(state);
                }
                else if (player.GetComponent<PlayerCharacterController>().size <= 0.0f )
                {
                    ExecuteStateExitProcesss(state);
                    state = State.END;
                    Debug.Log("exit play state");
                    Debug.Log("enter end state");
                    ExecuteStateEnterProcesss(state);
                }
                break;
            case State.PAUSE:
                if (Input.GetMouseButton(1))
                {
                    ExecuteStateExitProcesss(state);
                    state = stateBeforEnteringPause;
                    Debug.Log("exit pause state");
                    Debug.Log("enter play state");
                    ExecuteStateEnterProcesss(state);
                }
                break;
            case State.ACTUAL_RESULT:
                break;
            case State.END:
                
                break;
            default:
                break;
        }
    }

    /**********************************************************************************************//**
     * @fn  void ExecuteStateEnterProcesss(State enterState)
     *
     * @brief   ステートを入る時の処理.
     *
     * @author  Kazuyuki
     *
     * @param   enterState  State of the enter.
     **************************************************************************************************/

    void ExecuteStateEnterProcesss(State enterState)
    {
        switch (enterState)
        {
            case State.START:
                break;
            case State.PLAY:
                break;
            case State.PAUSE:
                break;
            case State.ACTUAL_RESULT:
                break;
            case State.END:
                break;
            default:
                break;
        }
    }

    /**********************************************************************************************//**
     * @fn  protected void ExecuteStateMainProcesss()
     *
     * @brief   そのステートで毎フレーム行う処理.
     *
     * @author  Kazuyuki
     **************************************************************************************************/

    protected void ExecuteStateMainProcesss()
    {
        switch (state)
        {
            case State.START:
                break;
            case State.PLAY:
                break;
            case State.PAUSE:
                break;
            case State.ACTUAL_RESULT:
                break;
            case State.END:
                break;
            default:
                break;
        }
    }

    /**********************************************************************************************//**
     * @fn  void ExecuteStateExitProcesss(State exitState)
     *
     * @brief   ステートを抜ける時の処理.
     *
     * @author  Kazuyuki
     *
     * @param   exitState   State of the exit.
     **************************************************************************************************/

    void ExecuteStateExitProcesss(State exitState)
    {
        switch (exitState)
        {
            case State.START:
                break;
            case State.PLAY:
                break;
            case State.PAUSE:
                break;
            case State.ACTUAL_RESULT:
                break;
            case State.END:
                break;
            default:
                break;
        }
    }
}
