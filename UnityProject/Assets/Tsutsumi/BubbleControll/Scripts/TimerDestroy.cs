﻿using UnityEngine;
using System.Collections;

public class TimerDestroy : MonoBehaviour {

    public float LIVE_TIMER;
    private float timeCount;

	// Use this for initialization
	void Start () {
        timeCount = 0.0f;
	}
	
	// Update is called once per frame
	void Update () {
        timeCount += Time.deltaTime;

        if (timeCount > LIVE_TIMER)
        {
            Destroy(gameObject);
        }
	}
}