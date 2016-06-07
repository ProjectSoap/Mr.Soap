using UnityEngine;
using System.Collections;

public class ResultManagerSystem : MonoBehaviour {
    
    //外部からセット
    public SaveDataManager saveDataManager;

    //スコア関連を保持する
    private ActionRecordManager.SActionRecord gameScore;    //今回のスコアなどのデータ
    private ActionRecordManager.SActionRecord saveDataOld;  //更新前のセーブデータ

	// Use this for initialization
	void Start () {
	    //ゲームの結果を確認
        gameScore = ActionRecordManager.sActionRecord;
        
        //セーブデータから値を取得


	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private void GetSaveData()
    {

    }
}
