using UnityEngine;
using System.Collections;

public class PlayerCharacterCreator : MonoBehaviour
{
    [SerializeField, Tooltip("せっけんくん")]
    GameObject m_sekkenKun;

    [SerializeField, Tooltip("せっけんちゃん")]
    GameObject m_sekkenChan;

    [SerializeField, Tooltip("せっけんヒーロー")]
    GameObject m_sekkenHero;

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
            case SceneData.CharacterSelect.SekkenHero:
                gameObject = Instantiate(m_sekkenHero, transform.position, transform.rotation) as GameObject;
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
        else
        {
            Debug.LogError("fuck!!!! playercharacter not create!!!!!");
        }
    }
}
