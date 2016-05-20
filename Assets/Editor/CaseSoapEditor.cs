
using UnityEngine;
using UnityEditor;


public class Test: Editor
{

    [MenuItem("Project Soap/汚れポイントの編集用メッシュを削除")]
    public static void CreateNewSomething()
    {

        // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            // シーン上に存在するオブジェクトならば処理.
            if (obj.activeInHierarchy && obj.tag == "DirtyPoint")
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                if (renderer != null)
                {
                    DestroyImmediate(renderer);
                }

                MeshFilter filter = obj.GetComponent<MeshFilter>();

                if (filter != null)
                {
                    DestroyImmediate(filter);
                }

                Debug.Log(obj.name); 
            }
        }
    }
    [MenuItem("Project Soap/汚れポイントの編集用メッシュを追加")]
    public static void AddEditMeshForDirtyPoint()
    {

        // typeで指定した型の全てのオブジェクトを配列で取得し,その要素数分繰り返す.
        foreach (GameObject obj in UnityEngine.Object.FindObjectsOfType(typeof(GameObject)))
        {
            // シーン上に存在するオブジェクトならば処理.
            if (obj.activeInHierarchy && obj.tag == "DirtyPoint")
            {
                MeshRenderer renderer = obj.GetComponent<MeshRenderer>();

                if (renderer == null)
                {
                    renderer = obj.AddComponent<MeshRenderer>();
                    renderer.shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                    renderer.sharedMaterial = new Material(Shader.Find("Unlit/Transparent"));

                }

                MeshFilter filter = obj.GetComponent<MeshFilter>();

                if (filter == null)
                {
                    filter = obj.AddComponent<MeshFilter>();
                    // create -Z direction Billboard
                    Mesh mesh = new Mesh();
                    mesh.name = "Plane(-Z)";

                    mesh.vertices = new Vector3[] {
                    new Vector3( -0.5f, -0.5f, 0.0f ),
                    new Vector3( -0.5f, 0.5f, 0.0f ),
                    new Vector3( 0.5f, -0.5f, 0.0f ),
                    new Vector3( 0.5f, 0.5f, 0.0f ),
                      };

                    mesh.triangles = new int[] {
                    0, 1, 2, 1, 3, 2
                    };

                    mesh.uv = new Vector2[] {
                    new Vector2( 0.0f, 1.0f ),
                      new Vector2( 0.0f, 0.0f ),
                     new Vector2( 1.0f, 1.0f ),
                    new Vector2( 1.0f, 0.0f ),
                    };

                    mesh.RecalculateNormals();

                    // attack mesh to filter
                    filter.mesh = mesh;
                }
                // GameObjectの名前を表示.
                Debug.Log(obj.name);
            }
        }
    }
}