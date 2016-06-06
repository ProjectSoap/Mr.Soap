using UnityEngine;
using System.Collections;

public class DirtySystem : MonoBehaviour
{
    [SerializeField]
    GameObject player;  // プレイヤー位置を知るためのもの

    [SerializeField]
    GameObject[] dirtyPointsOfArea1;    // 区画1の汚れ場所

    [SerializeField]
    GameObject[] dirtyPointsOfArea2;    // 区画2の汚れ場所

    [SerializeField]
    GameObject[] dirtyPointsOfArea3;    // 区画3の汚れ場所

    [SerializeField]
    GameObject[] dirtyPointsOfArea4;    // 区画4の汚れ場所



    [SerializeField]
    GameObject RealityPoint1;     // 区画1のレア汚れ

    [SerializeField]
    GameObject RealityPoint2;     // 区画2のレア汚れ

    [SerializeField]
    GameObject RealityPoint3;     // 区画3のレア汚れ

    [SerializeField]
    GameObject RealityPoint4;     // 区画4のレア汚れ

    [SerializeField]
    int totalDestroyDirtyCount;
    [SerializeField]
    int destroyDirtyCount1;          // 区画1で汚れを落とした数
    public int DestroyDirtyCount1
    {
        get { return destroyDirtyCount1; }
        set { destroyDirtyCount1 = value; }
    }
    [SerializeField]
    int destroyDirtyCount2;          // 区画2で汚れを落とした数
    public int DestroyDirtyCount2
    {
        get { return destroyDirtyCount2; }
        set { destroyDirtyCount2 = value; }
    }
    [SerializeField]
    int destroyDirtyCount3;          // 区画3で汚れを落とした数
    public int DestroyDirtyCount3
    {
        get { return destroyDirtyCount3; }
        set { destroyDirtyCount3 = value; }
    }
    [SerializeField]
    int destroyDirtyCount4;          // 区画4で汚れを落とした数
    public int DestroyDirtyCount4
    {
        get { return destroyDirtyCount4; }
        set { destroyDirtyCount4 = value; }
    }


    [SerializeField]
    bool isDestroyRealityDirty1; // 今回のプレイで区画1レア汚れを落としたか
    public bool IsDestroyRealityDirty1
    {
        get { return isDestroyRealityDirty1; }
        set { isDestroyRealityDirty1 = value; }
    }
    [SerializeField]
    bool isDestroyRealityDirty2; // 今回のプレイで区画2レア汚れを落としたか
    public bool IsDestroyRealityDirty2
    {
        get { return isDestroyRealityDirty2; }
        set { isDestroyRealityDirty2 = value; }
    }
    [SerializeField]
    bool isDestroyRealityDirty3; // 今回のプレイで区画3レア汚れを落としたか
    public bool IsDestroyRealityDirty3
    {
        get { return isDestroyRealityDirty3; }
        set { isDestroyRealityDirty3 = value; }
    }
    [SerializeField]
    bool isDestroyRealityDirty4; // 今回のプレイで区画4レア汚れを落としたか
    public bool IsDestroyRealityDirty4
    {
        get { return isDestroyRealityDirty4; }
        set { isDestroyRealityDirty4 = value; }
    }
    GameObject dirtyCounterObject;  // カウンタースクリプトを持つオブジェクト


    // Use this for initialization
    void Start ()
    {
        if (player == null)
        {
            player = GameObject.Find("PlayerCharacter");
        }
        dirtyCounterObject = GameObject.Find("DirtyCounter");
    }

    void Awake()
    {
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
	
	// Update is called once per frame
	void Update ()
    {
        for (int i = 0;i < dirtyPointsOfArea1.Length;i++)
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

    public void NoticeDestroyToSystem(uint areaNum ,bool isReality)
    {
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
