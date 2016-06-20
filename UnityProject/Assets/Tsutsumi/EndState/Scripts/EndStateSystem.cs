using UnityEngine;
using System.Collections;

public class EndStateSystem : MonoBehaviour {

    public GameObject Sekkenkun;
    public GameObject deathParticlePrefab;
    private ParticleSystem particleSystem;
    private bool effectStartFlg;
    private float sceneTimer;

	// Use this for initialization
	void Start () {
        effectStartFlg = false;
        sceneTimer = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {

        //シーン移動カウント
        if (effectStartFlg == true)
        {
            //タイマー加算
            sceneTimer += Time.deltaTime;
            if (sceneTimer > 2.0f)
            {
                //リザルトシーンへ移動する。(フェードアウト起動？)
                Fade.ChangeScene("ResultScene");
            }
        }

	}

    //操作関数
    public void StartEndState()
    {
        //セッケンくんに死亡エフェクトをセット
        GameObject obj;
        obj = Instantiate(deathParticlePrefab);
        obj.transform.parent = Sekkenkun.transform;

        //エフェクト位置調整
        Vector3 pos = Sekkenkun.transform.position;
        pos.y += 1.0f;
        obj.transform.position = pos;
        obj.transform.rotation = Sekkenkun.transform.rotation;

        effectStartFlg = true;
    }
    public void StopEndState()
    {
        effectStartFlg = false;
        sceneTimer = 0.0f;
    }
}
