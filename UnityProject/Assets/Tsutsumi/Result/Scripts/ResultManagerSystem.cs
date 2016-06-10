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
    public SaveDataManager saveDataManager;     //セーブデータ管理
    public CheckRecordCondition checkRecord;    //実績条件判定
    public ResultCanvasKaihouCon canvasKaihou;  //実績を取得した場合有効に

    //スコア関連を保持する
    private ActionRecordManager.SActionRecord gameScore;    //今回のスコアなどのデータ
    private ActionRecordManager.SActionRecord saveDataOld;  //更新前のセーブデータ
    private ActionRecordManager.SActionRecord saveDataNew;  //更新後のセーブデータ

    //ランキングを先頭１位から順番に保持。最後の所は今回のスコアが入る。
    private int[] rankingPoint = new int[11];

    //プレイ回数
    private int playCount;

    //今回のランキング1~11（一応
    private int rank;

    //実績を新たに開放した場合のフラグ
    private bool[] recordFlgOld = new bool[30];
    private bool[] recordFlgNew = new bool[30];
    private bool[] recordGetFlg = new bool[30];

    //ポイント表示完了フラグ
    private bool pointDrawEndFlg;
    //BGMトリガー
    private bool[] recordGetBGMTrig = new bool[30];

	// Use this for initialization
	void Start () {
        rank = 0;
        pointDrawEndFlg = false;
        for (int i = 0; i < 30; ++i)
        {
            recordGetBGMTrig[i] = true;
        }
        
        //セーブデータから値を取得
        Load();
        //実績の状態も取得
        GetRecord(true);

	    //ゲームの結果を確認
        gameScore = ActionRecordManager.sActionRecord;
        rankingPoint[10] = gameScore.C1WashCount + gameScore.C2WashCount + gameScore.C3WashCount + gameScore.C4WashCount;
        
        //新しいセーブデータ作成
        playCount++;
        UpdateSaveData();

        //セーブ
        Save();
        //実績の状態を再取得
        GetRecord(false);

        //新しいセーブデータと古いセーブデータを確認して実績が今回で解放されたのかチェックする。
        CheckGetRecord();

        //BGM再生
        BGMManager.Instance.PlayBGM("Result_BGM", 0);
	}
	
	// Update is called once per frame
	void Update () {
        //まだポイント表示がおわっていなければ何もしない。
        if (pointDrawEndFlg == false)
        {
            return;
        }

	    //実績が今回で解放されていれば表示へ
        bool _getRecordflg = false;
        for (int i = 0; i < 30; ++i)
        {
            if (recordGetFlg[i] == true)
            {
                //実績開放音を鳴らす
                if (recordGetBGMTrig[i] == true)
                {
                    //SE

                    recordGetBGMTrig[i] = false;
                }

                _getRecordflg = true;
                canvasKaihou.gameObject.SetActive(true);
                canvasKaihou.SetRecordImage(i);
                if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Joystick1Button0))
                {
                    recordGetFlg[i] = false;
                }

                return;
            }
        }
        if (_getRecordflg == false)
        {
            canvasKaihou.gameObject.SetActive(false);
        }


        //入力を見てメニューへ
        if (Input.GetKey(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKey(KeyCode.KeypadEnter) || Input.GetKey(KeyCode.Joystick1Button0))
        {
            Fade.ChangeScene("Menu");
        }
	}

    //ポイント表示が完了した
    public void SetPointDrawEndFlg(bool _flg)
    {
        pointDrawEndFlg = _flg;
    }

    //セーブデータを読み込む
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

    //セーブする
    private void Save()
    {
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.PlayCount, playCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C1WashCount, saveDataNew.C1WashCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C2WashCount, saveDataNew.C2WashCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C3WashCount, saveDataNew.C3WashCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C4WashCount, saveDataNew.C4WashCount);

        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.WashChainCount, saveDataNew.WashChainCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.ChachSopeCount, saveDataNew.ChachSopeCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.CrashCount, saveDataNew.CrashCount);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.WashCarCount, saveDataNew.WashCarCount);
        if(saveDataNew.isRain == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RainPlayFlg, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RainPlayFlg, 0);

        if (saveDataNew.isFog == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.FogPlayFlg, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.FogPlayFlg, 0);
        if (saveDataNew.isWind == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.WindPlayFlg, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.WindPlayFlg, 0);
        if (saveDataNew.isSekkenChanPlay == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.SekkenChanPlayFlg, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.SekkenChanPlayFlg, 0);
        if (saveDataNew.isSekkenKunPlay == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.SekkenKun0PlayFlg, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.SekkenKun0PlayFlg, 0);
        if (saveDataNew.C1HideWashFlg == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C1HideWash, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C1HideWash, 0);

        if (saveDataNew.C2HideWashFlg == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C2HideWash, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C2HideWash, 0);
        if (saveDataNew.C3HideWashFlg == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C3HideWash, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C3HideWash, 0);
        if (saveDataNew.C4HideWashFlg == true)
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C4HideWash, 1);
        else
            saveDataManager.SaveData(SaveDataManager.ESaveDataNo.C4HideWash, 0);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint1, rankingPoint[0]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint2, rankingPoint[1]);

        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint3, rankingPoint[2]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint4, rankingPoint[3]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint5, rankingPoint[4]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint6, rankingPoint[5]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint7, rankingPoint[6]);

        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint8, rankingPoint[7]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint9, rankingPoint[8]);
        saveDataManager.SaveData(SaveDataManager.ESaveDataNo.RankingPoint10, rankingPoint[9]);

    }

    //読み込んだセーブデータに今回のスコアを反映して新しいセーブデータを作成
    private void UpdateSaveData()
    {
        //セーブ用データ作成
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

        saveDataNew.isSekkenChanPlay = saveDataOld.isSekkenChanPlay || gameScore.isSekkenChanPlay;
        saveDataNew.isSekkenKunPlay = saveDataOld.isSekkenKunPlay || gameScore.isSekkenKunPlay;

        saveDataNew.C1HideWashFlg = saveDataOld.C1HideWashFlg || gameScore.C1HideWashFlg;
        saveDataNew.C2HideWashFlg = saveDataOld.C2HideWashFlg || gameScore.C2HideWashFlg;
        saveDataNew.C3HideWashFlg = saveDataOld.C3HideWashFlg || gameScore.C3HideWashFlg;
        saveDataNew.C4HideWashFlg = saveDataOld.C4HideWashFlg || gameScore.C4HideWashFlg;

        //ランキングをソートする。
        RankingSort();
    }

    //ランキングポイントをソートする。
    private void RankingSort()
    {
        int[] tempPointList = new int[11];
        bool[] selectFlg = new bool[11];
        int highPoint = 0;
        int highPointListNo = 0;

        //一時作業領域へコピー
        for (int i = 0; i < 11; ++i)
        {
            tempPointList[i] = rankingPoint[i];
            selectFlg[i] = false;
        }

        //高いの選んでいくソート
        for(int count = 0; count < 11; ++count)
        {
            highPoint = 0;
            highPointListNo = 0;
            
            //選ばれてない中で一番高い数字を取得
            for (int i = 0; i < 11; ++i)
            {
                //もう選択済み
                if (selectFlg[i] == true)
                {
                    continue;
                }

                //選択されておらず今までで一番高いポイントだった
                if (tempPointList[i] > highPoint)
                {
                    highPoint = tempPointList[i];
                    highPointListNo = i;
                }
            }

            //一番高いポイントが決定
            selectFlg[highPointListNo] = true;

            //今回のスコアだった
            if (highPointListNo == 10)
            {
                //0番配列は1位。
                rank = count + 1;
            }

            //答え格納先へ代入
            rankingPoint[count] = highPoint;
        }
    }

    //レコードを取得する
    private void GetRecord(bool oldFlg){

        if (oldFlg == true)
        {
            for (int i = 0; i < 30; ++i)
            {
                recordFlgOld[i] = checkRecord.CheckRecordConditionClear(i);
            }
        }
        else
        {
            for (int i = 0; i < 30; ++i)
            {
                recordFlgNew[i] = checkRecord.CheckRecordConditionClear(i);
            }
        }
    }
    //新旧レコードを比較して新たに取得したものを判断する。
    private void CheckGetRecord()
    {
        for (int i = 0; i < 30; ++i)
        {
            if (recordFlgOld[i] == false && recordFlgNew[i] == true)
            {
                recordGetFlg[i] = true;
            }
            else
            {
                recordGetFlg[i] = false;
            }
        }

        recordGetFlg[7] = true;
        recordGetFlg[14] = true;
        recordGetFlg[21] = true;
    }
}
