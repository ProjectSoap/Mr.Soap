using UnityEngine;
using System.Collections;
using System.Collections.Generic;
public class SetBgmTime : MonoBehaviour {
    TextAsset _bgmTime;
    GameObject soundPlayerObj;
    // Use this for initialization
    void Start () {
        _bgmTime = Resources.Load("SetBgmTime", typeof(TextAsset)) as TextAsset;
    }

    public void readBGMText(string fileName, ref float loopTime,ref float endTime)
    {
        _bgmTime = Resources.Load("SetBgmTime") as TextAsset;
        char[] kugiri = {'\r', '\n'};
        string[] layoutInfo = _bgmTime.text.Split(kugiri);
 
        string[] eachInfo;
        for (int i = 0; i < layoutInfo.Length; i++) {
            eachInfo = layoutInfo[i].Split(","[0]);
            eachInfo[0] = "Sound/BGM/" + eachInfo[0];
            if(eachInfo[0] == fileName)
            {
                loopTime = float.Parse(eachInfo[1]);
                endTime = float.Parse(eachInfo[2]);
            }
        }
    }
}
