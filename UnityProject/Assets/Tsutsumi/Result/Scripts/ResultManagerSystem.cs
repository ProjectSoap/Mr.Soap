using UnityEngine;
using System.Collections;

/************************************************
 * 
 * ResultManagerSystem.cs
 *  リザルト画面での進行管理を行う。入力も見る。
 * 
 ************************************************/
public class ResultManagerSystem : MonoBehaviour {
    
    //外部からセット
    public SaveDataManager saveDataManager;

    //スコア関連を保持する
    private ActionRecordManager.SActionRecord gameScore;    //今回のスコアなどのデータ
    private ActionRecordManager.SActionRecord saveDataOld;  //更新前のセーブデータ
    private ActionRecordManager.SActionRecord saveDataNew;  //更新後のセーブデータ

    //ランキングを先頭１位から順番に保持。最後の所は今回のスコアが入る。
    private int[] rankingPoint = new int[11];

    //プレイ回数
    private int playCount;

	// Use this for initialization
	void Start () {

        //セーブデータから値を取得
        Load();

	    //ゲームの結果を確認
        gameScore = ActionRecordManager.sActionRecord;
        rankingPoint[10] = gameScore.C1WashCount + gameScore.C2WashCount + gameScore.C3WashCount + gameScore.C4WashCount;
        
        //新しいセーブデータ作成
        playCount++;
        UpdateSaveData();

	}
	
	// Update is called once per frame
	void Update () {
	    //実績が解放される条件に合致すれば表示へ
        
        //入力を見てメニューへ

	}

    private void Load()
    {
        //プレイ回数
        playCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.PlayCount);

        //ゲーム中の増減の可能性のあるセーブデータを読み込み
        saveDataOld.C1WashCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C1WashCount);
        saveDataOld.C2WashCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C2WashCount);
        saveDataOld.C3WashCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C3WashCount);
        saveDataOld.C4WashCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C4WashCount);

        saveDataOld.WashChainCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C1WashCount);
        saveDataOld.ChachSopeCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.ChachSopeCount);
        saveDataOld.CrashCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.CrashCount);
        saveDataOld.WashCarCount = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.WashCarCount);

        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RainPlayFlg) > 0) saveDataOld.isRain = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.FogPlayFlg) > 0) saveDataOld.isFog = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.WindPlayFlg) > 0) saveDataOld.isWind = true;

        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.SekkenChanPlayFlg) > 0) saveDataOld.isSekkenChanPlay = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.SekkenKun0PlayFlg) > 0) saveDataOld.isSekkenKunPlay = true;

        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C1HideWash) > 0) saveDataOld.C1HideWashFlg = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C2HideWash) > 0) saveDataOld.C2HideWashFlg = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C3HideWash) > 0) saveDataOld.C3HideWashFlg = true;
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.C4HideWash) > 0) saveDataOld.C4HideWashFlg = true;

        //ランキング系読み込み
        rankingPoint[0] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint1);
        rankingPoint[1] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint2);
        rankingPoint[2] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint3);
        rankingPoint[3] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint4);
        rankingPoint[4] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint5);

        rankingPoint[5] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint6);
        rankingPoint[6] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint7);
        rankingPoint[7] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint8);
        rankingPoint[8] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint9);
        rankingPoint[9] = saveDataManager.LoadData(SaveDataManager.ESaveDataNo.RankingPoint10);

        //今回のスコアを入れる予定
        rankingPoint[10] = 0;
    }

    private void UpdateSaveData()
    {
        saveDataNew.C1WashCount = saveDataOld.C1WashCount + gameScore.C1WashCount;
        saveDataNew.C2WashCount = saveDataOld.C2WashCount + gameScore.C2WashCount;
        saveDataNew.C3WashCount = saveDataOld.C3WashCount + gameScore.C3WashCount;
        saveDataNew.C4WashCount = saveDataOld.C4WashCount + gameScore.C4WashCount;

        saveDataNew.WashChainCount = saveDataOld.WashChainCount + gameScore.WashChainCount;
        saveDataNew.ChachSopeCount = saveDataOld.ChachSopeCount + gameScore.ChachSopeCount;
        saveDataNew.CrashCount = saveDataOld.CrashCount + gameScore.CrashCount;
        saveDataNew.WashCarCount = saveDataOld.WashCarCount + gameScore.WashCarCount;

        saveDataNew.isRain = saveDataOld.isRain || gameScore.isRain;
        saveDataNew.isFog = saveDataOld.isFog || gameScore.isFog;
        saveDataNew.isWind = saveDataOld.isWind || gameScore.isWind;
    }
}
