﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class Fade : MonoBehaviour{
    private enum State { Out, In, End };
    private State fade = State.Out;

    [SerializeField]
    float time;
    [SerializeField]
    float fadeintime;             // フェードイン・アウトそれぞれにかける時間

    [SerializeField]
    float fadeouttime;             // フェードイン・アウトそれぞれにかける時間

    private float alpha;
    private static string SceneName;           // 遷移先のシーン名

    private int[] bubblecount = new int[5];

    [SerializeField]
    Image bubblekun;

    [SerializeField]
    List<Image> bubble;
    [SerializeField]
    List<Image> soaps;
    [SerializeField]
    Sprite awa;

    public static float soapheight;
    public static Vector2 awaScale;
    public static float soapheightinter;

    [SerializeField]
    float awainter;
    private static bool fadeflg;
    private static bool destroy;
    [SerializeField]
    Canvas screen;
    //========================================
    // Use this for initialization
    //========================================
    void Start(){
        Vector3 trans = new Vector3(-200,0,0);
        // 一時的に破棄しない
        for(int i = 0 ; i < soaps.Count ; ++i)
        {
            trans.y = soapheight - soapheightinter*i;
            soaps[i].rectTransform.position = trans;
            soaps[i].transform.SetSiblingIndex(1);
        }
        time = 0.0f;
        fadeflg = false;
        DontDestroyOnLoad(this);
        soapheight = (float)Screen.height*0.9f;
        soapheightinter = Screen.height / 5;
        awaScale = new Vector2(Screen.width / 8.5f, Screen.height / 4.5f);
	}

    void Update(){
       if(destroy)
       {
           DestryBubble();
           destroy = false;
       }
       Vector3 move = new Vector3((float)Screen.width / fadeintime * Time.deltaTime, 0, 0);
       bubble[0].rectTransform.sizeDelta = awaScale;
       if (fadeflg)
       {   
           time += Time.deltaTime;
           for (int i = 0; i < soaps.Count; ++i)
           {
               if (time - (i * awainter) > 0)
               {
                   move = soaps[i].rectTransform.position;
                   move.x += (float)Screen.width / (fadeintime - soaps.Count * awainter) * (Time.deltaTime);
                   soaps[i].rectTransform.position = move;
                   if ((int)move.x > bubblecount[i] * ((float)Screen.width+50)/9 && move.x < (float)Screen.width)
                   {
                       Image add = Image.Instantiate(bubble[0]);
                       move.x = bubblecount[i] * ((float)Screen.width) / 9+Screen.width/17;
                       add.rectTransform.parent = soaps[i].rectTransform.parent;
                       add.color = bubble[0].color;
                       add.transform.SetAsLastSibling();
                       add.rectTransform.position = move;
                       bubble.Add(add);
                       bubblecount[i]++;
                   }
                   soaps[i].transform.SetAsLastSibling();
                   
               }
           }
       }
        Color c;
        for(int i= 1; i<bubble.Count;++i)
        {
            c = bubble[i].color;
            if ((c.a < 1.0f || time < fadeintime) && time < fadeouttime + i * 0.02f)
            {
                c.a+= 0.04f;
                if(c.a>1.0f)
                {
                    c.a = 1.0f;
                }
                bubble[i].color = c;
            }
            if(c.a >0.0f && time > fadeouttime+ i * 0.02f)
            {
                c.a -= 0.08f;
                bubble[i].color = c;
                if (bubble[bubble.Count - 1].color.a <= 0.0f)
                {
                    DestryBubble();
                    fadeflg = false;

                }
            }
        }

        if (time>fadeouttime && Application.loadedLevelName != SceneName)
        {
            Application.LoadLevel(SceneName);
        }


        
    }

    //========================================
    // フェードイン・アウト処理
    //========================================
    
    public static void ChangeScene(string nextScene)
    {
        
       // if (Input.GetKeyDown(KeyCode.L))
        if (!fadeflg)
        {
            SceneName = nextScene;
            fadeflg = true;
            destroy = true;
            soapheight = (float)Screen.height * 0.9f;
            soapheightinter = Screen.height / 5;
            awaScale = new Vector2(Screen.width / 6.5f, Screen.height / 3.5f);
        }
    }

    void DestryBubble()
    {
        {
            Vector3 trans = new Vector3(-200, 0, 0);
            // 一時的に破棄しない
            for (int i = 0; i < soaps.Count; ++i)
            {
                trans.y = soapheight - soapheightinter * i;
                soaps[i].rectTransform.position = trans;
                bubblecount[i] = 0;
            }
            for (int i = 1; i < bubble.Count; ++i)
            {
                Destroy(bubble[i].gameObject);
            }
            bubble.Clear();
            bubble.Add(bubblekun);
            time = 0.0f;
        }
    }

    public static bool FadeEnd()
    {
        return !fadeflg;
    }

}
