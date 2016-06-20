using UnityEngine;
using System.Collections;

public class RankingSceneSystem : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (BGMManager.Instance != null)
        {
            BGMManager.Instance.PlayBGM("Ranking_BGM", 0);
        }
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
