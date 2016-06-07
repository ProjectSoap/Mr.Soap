using UnityEngine;
using System.Collections;

public class DityApparancePosition : MonoBehaviour {

    [SerializeField]
    GameObject dirtyObject;   // 汚れオブジェクト

    [SerializeField]
    Vector2 createRange; // 生成範囲

    [SerializeField]
    uint createNumber; // 生成個数


    DirtyCreater myCreater;   // 管理用
    public DirtyCreater MyCreater
    {
        get { return myCreater; }
        set { myCreater = value; }
    }
    GameObject[] dirtyObjectInstance;   // 管理用

    bool[] isMyDityDestroy;  // 消されたかどうか

    bool isRangeOut;   // マップ範囲外であるか

    char rangeOutCountPar10Seconds; // ミニマップ範囲外に出て何毎10秒いたか

    float deltaTime;    // 消されてからの経過時間
    float oldDeltaTime;    // 前の経過時間

    float countStartTime;
    bool isCountPar10Seconds; // 毎10秒経過したかのフラグ
    uint affiliationArea;
    
    // Use this for initialization
    void Start ()
    {
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
        dirtyObjectInstance = new GameObject[createNumber];
        isMyDityDestroy = new bool[createNumber];
    }


    bool isCreate;
    public bool IsCreate
    {
        get { return isCreate; }
        set { isCreate = value; }
    }
    
    public uint CreateNumber
    {
        get { return createNumber; }
        set { createNumber = value; }
    }
    // Update is called once per frame
    void Update () {

        // フラグが立ってるなら汚れ作る
        if (IsCreate)
        {
            for (int i = 0; i < CreateNumber; i++)
            {
                Vector3 pos = new Vector3(Mathf.Cos(Mathf.Deg2Rad * i * 360.0f / (float)CreateNumber), Mathf.Sin(Mathf.Deg2Rad * i * 360.0f / (float)CreateNumber), 0);
                pos.x *= createRange.x;
                pos.y *= createRange.y;
                pos = transform.rotation * pos;

                // 消されたオブジェクトは新たに作る
                if (dirtyObjectInstance[i] == null)
                {

                    dirtyObjectInstance[i] = (GameObject)Instantiate(dirtyObject, pos + transform.position, transform.rotation);

                    // 親子関係的なものを構築
                    DirtyObjectScript obj = dirtyObjectInstance[i].GetComponent<DirtyObjectScript>();
                    dirtyObjectInstance[i].transform.parent = this.transform;
                    obj.MyPoint = this;   // 汚れスクリプトに自分を伝える
                    obj.SwitchMaterial((int)Random.Range(0,7));
                    isMyDityDestroy[i] = false;
                }

            }
            isCreate = false;
        }
    }

    public void NoticeDestroy()
    {
        MyCreater.NoticeDestroy();
    }

}
