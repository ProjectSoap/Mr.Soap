using UnityEngine;
using System.Collections;

public class RecoverySoapCreatersManager : MonoBehaviour {
    // 稼働しているか
    bool isRunning;
    public bool IsRunning
    {
        get { return isRunning; }
        set { isRunning = value; }
    }
    [SerializeField]
    bool isUnlockArea1;
    [SerializeField]
    bool isUnlockArea2;
    [SerializeField]
    bool isUnlockArea3;
    [SerializeField]
    bool isUnlockArea4;

    [SerializeField]
    float countSecond;

    [SerializeField]
    uint minTimeForInstance;

    [SerializeField]
    uint maxTimeForInstance;

    [SerializeField]
    GameObject[] RecoverySoapCreaters1;
    [SerializeField]
    GameObject[] RecoverySoapCreaters2;
    [SerializeField]
    GameObject[] RecoverySoapCreaters3;
    [SerializeField]
    GameObject[] RecoverySoapCreaters4;

    [SerializeField]
    GameObject player;

    NorticeDirectionRecaverySoap arrow;
    NorticeUIOfAppearanceRecoverySoap sprite;

    // Use this for initialization
    void Start ()
    {
        isRunning = false;

        arrow = GameObject.Find("NorticeRecoveryDirection").GetComponent<NorticeDirectionRecaverySoap>();
        sprite = GameObject.Find("NorticeRecoverySoapSprite").GetComponent<NorticeUIOfAppearanceRecoverySoap>();
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (IsRunning)
        {
            countSecond += Time.deltaTime;


            CheckDistanceFromPlayer();
            // 指定した秒数内
            if (minTimeForInstance <= countSecond && countSecond <= maxTimeForInstance)
            {

                uint difference = maxTimeForInstance - minTimeForInstance;
                if (difference / ((float)maxTimeForInstance - countSecond) > Random.value)
                {
                    uint elemMax;
                    elemMax = (uint)RecoverySoapCreaters1.Length;
                    if (elemMax > 0)
                    {
                        uint randElem = (uint)Random.Range(0.0f, (float)elemMax - 1);

                        RecoverySoapCreater script = RecoverySoapCreaters1[randElem].GetComponent<RecoverySoapCreater>();
                        if (script)
                        {
                            if (!script.IsHaveRevoverySoap && script.IsRangeOut)
                            {
                                script.IsInstance = true;
                                countSecond = 0;    // カウントリセット
                                sprite.IsAppearance = true;
                            }

                        }

                    }
                }
            }
            if (maxTimeForInstance < countSecond)
            {
                countSecond = 0; // 最大時間を超えたらリセット

            }
        }
       
	}


    void CheckDistanceFromPlayer()
    {
        arrow.Near = 1000;
        // 区画1のせっけん出現候補地とプレイヤーの距離を検証
        if (isUnlockArea1)
        {
            for (uint i = 0; i < RecoverySoapCreaters1.Length; i++)
            {
                RecoverySoapCreater creater = RecoverySoapCreaters1[i].GetComponent<RecoverySoapCreater>();
                creater.CheckDistance(player.transform.position);
                if (creater.IsHaveRevoverySoap)
                {

                    arrow.IsAppearance = true;
                    if (arrow.Near > Vector3.Distance(creater.transform.position, player.transform.position))
                    {
                        arrow.RecoverySoap = creater.RecoverySoap;
                        arrow.Near = Vector3.Distance(creater.transform.position, player.transform.position);
                    }
                } 

            }
        }
        
        // 区画2のせっけん出現候補地とプレイヤーの距離を検証
        if (isUnlockArea2)
        {
            for (uint i = 0; i < RecoverySoapCreaters2.Length; i++)
            {
                RecoverySoapCreater creater = RecoverySoapCreaters2[i].GetComponent<RecoverySoapCreater>();
                creater.CheckDistance(player.transform.position);

            }
        }

        // 区画3のせっけん出現候補地とプレイヤーの距離を検証
        if (isUnlockArea3)
        {
            for (uint i = 0; i < RecoverySoapCreaters3.Length; i++)
            {
                RecoverySoapCreater creater = RecoverySoapCreaters3[i].GetComponent<RecoverySoapCreater>();
                creater.CheckDistance(player.transform.position);

            }
        }

        // 区画4のせっけん出現候補地とプレイヤーの距離を検証
        if (isUnlockArea4)
        {
            for (uint i = 0; i < RecoverySoapCreaters4.Length; i++)
            {
                RecoverySoapCreater creater = RecoverySoapCreaters4[i].GetComponent<RecoverySoapCreater>();
                creater.CheckDistance(player.transform.position);

            }
        }
    }
}
