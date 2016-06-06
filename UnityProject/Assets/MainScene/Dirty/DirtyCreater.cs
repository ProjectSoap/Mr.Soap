using UnityEngine;
using System.Collections;
/*
#define CREATE_MAX 10   // 念の為に
*/
public class DirtyCreater : MonoBehaviour {
    
    [SerializeField]
    GameObject dirtyObject;   // 汚れオブジェクト

    [SerializeField]
    Vector2 createRange; // 生成範囲

    [SerializeField]
    uint createNumber; // 生成個数

    
    DirtySystem parntDirtySystem;   // 管理用
    public DirtySystem ParntDirtySystem
    {
        get { return parntDirtySystem; }
        set { parntDirtySystem = value; }
    }
    [SerializeField]
    GameObject[] appearancePoints;   // 出現位置管理

    bool isMyDityDestroy;  // 消されたかどうか

    bool isRangeOut;   // マップ範囲外であるか

    char rangeOutCountPar10Seconds; // ミニマップ範囲外に出て何毎10秒いたか

    float deltaTime;    // 消されてからの経過時間
    float oldDeltaTime;    // 前の経過時間

    float countStartTime;
    bool isCountPar10Seconds; // 毎10秒経過したかのフラグ
    uint affiliationArea;   // 所属区画

    bool isReality;
    public bool IsReality
    {
        get { return isReality; }
        set { isReality = value; }
    }

    public uint AffiliationArea
    {
        get { return affiliationArea; }
        set { affiliationArea = value; }
    }
    // Use this for initialization
    void Start ()
    {
    }


    void Awake()
    {
        countStartTime = Time.deltaTime;
        isMyDityDestroy =false;

        for (int i = 0; i < createNumber; i++)
        {

            appearancePoints[i].GetComponent<DityApparancePosition>().MyCreater = this;

        }
        appearancePoints[0].GetComponent<DityApparancePosition>().IsCreate = true;

#if DEBUG
        if (GetComponent<MeshRenderer>() != null)
        {
            Destroy(GetComponent<MeshRenderer>());
        }
#endif

        // リリースビルド時エディット用のメッシュがついてたらエラー！
#if DEBUG
        if (GetComponent<MeshRenderer>() != null)
        {
            Debug.Log(this);
            Destroy(GetComponent<MeshRenderer>());
        }
#endif
    }
    

    public void NoticeDestroy()
    {
        ParntDirtySystem.NoticeDestroyToSystem(AffiliationArea,isReality);
        isMyDityDestroy = true;
    }

    // Update is called once per frame
    void Update ()
    {
        DrawDebugQuadMesh();
        bool isCreateFlag = false;
        if (isMyDityDestroy)
        {
            // どれか消されてんなら生成フラグ立てる
            isCreateFlag = true;
        }

        // 汚れが一個でも消されてんなら生成するためのカウントを行う
        if (isCreateFlag)
        {
            CountTime();
        }
        // 生成カウントが毎10秒経過
        if (isCountPar10Seconds)
        {
            CreateDirty();
        }
    }


    void DrawDebugQuadMesh()
    {
        Vector3[] line = new Vector3[4];
        line[0].Set(-0.5f* createRange.x,  0.5f * createRange.y, 0);
        line[1].Set(-0.5f* createRange.x, -0.5f * createRange.y, 0);
        line[2].Set( 0.5f* createRange.x, -0.5f * createRange.y, 0);
        line[3].Set( 0.5f* createRange.x,  0.5f * createRange.y, 0);
        for (int i = 0; i < 4;i++)
        {
            line[i] = transform.rotation * line[i];
        }
        Debug.DrawLine(transform.position + line[0], transform.position + line[1], Color.red);
        Debug.DrawLine(transform.position + line[1], transform.position + line[2], Color.red);
        Debug.DrawLine(transform.position + line[2], transform.position + line[3], Color.red);
        Debug.DrawLine(transform.position + line[3], transform.position + line[0], Color.red);
    }

    public void CheckDistance(Vector3 playerPosition)
    {
        float distance = Vector3.Distance(playerPosition, transform.position);
        if (distance >= 15.0f)
        {
            isRangeOut = true;
        }
        else
        {
            isRangeOut = false;
        }
    }

    public void CreateDirty()
    {

        if (isCountPar10Seconds)
        {
            bool isCreate = false;
            // 毎10秒経過回数で確率アップ
            switch (rangeOutCountPar10Seconds)
            {
                case (char)1:
                    if (Random.Range(0, 100) <= 20)
                    {
                        isCreate = true;
                        deltaTime = 0;
                        rangeOutCountPar10Seconds = (char)0;
                    }
                    break;
                case (char)2:
                    if (Random.Range(0, 100) <= 30)
                    {
                        isCreate = true;
                        deltaTime = 0;
                        rangeOutCountPar10Seconds = (char)0;
                    }
                    break;
                case (char)3:
                    if (Random.Range(0, 100) <= 60)
                    {
                        isCreate = true;
                        deltaTime = 0;
                        rangeOutCountPar10Seconds = (char)0;
                    }
                    break;
                case (char)4:
                    if (true)
                    {
                        isCreate = true;
                        deltaTime = 0;
                        rangeOutCountPar10Seconds = (char)0;
                    }
                    break;
                default:
                    isCreate = false;
                    break;
            }

            // フラグが立ってるなら汚れ作る
            if (isCreate)
            {
                int num = appearancePoints.Length;
                num = (int)Random.Range((int)0, (int)num);
                appearancePoints[num].GetComponent<DityApparancePosition>().IsCreate = true;
            }
        }
        isCountPar10Seconds = false;

    } 

    void CountTime()
    {

        // 範囲外ならカウント
        if (isRangeOut)
        {
            oldDeltaTime = deltaTime;
            deltaTime += Time.deltaTime;

            // 毎10秒経過したか
            if (((int)deltaTime - 10 * rangeOutCountPar10Seconds)  >= 10)
            {
                isCountPar10Seconds = true;
                rangeOutCountPar10Seconds++;
            }
            else
            {
                isCountPar10Seconds = false;
            }
        }

    }
}
