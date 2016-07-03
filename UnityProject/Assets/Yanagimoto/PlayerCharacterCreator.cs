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
        GameObject  gameObject  = null;
        Transform   parent      = GameObject.Find("PauseObject").transform;

        //if(ActionRecordManager.sActionRecord.isSekkenChanPlay)
        //    gameObject = Instantiate(m_sekkenChan, transform.position, transform.rotation) as GameObject;
        //else if(ActionRecordManager.sActionRecord.isSekkenKun0Play)
        //    gameObject = Instantiate(m_sekkenHero, transform.position, transform.rotation) as GameObject;
        //else
        //    gameObject = Instantiate(m_sekkenKun, transform.position, transform.rotation) as GameObject;


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

        //GameObject obj = GameObject.Find("CharNo");

        //if (obj == null)
        //    Debug.LogError("CharNo not found");

        //SelectingCharactor selectingCharacter = obj.GetComponent<SelectingCharactor>();

        //switch (selectingCharacter.GetCharNo())
        //{   
        //    case SelectingCharactorNo.SOAP:
        //        gameObject = Instantiate(m_sekkenKun, transform.position, transform.rotation) as GameObject;
        //        break;
        //    case SelectingCharactorNo.SOAP0:
        //        gameObject = Instantiate(m_sekkenChan, transform.position, transform.rotation) as GameObject;
        //        break;
        //    case SelectingCharactorNo.SOAPTYAN:
        //        gameObject = Instantiate(m_sekkenHero, transform.position, transform.rotation) as GameObject;
        //        break;
        //    default:
        //        break;
        //}

        if (gameObject != null)
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
