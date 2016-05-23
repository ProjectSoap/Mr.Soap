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

    float createTime;   // 生成時間

    GameObject[] dirtyObjectInstance;   // 管理用

    bool isMyDityDestroy;  // 消されたかどうか

    bool isRangeOut;   // マップ範囲外であるか

    char rangeOutCount;

    float deltaTime;

    // Use this for initialization
    void Start ()
    {
        dirtyObjectInstance = new GameObject[createNumber];
        createTime = 0;
        isMyDityDestroy = true;

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

    void DirtyDestroy()
    {
        isMyDityDestroy = true;
    }

    // Update is called once per frame
    void Update ()
    {
        DrawDebugQuadMesh();
        //
        deltaTime = Time.deltaTime;
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
        for (int i = 0; i < createNumber; i++)
        {
            Vector3 pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * i * 360.0f / (float)createNumber), Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)createNumber), 0);
            pos.x *= createRange.x;
            pos.y *= createRange.y;
            pos = transform.rotation * pos;
            dirtyObjectInstance[i] = (GameObject)Instantiate(dirtyObject, pos + transform.position, transform.rotation);
            
            // 親子関係的なものを構築
            DirtyObjectScript obj = dirtyObjectInstance[i].GetComponent<DirtyObjectScript>();
            dirtyObjectInstance[i].transform.parent = this.transform;
            obj.MyCreater = this;   // 汚れスクリプトに自分を伝える
            isMyDityDestroy = false;

        }
        
    } 

    void CountTime()
    {
        if (isRangeOut)
        {
            rangeOutCount++;
        }
    }
}
