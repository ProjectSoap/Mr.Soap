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

    PlayerCharacterController player;

    [Header("DirtyCreaterPointオブジェクトを入れる場所")]

    /**
     * <summary>区画1の汚れ場所.</summary>
     */

    [SerializeField,Tooltip("区画1のふつうの汚れ生成場所")]
    DirtyCreater[] m_commonDirtyCreatersOfArea1;

	[SerializeField, Tooltip("区画1の着地汚れ生成場所")]
	DirtyCreater[] m_landingDirtyCreatersOfArea1;

	[SerializeField, Tooltip("区画1の壁汚れ生成場所")]
	DirtyCreater[] m_wallDirtyCreatersOfArea1;

	[SerializeField, Tooltip("区画1の車についた汚れ生成場所")]
	DirtyCreater[] m_carDirtyCreatersOfArea1;
	/**
	/**
     * <summary>区画2の汚れ場所.</summary>
     */

	[SerializeField, Tooltip("区画2のふつうの汚れ生成場所")]
	DirtyCreater[] m_commonDirtyCreatersOfArea2;
	[SerializeField, Tooltip("区画2の着地汚れ生成場所")]
	DirtyCreater[] m_landingDirtyCreatersOfArea2;

	[SerializeField, Tooltip("区画2の壁汚れ生成場所")]
	DirtyCreater[] m_wallDirtyCreatersOfArea2;

	[SerializeField, Tooltip("区画2の車についた汚れ生成場所")]
	DirtyCreater[] m_carDirtyCreatersOfArea2;
	/**
	/**
     * <summary>区画3の汚れ場所.</summary>
     */

	[SerializeField, Tooltip("区画3のふつうの汚れ生成場所")]
	DirtyCreater[] m_commonDirtyCreatersOfArea3;
	[SerializeField, Tooltip("区画3の着地汚れ生成場所")]
	DirtyCreater[] m_landingDirtyCreatersOfArea3;

	[SerializeField, Tooltip("区画3の壁汚れ生成場所")]
	DirtyCreater[] m_wallDirtyCreatersOfArea3;
	[SerializeField, Tooltip("区画3の車についた汚れ生成場所")]
	DirtyCreater[] m_carDirtyCreatersOfArea3;
	/**
	/**
     * <summary>区画4の汚れ場所.</summary>
     */

	[SerializeField, Tooltip("区画4のふつうの汚れ生成場所")]
	DirtyCreater[] m_commonDirtyCreatersOfArea4;
	[SerializeField, Tooltip("区画4の着地汚れ生成場所")]
	DirtyCreater[] m_landingDirtyCreatersOfArea4;

	[SerializeField, Tooltip("区画4の壁汚れ生成場所")]
	DirtyCreater[] m_wallDirtyCreatersOfArea4;

	[SerializeField, Tooltip("区画4の車についた汚れ生成場所")]
	DirtyCreater[] m_carDirtyCreatersOfArea4;
	/**
     * <summary>区画1のレア汚れ.</summary>
     */

	[SerializeField, Tooltip("区画1のレア汚れ生成ポイント")]
	DirtyCreater RealityPoint1;

    /**
     * <summary>区画2のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画2のレア汚れ生成ポイント")]
	DirtyCreater RealityPoint2;

    /**
     * <summary>区画3のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画3のレア汚れ生成ポイント")]
	DirtyCreater RealityPoint3;

    /**
     * <summary>区画4のレア汚れ.</summary>
     */

    [SerializeField, Tooltip("区画4のレア汚れ生成ポイント")]
	DirtyCreater RealityPoint4;

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

        isRunning = true;
        // プレイヤーの取得
        if (player == null)
        {
            player = GameObject.Find("PlayerCharacter").GetComponent<PlayerCharacterController>();
        }
        dirtyCounterObject = GameObject.Find("DirtyCounter");
		

        ///// ふつーの汚れ
        for (int i = 0; i < m_commonDirtyCreatersOfArea1.Length; i++)
        {
			m_commonDirtyCreatersOfArea1[i].ParntDirtySystem = this;
			m_commonDirtyCreatersOfArea1[i].AffiliationArea = 1;
			m_commonDirtyCreatersOfArea1[i].Player = player;

		}

        for (int i = 0; i < m_commonDirtyCreatersOfArea2.Length; i++)
        {
			m_commonDirtyCreatersOfArea2[i].ParntDirtySystem = this;
			m_commonDirtyCreatersOfArea2[i].AffiliationArea = 2;
			m_commonDirtyCreatersOfArea2[i].Player = player;
		}

        for (int i = 0; i < m_commonDirtyCreatersOfArea3.Length; i++)
        {
			m_commonDirtyCreatersOfArea3[i].ParntDirtySystem = this;
			m_commonDirtyCreatersOfArea3[i].AffiliationArea = 3;
			m_commonDirtyCreatersOfArea3[i].Player = player;
		}

        for (int i = 0; i < m_commonDirtyCreatersOfArea4.Length; i++)
        {
			m_commonDirtyCreatersOfArea4[i].ParntDirtySystem = this;
			m_commonDirtyCreatersOfArea4[i].AffiliationArea = 4;
			m_commonDirtyCreatersOfArea4[i].Player = player;
		}



		///// 着地で落として欲しい汚れ
		for (int i = 0; i < m_landingDirtyCreatersOfArea1.Length; i++)
		{
			m_landingDirtyCreatersOfArea1[i].ParntDirtySystem = this;
			m_landingDirtyCreatersOfArea1[i].AffiliationArea = 1;
			m_landingDirtyCreatersOfArea1[i].Player = player;
		}

		for (int i = 0; i < m_landingDirtyCreatersOfArea2.Length; i++)
		{
			m_landingDirtyCreatersOfArea2[i].ParntDirtySystem = this;
			m_landingDirtyCreatersOfArea2[i].AffiliationArea = 2;
			m_landingDirtyCreatersOfArea2[i].Player = player;
		}

		for (int i = 0; i < m_landingDirtyCreatersOfArea3.Length; i++)
		{
			m_landingDirtyCreatersOfArea3[i].ParntDirtySystem = this;
			m_landingDirtyCreatersOfArea3[i].AffiliationArea = 3;
			m_landingDirtyCreatersOfArea3[i].Player = player;
		}

		for (int i = 0; i < m_landingDirtyCreatersOfArea4.Length; i++)
		{
			m_landingDirtyCreatersOfArea4[i].ParntDirtySystem = this;
			m_landingDirtyCreatersOfArea4[i].AffiliationArea = 4;
			m_landingDirtyCreatersOfArea4[i].Player = player;
		}

		///// 壁についた汚れ
		for (int i = 0; i < m_wallDirtyCreatersOfArea1.Length; i++)
		{
			m_wallDirtyCreatersOfArea1[i].ParntDirtySystem = this;
			m_wallDirtyCreatersOfArea1[i].AffiliationArea = 1;
			m_wallDirtyCreatersOfArea1[i].Player = player;
		}

		for (int i = 0; i < m_wallDirtyCreatersOfArea2.Length; i++)
		{
			m_wallDirtyCreatersOfArea2[i].ParntDirtySystem = this;
			m_wallDirtyCreatersOfArea2[i].AffiliationArea = 2;
			m_wallDirtyCreatersOfArea2[i].Player = player;
		}

		for (int i = 0; i < m_wallDirtyCreatersOfArea3.Length; i++)
		{
			m_wallDirtyCreatersOfArea3[i].ParntDirtySystem = this;
			m_wallDirtyCreatersOfArea3[i].AffiliationArea = 3;
			m_wallDirtyCreatersOfArea3[i].Player = player;
		}

		for (int i = 0; i < m_wallDirtyCreatersOfArea4.Length; i++)
		{
			m_wallDirtyCreatersOfArea4[i].ParntDirtySystem = this;
			m_wallDirtyCreatersOfArea4[i].AffiliationArea = 4;
			m_wallDirtyCreatersOfArea4[i].Player = player;
		}

		///// 車についた汚れ
		for (int i = 0; i < m_carDirtyCreatersOfArea1.Length; i++)
		{
			m_carDirtyCreatersOfArea1[i].ParntDirtySystem = this;
			m_carDirtyCreatersOfArea1[i].AffiliationArea = 1;
			m_carDirtyCreatersOfArea1[i].IsAdhereCar = true;
			m_carDirtyCreatersOfArea1[i].Player = player;
		}

		for (int i = 0; i < m_carDirtyCreatersOfArea2.Length; i++)
		{
			m_carDirtyCreatersOfArea2[i].ParntDirtySystem = this;
			m_carDirtyCreatersOfArea2[i].AffiliationArea = 2;
			m_carDirtyCreatersOfArea2[i].IsAdhereCar = true;
			m_carDirtyCreatersOfArea2[i].Player = player;
		}

		for (int i = 0; i < m_carDirtyCreatersOfArea3.Length; i++)
		{
			m_carDirtyCreatersOfArea3[i].ParntDirtySystem = this;
			m_carDirtyCreatersOfArea3[i].AffiliationArea = 3;
			m_carDirtyCreatersOfArea3[i].IsAdhereCar = true;
			m_carDirtyCreatersOfArea3[i].Player = player;
		}

		for (int i = 0; i < m_carDirtyCreatersOfArea4.Length; i++)
		{
			m_carDirtyCreatersOfArea4[i].ParntDirtySystem = this;
			m_carDirtyCreatersOfArea4[i].AffiliationArea = 4;
			m_carDirtyCreatersOfArea4[i].IsAdhereCar = true;
			m_carDirtyCreatersOfArea4[i].Player = player;
		}

		//// レアの汚れ


		if (RealityPoint1)
        {
			RealityPoint1.ParntDirtySystem = this;
			RealityPoint1.IsReality = true;
			RealityPoint1.AffiliationArea = 1;
			RealityPoint1.Player = player;

		}


        if (RealityPoint2)
        {
			RealityPoint2.ParntDirtySystem = this;
			RealityPoint2.IsReality = true;
			RealityPoint2.AffiliationArea = 2;
			RealityPoint2.Player = player;
		}
        if (RealityPoint3)
        {
            RealityPoint3.ParntDirtySystem = this;
            RealityPoint3.IsReality = true;
			RealityPoint3.AffiliationArea = 3;
			RealityPoint3.Player = player;
		}
        if (RealityPoint4)
        {
			
            RealityPoint4.ParntDirtySystem = this;
            RealityPoint4.IsReality = true;
			RealityPoint4.AffiliationArea = 4;
			RealityPoint4.Player = player;
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

	void Update()
	{


		for (int i = 0; i < m_commonDirtyCreatersOfArea1.Length; i++)
		{
			DirtyCreater creater = m_commonDirtyCreatersOfArea1[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_commonDirtyCreatersOfArea2.Length; i++)
		{
			DirtyCreater creater = m_commonDirtyCreatersOfArea2[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_commonDirtyCreatersOfArea3.Length; i++)
		{
			DirtyCreater creater = m_commonDirtyCreatersOfArea3[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		for (int i = 0; i < m_commonDirtyCreatersOfArea4.Length; i++)
		{
			DirtyCreater creater = m_commonDirtyCreatersOfArea4[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		// 着地
		for (int i = 0; i < m_landingDirtyCreatersOfArea1.Length; i++)
		{
			DirtyCreater creater = m_landingDirtyCreatersOfArea1[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_landingDirtyCreatersOfArea2.Length; i++)
		{
			DirtyCreater creater = m_landingDirtyCreatersOfArea2[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_landingDirtyCreatersOfArea3.Length; i++)
		{
			DirtyCreater creater = m_landingDirtyCreatersOfArea3[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		for (int i = 0; i < m_landingDirtyCreatersOfArea4.Length; i++)
		{
			DirtyCreater creater = m_landingDirtyCreatersOfArea4[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}
		// 壁

		for (int i = 0; i < m_wallDirtyCreatersOfArea1.Length; i++)
		{
			DirtyCreater creater = m_wallDirtyCreatersOfArea1[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_wallDirtyCreatersOfArea2.Length; i++)
		{
			DirtyCreater creater = m_wallDirtyCreatersOfArea2[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_wallDirtyCreatersOfArea3.Length; i++)
		{
			DirtyCreater creater = m_wallDirtyCreatersOfArea3[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		for (int i = 0; i < m_wallDirtyCreatersOfArea4.Length; i++)
		{
			DirtyCreater creater = m_wallDirtyCreatersOfArea4[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		// 車
		for (int i = 0; i < m_carDirtyCreatersOfArea1.Length; i++)
		{
			DirtyCreater creater = m_carDirtyCreatersOfArea1[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_carDirtyCreatersOfArea2.Length; i++)
		{
			DirtyCreater creater = m_carDirtyCreatersOfArea2[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		for (int i = 0; i < m_carDirtyCreatersOfArea3.Length; i++)
		{
			DirtyCreater creater = m_carDirtyCreatersOfArea3[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}

		for (int i = 0; i < m_carDirtyCreatersOfArea4.Length; i++)
		{
			DirtyCreater creater = m_carDirtyCreatersOfArea4[i].GetComponent<DirtyCreater>();
			creater.CheckDistance(player.transform.position);
		}


		// レアの汚れは距離をチェックしない = 生成しない
		totalDestroyDirtyCount = destroyDirtyCount1 + destroyDirtyCount2 + destroyDirtyCount3 + destroyDirtyCount4;
		if (dirtyCounterObject)
		{
			dirtyCounterObject.GetComponent<DirtyCounter>().Count = totalDestroyDirtyCount;
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

    public void NoticeDestroyToSystem(DirtyCreater destroyedParent)
    {
        switch (destroyedParent.AffiliationArea)
        {
            case 1:

                DestroyDirtyCount1++;
				ActionRecordManager.sActionRecord.C1WashCount = destroyDirtyCount1;
                if (10000 < DestroyDirtyCount1)
                {
                    DestroyDirtyCount1 = 9999;
                }
                else if (DestroyDirtyCount1 < 0)
                {
                    DestroyDirtyCount1 = 0;
                }
                if (destroyedParent.IsReality)
                {
                    IsDestroyRealityDirty1 = true;
                }
                
                break;

            case 2:

                DestroyDirtyCount2++;
				ActionRecordManager.sActionRecord.C2WashCount = destroyDirtyCount2;
				if (10000 < DestroyDirtyCount2)
                {
                    DestroyDirtyCount2 = 9999;
                }
                else if (DestroyDirtyCount2 < 0)
                {
                    DestroyDirtyCount2 = 0;
				}
				if (destroyedParent.IsReality)
				{
                    IsDestroyRealityDirty2 = true;
                }
                break;
            case 3:

                DestroyDirtyCount3++;
				ActionRecordManager.sActionRecord.C3WashCount = destroyDirtyCount3;
				if (10000 < DestroyDirtyCount3)
                {
                    DestroyDirtyCount3 = 9999;
                }
                else if (DestroyDirtyCount3 < 0)
                {
                    DestroyDirtyCount3 = 0;
				}
				if (destroyedParent.IsReality)
				{
                    IsDestroyRealityDirty3 = true;
                }
                break;
            case 4:

                DestroyDirtyCount4++;
				ActionRecordManager.sActionRecord.C4WashCount = destroyDirtyCount4;
				if (10000 < DestroyDirtyCount4)
                {
                    DestroyDirtyCount4 = 9999;
                }
                else if (DestroyDirtyCount4 < 0)
                {
                    DestroyDirtyCount4 = 0;
				}
				if (destroyedParent.IsReality)
				{
                    IsDestroyRealityDirty4 = true;
                }
                break;
        }

    }
}
