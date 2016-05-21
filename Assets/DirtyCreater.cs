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

    bool is_myDityDestroy;  // 消されたかどうか

    // Use this for initialization
    void Start ()
    {
        dirtyObjectInstance = new GameObject[createNumber];
        createTime = 0;
        is_myDityDestroy = true;

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
        is_myDityDestroy = true;
    }

    // Update is called once per frame
    void Update ()
    {
        DrawDebugQuadMesh();
        // 汚れがなくて 生成時間時間が0
        if (is_myDityDestroy && createTime <= 0)
        {
            for (int i = 0;i < createNumber; i++)
            {
                Vector3 pos =  new Vector3(Mathf.Cos( Mathf.Deg2Rad * i * 360.0f / (float)createNumber), Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)createNumber), 0);
                pos.x *= createRange.x;
                pos.y *= createRange.y;
                pos = transform.rotation * pos;
                dirtyObjectInstance[i] = (GameObject)Instantiate(dirtyObject, pos + transform.position, transform.rotation);

                // デバッグ用 街範囲外や規定高度外(クリエイトポイント基準)な場合 警告
#if DEBUG
                if (pos.y < -0.5)
                {
                    Debug.Log(dirtyObjectInstance[i].name + "の生成場所が規定範囲を超えています!");
                }
#endif

                // 親子関係的なものを構築
                DirtyObjectScript obj = dirtyObjectInstance[i].GetComponent<DirtyObjectScript>();
                dirtyObjectInstance[i].transform.parent = this.transform;
                obj.MyCreater = this;   // 汚れスクリプトに自分を伝える
                is_myDityDestroy = false;

            }
            createTime = 600;

        }
        else
        {
            createTime -= Time.deltaTime;
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
}
