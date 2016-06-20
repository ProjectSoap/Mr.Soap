using UnityEngine;
using System.Collections;


// シーン間データをやり取りするクラス
public class SceneData : MonoBehaviour {

    public enum CharacterSelect
    {
        SekkenKun,
        SekkenChan,
<<<<<<< HEAD
        SekkenKing,
=======
        SekkenHero,
>>>>>>> 5e03151d84bbdbae28a1986085c13fbe5f72fb80
    }

    [SerializeField, Tooltip("どのキャラクターを選択したか")]
    public static CharacterSelect characterSelect = CharacterSelect.SekkenKun;

}
