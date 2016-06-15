﻿using UnityEngine;
using System.Collections;


// シーン間データをやり取りするクラス
public class SceneData : MonoBehaviour {

    public enum CharacterSelect
    {
        SekkenKun,
        SekkenChan,
        SekkenKing,
    }

    [SerializeField, Tooltip("どのキャラクターを選択したか")]
    public static CharacterSelect characterSelect = CharacterSelect.SekkenKun;

}
