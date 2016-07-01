using UnityEngine;
using System.Collections;


// シーン間データをやり取りするクラス
public class SceneData : MonoBehaviour {

    public enum CharacterSelect
    {
        SekkenKun,
        SekkenHero,
        SekkenChan,
    }

    [SerializeField, Tooltip("どのキャラクターを選択したか")]
    public static CharacterSelect characterSelect = CharacterSelect.SekkenKun;
    [SerializeField, Tooltip("どのモードを選択したか")]
    public static PlayModeState modeSelect = PlayModeState.NORMAL;

}
