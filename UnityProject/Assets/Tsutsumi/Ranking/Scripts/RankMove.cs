using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class RankMove : MonoBehaviour {

    //外部から定義される
    public float START_TIME;
    public float END_TIME;
    public float MOVE_DISTANCE;
    public int RANK;
    public TexNum pointTexNum;
    public SaveDataManager dataCon;
    public Text nameTextBack;
    public Text nameText;

    //使用変数
    private Vector3 firstPosition;
    private Vector3 goalPosition;
    private float nowTime;
    private bool seTrig;

	// Use this for initialization
	void Start () {
        nowTime = 0.0f;
        goalPosition = transform.position;

        //初期位置へ移動
        firstPosition = transform.position;
        firstPosition.y += MOVE_DISTANCE;
        transform.position = firstPosition;

        //セーブデータ読み込み
        if(RANK > 0 && RANK < 10){
            int rank = (int)SaveDataManager.ESaveDataNo.RankingPoint1;
            rank += RANK - 1;

            int point;
            point = dataCon.LoadData((SaveDataManager.ESaveDataNo)rank);

            //pointを読み込んで表示部へ適用。
            if (point >= 0)
            {
                pointTexNum.SetNum(point);
                nameTextBack.text = dataCon.LoadData((SaveDataManager.ESaveDataStringNo)RANK-1);
                nameText.text = dataCon.LoadData((SaveDataManager.ESaveDataStringNo)RANK - 1);
            }
            else
            {
                //エラーなら０点をとりあえず表示。
                point = 0;
                pointTexNum.SetNum(point);
                nameTextBack.text = nameText.text = "";
            }
        }

        seTrig = true;
        
	}
	
	// Update is called once per frame
	void Update () {
        //ゴールなら指定位置で何もしない
        if (nowTime >= END_TIME)
        {
            transform.position = goalPosition;
            return;
        }

        //時間加算
        nowTime += Time.deltaTime;

        //スタート時間じゃなければ指定位置で待機
        if (nowTime < START_TIME)
        {
            transform.position = firstPosition;
            return;
        }

        //移動時間中
        if (seTrig == true)
        {
            if (BGMManager.Instance != null)
            {
                BGMManager.Instance.PlaySE("Ranking_Slide");
            }
            seTrig = false;
        }

        float percent = (nowTime - START_TIME) / (END_TIME - START_TIME);

        transform.position = firstPosition + (goalPosition - firstPosition) * percent;

	}
}
