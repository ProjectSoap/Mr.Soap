using UnityEngine;
using System.Collections;

public class DirtyCreater : MonoBehaviour {
    
    [SerializeField]
    Object[] dirty;   // 汚れオブジェクト

    [SerializeField]
    Vector2 createRange; // 生成範囲

    [SerializeField]
    Vector2 createNumber; // 生成個数

    float createTime;

    GameObject dirtyInstance;

    bool is_myDityDestroy;

    // Use this for initialization
    void Start () {
        int width = 960;
        int height = 540;

        bool fullscreen = true;

        int preferredRefreshRate = 60;

        Screen.SetResolution(width, height, fullscreen, preferredRefreshRate);


        createTime = 20;
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
            for (int i = 0;i < dirty.GetLength(0);i++)
            {
                dirtyInstance = (GameObject)Instantiate(dirty[0], (transform.rotation * new Vector3(Mathf.Sin(0.0f), Mathf.Cos((float)(dirty.GetLength(1)) - 1.0f * 360.0f / i), 0)) + transform.position, transform.rotation);
                DirtyObjectScript obj = dirtyInstance.GetComponent<DirtyObjectScript>();
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
