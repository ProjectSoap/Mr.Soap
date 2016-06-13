using UnityEngine;
using System.Collections;

public class GameStartMgr : MonoBehaviour {

    ResourceRequest resReq;
    GameObject BGMManager;

	// Use this for initialization
	void Start () {



        StartCoroutine(Load());
	}
	
	// Update is called once per frame
	void Update () {
       
        DontDestroyOnLoad(BGMManager);
        if(resReq.isDone)
        {
            Application.LoadLevel("main");
        }
	}

    public IEnumerator Load()
    {
        // リソースの非同期読込開始
        resReq = Resources.LoadAsync<BGMManager>("BGMManager");
        // 終わるまで待つ
        while (resReq.isDone == false)
        {
            Debug.Log("Loading progress:" + resReq.progress.ToString());
            yield return 0;
        }
        // プレハブを取得
        BGMManager = (GameObject)Instantiate(resReq.asset);
       // Instantiate(BGMManager);
        //BGMManager = new GameObject();
     //   bgmmanager = BGMManager.AddComponent<BGMManager>();
        
    }

}
