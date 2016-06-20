/**********************************************************************************************//**
 * @file    Assets\HondyTestUnits\NorticeUIOfAppearanceRecoverySoap.cs
 *
 * @brief   Implements the nortice user interface of appearance recovery SOAP class.
 **************************************************************************************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

/**********************************************************************************************//**
 * @class   NorticeUIOfAppearanceRecoverySoap
 *
 * @brief   回復石鹸が出たのを知らせるUI.
 *
 * @author  Kazuyuki
 *
 * @sa  UnityEngine.MonoBehaviour
 **************************************************************************************************/

public class NorticeUIOfAppearanceRecoverySoap : MonoBehaviour {

    [SerializeField, TooltipAttribute("スクロール速度")]
    float scrollSpeed;  /* The scroll speed */

    bool isAppearance;  /* せっけん出現フラグ */
    public bool IsAppearance
    {
        get { return isAppearance; }
        set { isAppearance = value; }
    }
    /**********************************************************************************************//**
     * @fn  void Start ()
     *
     * @brief   Use this for initialization.
     *
     * @author  Kazuyuki
     **************************************************************************************************/

    void Start () {
	
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
        if (IsAppearance)
        {
            
            transform.position += new Vector3(-Mathf.Abs(scrollSpeed), 0, 0);
            float width = this.GetComponent<RectTransform>().sizeDelta.x;

            if (transform.position.x + width * 0.5f < 0)
            {
                IsAppearance = false;
                transform.position = new Vector3(width + Screen.width, transform.position.y, transform.position.z); 
            }
        }

	}
}
