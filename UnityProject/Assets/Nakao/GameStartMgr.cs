﻿using UnityEngine;
using System.Collections;

public class GameStartMgr : MonoBehaviour {

    ResourceRequest resReq;
    GameObject BGMManager;
    public string nextSceneName = "sekTitle";

    // Use this for initialization
    void Start () {

        Application.targetFrameRate = 60;

        
    }

    void Awake ()
    {
        StartCoroutine(Load());
    }
    
    // Update is called once per frame
    void Update () {
       
        DontDestroyOnLoad(BGMManager);
        if(resReq.isDone)
        {
            Application.LoadLevel(nextSceneName);
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
        BGMManager = Instantiate(resReq.asset) as GameObject;
       // Instantiate(BGMManager);
        //BGMManager = new GameObject();
     //   bgmmanager = BGMManager.AddComponent<BGMManager>();
        
    }

}
