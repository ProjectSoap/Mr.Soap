/**
// <copyright file="DirtySystem.cs" company="MyCompany.com">
// Copyright (c) 2016 MyCompany.com. All rights reserved.
// </copyright>
// <author>Kazuyuki</author>
// <date>2016/06/14</date>
// <summary>Implements the dirty system class</summary>
 */

using UnityEngine;
using System.Collections;

/**
 * <summary>A dirty system.</summary>
 *
 * <remarks>Kazuyuki,.</remarks>
 *
 * <seealso cref="T:UnityEngine.MonoBehaviour"/>
 */

public class DirtySystem : MonoBehaviour
{
    /**
     * <summary>稼働してるか.</summary>
     */

    bool isRunning;

    /**
     * <summary>Gets or sets a value indicating whether this object is running.</summary>
     *
     * <value>true if this object is running, false if not.</value>
     */

    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }

    /**
     * <summary>プレイヤー位置を知るためのもの.</summary>
     */

    GameObject player;

    [Header("DirtyCreaterPointオブジェクトを入れる場所")]

    /**
     * <summary>区画1の汚れ場所.</summary>
     */

    [SerializeField,Tooltip("区画1の生成ポイント")]
    GameObject[] dirtyPointsOfArea1;

    /**
     * <summary>区画2の汚れ場所.</summary>
     */

    [SerializeField, Tooltip("区画2の生成ポイント")]
    GameObject[] dirtyPointsOfArea2;

    /**
     * <summary>区画3の汚れ場所.</summary>
     */

    [SerializeField, Tooltip("区画3の生成ポイント")]
    GameObject[] dirtyPointsOfArea3;

    /**
     * <summary>区画4の汚れ場所.</summary>
     */

    [SerializeField, Tooltip("区画4の生成ポイント")]
    GameObject[] dirtyPointsOfArea4;

    /**
     * <summary>区画1のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画1のレア汚れ生成ポイント")]
    GameObject RealityPoint1;

    /**
     * <summary>区画2のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画2のレア汚れ生成ポイント")]
    GameObject RealityPoint2;

    /**
     * <summary>区画3のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画3のレア汚れ生成ポイント")]
    GameObject RealityPoint3;

    /**
     * <summary>区画4のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画4のレア汚れ生成ポイント")]
    GameObject RealityPoint4;

    /**
     * <summary>Number of total destroy dirties.</summary>
     */

    [Header("以下確認用内部変数")]
    [SerializeField]
    int totalDestroyDirtyCount;

    /**
     * <summary>区画1で汚れを落とした数.</summary>
     */

    [SerializeField]
    int destroyDirtyCount1;

    /**
     * <summary>The wash chain.</summary>
     */

    WashChain washChain;

    /**
     * <summary>Gets or sets the destroy dirty count 1.</summary>
     *
     * <value>The destroy dirty count 1.</value>
     */

    public int DestroyDirtyCount1
    {
        get { return destroyDirtyCount1; }
        set { destroyDirtyCount1 = value; }
    }

    /**
     * <summary>区画2で汚れを落とした数.</summary>
     */

    [SerializeField]
    int destroyDirtyCount2;

    /**
     * <summary>Gets or sets the destroy dirty count 2.</summary>
     *
     * <value>The destroy dirty count 2.</value>
     */

    public int DestroyDirtyCount2
    {
        get { return destroyDirtyCount2; }
        set { destroyDirtyCount2 = value; }
    }

    /**
     * <summary>区画3で汚れを落とした数.</summary>
     */

    [SerializeField]
    int destroyDirtyCount3;

    /**
     * <summary>Gets or sets the destroy dirty count 3.</summary>
     *
     * <value>The destroy dirty count 3.</value>
     */

    public int DestroyDirtyCount3
    {
        get { return destroyDirtyCount3; }
        set { destroyDirtyCount3 = value; }
    }

    /**
     * <summary>区画4で汚れを落とした数.</summary>
     */

    [SerializeField]
    int destroyDirtyCount4;

    /**
     * <summary>Gets or sets the destroy dirty count 4.</summary>
     *
     * <value>The destroy dirty count 4.</value>
     */

    public int DestroyDirtyCount4
    {
        get { return destroyDirtyCount4; }
        set { destroyDirtyCount4 = value; }
    }

    /**
     * <summary>今回のプレイで区画1レア汚れを落としたか.</summary>
     */

    [SerializeField]
    bool isDestroyRealityDirty1;

    /**
     * <summary>    Gets or sets a value indicating whether this object is destroy reality dirty
     *  1.</summary>
     *
     * <value>true if this object is destroy reality dirty 1, false if not.</value>
     */

    public bool IsDestroyRealityDirty1
    {
        get { return isDestroyRealityDirty1; }
        set { isDestroyRealityDirty1 = value; }
    }

    /**
     * <summary>今回のプレイで区画2レア汚れを落としたか.</summary>
     */

    [SerializeField]
    bool isDestroyRealityDirty2;

    /**
     * <summary>    Gets or sets a value indicating whether this object is destroy reality dirty
     *  2.</summary>
     *
     * <value>true if this object is destroy reality dirty 2, false if not.</value>
     */

    public bool IsDestroyRealityDirty2
    {
        get { return isDestroyRealityDirty2; }
        set { isDestroyRealityDirty2 = value; }
    }

    /**
     * <summary>今回のプレイで区画3レア汚れを落としたか.</summary>
     */

    [SerializeField]
    bool isDestroyRealityDirty3;

    /**
     * <summary>    Gets or sets a value indicating whether this object is destroy reality dirty
     *  3.</summary>
     *
     * <value>true if this object is destroy reality dirty 3, false if not.</value>
     */

    public bool IsDestroyRealityDirty3
    {
        get { return isDestroyRealityDirty3; }
        set { isDestroyRealityDirty3 = value; }
    }

    /**
     * <summary>今回のプレイで区画4レア汚れを落としたか.</summary>
     */

    [SerializeField]
    bool isDestroyRealityDirty4;

    /**
     * <summary>    Gets or sets a value indicating whether this object is destroy reality dirty
     *  4.</summary>
     *
     * <value>true if this object is destroy reality dirty 4, false if not.</value>
     */

    public bool IsDestroyRealityDirty4
    {
        get { return isDestroyRealityDirty4; }
        set { isDestroyRealityDirty4 = value; }
    }

    /**
     * <summary>カウンタースクリプトを持つオブジェクト.</summary>
     */

    GameObject dirtyCounterObject;

    /**
     * <summary>Use this for initialization.</summary>
     *
     * <remarks>Kazuyuki,.</remarks>
     */

    void Start ()
    {

        washChain = GameObject.Find("WashChain").GetComponent<WashChain>();
        isRunning = true;
        // プレイヤーの取得
        if (player == null)
        {
            player = GameObject.Find("PlayerCharacter");
        }
        dirtyCounterObject = GameObject.Find("DirtyCounter");

        DirtyCreater creater;

        ///// ふつーの汚れ
        for (int i = 0; i < dirtyPointsOfArea1.Length; i++)
        {
            creater = dirtyPointsOfArea1[i].GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.AffiliationArea = 1;
        }

        for (int i = 0; i < dirtyPointsOfArea2.Length; i++)
        {
            creater = dirtyPointsOfArea2[i].GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.AffiliationArea = 2;
        }

        for (int i = 0; i < dirtyPointsOfArea3.Length; i++)
        {
            creater = dirtyPointsOfArea3[i].GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.AffiliationArea = 3;
        }

        for (int i = 0; i < dirtyPointsOfArea4.Length; i++)
        {
            creater = dirtyPointsOfArea4[i].GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.AffiliationArea = 4;
        }


        //// レアの汚れ


        if (RealityPoint1)
        {

            creater = RealityPoint1.GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.IsReality = true;
            creater.AffiliationArea = 1;

        }


        if (RealityPoint2)
        {

            creater = RealityPoint2.GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.IsReality = true;
            creater.AffiliationArea = 2;
        }
        if (RealityPoint3)
        {

            creater = RealityPoint3.GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.IsReality = true;
            creater.AffiliationArea = 3;
        }
        if (RealityPoint4)
        {

            creater = RealityPoint4.GetComponent<DirtyCreater>();
            creater.ParntDirtySystem = this;
            creater.IsReality = true;
            creater.AffiliationArea = 4;
        }

    }

    /**
     * <summary>Awakes this object.</summary>
     *
     * <remarks>Kazuyuki,.</remarks>
     */

    void Awake()
    {
      


}

    /**
     * <summary>Update is called once per frame.</summary>
     *
     * <remarks>Kazuyuki,.</remarks>
     */

	void Update ()
    {

        if (IsRunning)
        {

            for (int i = 0; i < dirtyPointsOfArea1.Length; i++)
            {
                DirtyCreater creater = dirtyPointsOfArea1[i].GetComponent<DirtyCreater>();
                creater.CheckDistance(player.transform.position);
            }


            for (int i = 0; i < dirtyPointsOfArea2.Length; i++)
            {
                DirtyCreater creater = dirtyPointsOfArea2[i].GetComponent<DirtyCreater>();
                creater.CheckDistance(player.transform.position);
            }


            for (int i = 0; i < dirtyPointsOfArea3.Length; i++)
            {
                DirtyCreater creater = dirtyPointsOfArea3[i].GetComponent<DirtyCreater>();
                creater.CheckDistance(player.transform.position);
            }

            for (int i = 0; i < dirtyPointsOfArea4.Length; i++)
            {
                DirtyCreater creater = dirtyPointsOfArea4[i].GetComponent<DirtyCreater>();
                creater.CheckDistance(player.transform.position);
            }
            totalDestroyDirtyCount = destroyDirtyCount1 + destroyDirtyCount2 + destroyDirtyCount3 + destroyDirtyCount4;
            if (dirtyCounterObject)
            {
                dirtyCounterObject.GetComponent<DirtyCounter>().Count = totalDestroyDirtyCount;
            }


        }


    }

    /**********************************************************************************************//**
     * @fn  public void NoticeDestroyToSystem(uint areaNum ,bool isReality)
     *
     * @brief   外部からの汚れ消し窓口
     *
     * @author  Kazuyuki
     *
     * @param   areaNum     The area number.
     * @param   isReality   true if this object is reality.
     **************************************************************************************************/

    /**
     * <summary>Notice destroy to system.</summary>
     *
     * <remarks>Kazuyuki,.</remarks>
     *
     * <param name="areaNum">  The area number.</param>
     * <param name="isReality">true if this object is reality.</param>
     */

    public void NoticeDestroyToSystem(uint areaNum ,bool isReality)
    {
        washChain.GetWash();
        switch (areaNum)
        {
            case 1:

                DestroyDirtyCount1++;
                if (10000 < DestroyDirtyCount1)
                {
                    DestroyDirtyCount1 = 9999;
                }
                else if (DestroyDirtyCount1 < 0)
                {
                    DestroyDirtyCount1 = 0;
                }
                if (isReality)
                {
                    IsDestroyRealityDirty1 = true;
                }
                
                break;

            case 2:

                DestroyDirtyCount2++;
                if (10000 < DestroyDirtyCount2)
                {
                    DestroyDirtyCount2 = 9999;
                }
                else if (DestroyDirtyCount2 < 0)
                {
                    DestroyDirtyCount2 = 0;
                }
                if (isReality)
                {
                    IsDestroyRealityDirty2 = true;
                }
                break;
            case 3:

                DestroyDirtyCount3++;
                if (10000 < DestroyDirtyCount3)
                {
                    DestroyDirtyCount3 = 9999;
                }
                else if (DestroyDirtyCount3 < 0)
                {
                    DestroyDirtyCount3 = 0;
                }
                if (isReality)
                {
                    IsDestroyRealityDirty3 = true;
                }
                break;
            case 4:

                DestroyDirtyCount4++;
                if (10000 < DestroyDirtyCount4)
                {
                    DestroyDirtyCount4 = 9999;
                }
                else if (DestroyDirtyCount4 < 0)
                {
                    DestroyDirtyCount4 = 0;
                }
                if (isReality)
                {
                    IsDestroyRealityDirty4 = true;
                }
                break;
        }

    }
}
