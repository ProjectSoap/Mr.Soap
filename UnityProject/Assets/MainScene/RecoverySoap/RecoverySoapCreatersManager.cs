using UnityEngine;
using System.Collections;

public class RecoverySoapCreatersManager : MonoBehaviour {


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

    // Use this for initialization
    void Start ()
    {
	
	}
	
	// Update is called once per frame
	void Update ()
    {
        countSecond += Time.deltaTime;
        for (uint i = 0;i < RecoverySoapCreaters1.Length;i++ )
        {
            RecoverySoapCreater creater = RecoverySoapCreaters1[i].GetComponent<RecoverySoapCreater>();
            creater.CheckDistance(player.transform.position);

        }
        for (uint i = 0; i < RecoverySoapCreaters2.Length; i++)
        {
            RecoverySoapCreater creater = RecoverySoapCreaters2[i].GetComponent<RecoverySoapCreater>();
            creater.CheckDistance(player.transform.position);

        }
        for (uint i = 0; i < RecoverySoapCreaters3.Length; i++)
        {
            RecoverySoapCreater creater = RecoverySoapCreaters3[i].GetComponent<RecoverySoapCreater>();
            creater.CheckDistance(player.transform.position);

        }
        for (uint i = 0; i < RecoverySoapCreaters1.Length; i++)
        {
            RecoverySoapCreater creater = RecoverySoapCreaters1[i].GetComponent<RecoverySoapCreater>();
            creater.CheckDistance(player.transform.position);

        }

        // 指定した秒数内
        if (minTimeForInstance <= countSecond && countSecond <= maxTimeForInstance)
        {

            uint difference = maxTimeForInstance - minTimeForInstance ;
            if (difference / ((float)maxTimeForInstance - countSecond) > Random.value)
            {

                uint elemMax = (uint)RecoverySoapCreaters2.Length;
                if (elemMax > 0)
                {
                    uint randElem = (uint)Random.Range(0.0f, (float)elemMax - 1);
                    
                    RecoverySoapCreater script = RecoverySoapCreaters1[randElem].GetComponent<RecoverySoapCreater>();
                    if (script)
                    {
                        if (!script.IsHaveRevoverySoap && script.IsRangeOut)
                        {
                            script.IsInstance = true;
                        }
                        
                    }
                        
                }
            }
        }
	}
}
