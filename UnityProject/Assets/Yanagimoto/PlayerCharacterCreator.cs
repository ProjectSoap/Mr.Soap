using UnityEngine;
using System.Collections;

public class PlayerCharacterCreator : MonoBehaviour
{
    [SerializeField, Tooltip("せっけんくん")]
    GameObject m_sekkenKun;

    [SerializeField, Tooltip("せっけんちゃん")]
    GameObject m_sekkenChan;

    [SerializeField, Tooltip("せっけんキング")]
    GameObject m_sekkenKing;

    void Awake()
    {
        GameObject gameObject = null;
        Transform parent = GameObject.Find("PauseObject").transform;

        switch (SceneData.characterSelect)
        {
            case SceneData.CharacterSelect.SekkenKun:
                gameObject = Instantiate(m_sekkenKun, transform.position, transform.rotation) as GameObject;
                break;
            case SceneData.CharacterSelect.SekkenChan:
                gameObject = Instantiate(m_sekkenChan, transform.position, transform.rotation) as GameObject;
                break;
            case SceneData.CharacterSelect.SekkenKing:
                gameObject = Instantiate(m_sekkenKing, transform.position, transform.rotation) as GameObject;
                break;
            default:
                break;
        }

        if(gameObject != null)
        {
            gameObject.transform.parent = parent;
            gameObject.name = "PlayerCharacter";
            gameObject.SetActive(true);
        }
    }
}
