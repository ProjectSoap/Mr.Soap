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
    public ResultCanvasRanking canvasRanking;   //ランキング画面表示
    public ResultCanvasKaihouCon canvasKaihou;  //実績を取得した場合有効に
    public ResultCanvasSceneSelectCon canvasSceneSelect;    //シーン選択キャンバス
    public ResultSekkenControll sekkenControll;             //せっけんくんの制御

    //スコア関連を保持する
    private ActionRecordManager.SActionRecord gameScore;    //今回のスコアなどのデータ
    private ActionRecordManager.SActionRecord saveDataOld;  //更新前のセーブデータ
    private ActionRecordManager.SActionRecord saveDataNew;  //更新後のセーブデータ

    //ランキングを先頭１位から順番に保持。ソート関数がこれを並び替える。
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

    //ランキング表示完了フラグ
    private bool rankingDrawEndFlg;

    //画面遷移表示フラグ
    private bool sceneMoveDisplayFlg;
    private bool sceneMenuFlg;

    //入力完了フラグ
    private bool inputEndFlg;

	// Use this for initialization
	void Start () {
        rank = 0;
        pointDrawEndFlg = false;
        rankingDrawEndFlg = false;
        sceneMoveDisplayFlg = false;
        sceneMenuFlg = true;
        inputEndFlg = false;

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

        //取得ポイントを元にせっけんキャラクターとアニメーション切り替え
        ResultSekkenControll.ESekkenNo sekkenNo = ResultSekkenControll.ESekkenNo.No_Sekkenkun;
        if (gameScore.isSekkenChanPlay == true)
        {
            sekkenNo = ResultSekkenControll.ESekkenNo.No_Sekkenchan;
        }
        if (gameScore.isSekkenKun0Play == true)
        {
            sekkenNo = ResultSekkenControll.ESekkenNo.No_Sekkenkun0;
        }
        //sekkenNo = ResultSekkenControll.ESekkenNo.No_Sekkenchan;
        sekkenControll.SelectSekkenAndAnimation(sekkenNo, rankingPoint[10]);

        //新しいセーブデータ作成
        playCount++;
        UpdateSaveData();   //内部でランキングをソートしているので注意。

        //セーブ
        Save();
        //実績の状態を再取得
        GetRecord(false);

        //新しいセーブデータと古いセーブデータを確認して実績が今回で解放されたのかチェックする。
        CheckGetRecord();

        //ランキング画面へスコアをセット
        for (int i = 0; i < 9; ++i)
        {
            canvasRanking.SetPoint(rankingPoint[i], i);
        }
        //10位以下だった
        if (rank >= 9)
        {
            canvasRanking.SetPoint(rankingPoint[rank], 9);
            canvasRanking.SetScoreRank(9);
            canvasRanking.ChangeActive(true, 9);
        }
        else
        {
            //１～９位にランクイン
            canvasRanking.SetScoreRank(rank);
            canvasRanking.ChangeActive(false, 9);
        }
        

        //BGM再生
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM("Result_BGM", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {

        //まだポイント表示がおわっていなければ何もしない。
        if (pointDrawEndFlg == false || inputEndFlg == true)
        {
            return;
        }

        //ランキング表示へ
        if (rankingDrawEndFlg == false)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                rankingDrawEndFlg = true;
            }
            canvasRanking.gameObject.SetActive(true);
            return;
        }
        else
        {
            canvasRanking.gameObject.SetActive(false);
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
                    recordGetBGMTrig[i] = false;
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Actual_Open");
                    }
                }

                _getRecordflg = true;
                canvasKaihou.gameObject.SetActive(true);
                canvasKaihou.SetRecordImage(i);
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
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
        //画面遷移表示
        if (sceneMoveDisplayFlg == true)
        {
            canvasSceneSelect.gameObject.SetActive(true);

            //画面を選択する
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                sceneMenuFlg = true;
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Move");
                }
            }
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                sceneMenuFlg = false;
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Move");
                }
            }
            canvasSceneSelect.WakuSelect(sceneMenuFlg);

            //戻る
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                sceneMoveDisplayFlg = false;
                sceneMenuFlg = true;
                canvasSceneSelect.gameObject.SetActive(false);
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Cancel");
                }

                return;
            }

            //画面遷移する。
            if (sceneMenuFlg == true)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                    }
                    Fade.ChangeScene("Menu");
                    inputEndFlg = true;
                }
            }
            else
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
                {
                    //SE
                    if (BGMManager.Instance != null)
                    {
                        BGMManager.Instance.PlaySE("Cursor_Decision");
                    }
                    //キャラクター情報を保持したまま今回の成績を削除
                    ActionRecordManager.sActionRecord.ResetCharaHozi();
                    Fade.ChangeScene("main");
                    inputEndFlg = true;
                }
            }
        }
        else
        {
            canvasSceneSelect.gameObject.SetActive(false);
            sceneMenuFlg = true;

            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButton(0) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Joystick1Button0))
            {
                sceneMoveDisplayFlg = true;
                //SE
                if (BGMManager.Instance != null)
                {
                    BGMManager.Instance.PlaySE("Cursor_Decision");
                }
            }
        }
	}

    //ポイント表示が完了した
    public void SetPointDrawEndFlg(bool _flg)
    {
        pointDrawEndFlg = _flg;
    }
    public int GetMyRank()
    {
        return rank;
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
        if (saveDataManager.LoadData(SaveDataManager.ESaveDataNo.SekkenKun0PlayFlg) > 0) saveDataOld.isSekkenKun0Play = true;

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
        if (saveDataNew.isSekkenKun0Play == true)
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
        saveDataNew.isSekkenKun0Play = saveDataOld.isSekkenKun0Play || gameScore.isSekkenKun0Play;

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

        //高いの選んでいくソート。
        rank = 10;  //初期値セット。11位(ランク外)
        //０番配列（１位）から順番に決定
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
                //index番号を入れる
                rank = count;
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
    }
}
