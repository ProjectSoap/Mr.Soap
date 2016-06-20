using UnityEngine;
using UnityEngine.Audio;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;

public class BGMManager : SingletonMonoBehaviour<BGMManager>
{

    public List<AudioClip> BGMList;
    public List<AudioClip> SEList;
    public int MaxSE = 10;
    public AudioMixer mixer;
    float nowTime = 0.0f;
    float fadenowTime = 0.0f;
    float fadeInTime = 0.0f;
    float fadeOutTime = 0.0f;
    float endTime = 0.0f;
    float loopTime = 0.0f;
    public float keisuu ;
    private GameObject obj;
    private AudioSource bgmSource = null;
    private AudioSource fadebgmSource = null;
    private List<AudioSource> seSources = null;
    private Dictionary<string, AudioClip> bgmDict = null;
    private Dictionary<string, AudioClip> seDict = null;
    private TextAsset _bgmTime;
    private bool fadeOut = false;


    public void Awake()
    {
        if (this != Instance)
        {
            Destroy(this);
            return;
        }

        DontDestroyOnLoad(this.gameObject);

        //create listener
        if (FindObjectsOfType(typeof(AudioListener)).All(o => !((AudioListener)o).enabled))
        {
            this.gameObject.AddComponent<AudioListener>();
        }
        //create audio sources
        this.bgmSource = this.gameObject.AddComponent<AudioSource>();
        this.bgmSource.loop = true;
        this.bgmSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[1];
        this.seSources = new List<AudioSource>();

        //create clip dictionaries
        this.bgmDict = new Dictionary<string, AudioClip>();
        this.seDict = new Dictionary<string, AudioClip>();

        Action<Dictionary<string, AudioClip>, AudioClip> addClipDict = (dict, c) =>
        {
            if (!dict.ContainsKey(c.name))
            {
                dict.Add(c.name, c);
            }
        };

        this.BGMList.ForEach(bgm => addClipDict(this.bgmDict, bgm));
        this.SEList.ForEach(se => addClipDict(this.seDict, se));

        _bgmTime = Resources.Load("SetBgmTime", typeof(TextAsset)) as TextAsset;

    }
    void readBGMText(string fileName)
    {
        
    }


    public void PlaySE(string seName)
    {
        if (!this.seDict.ContainsKey(seName)) throw new ArgumentException(seName + " not found", "seName");

        AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
        if (source == null)
        {
            if (this.seSources.Count >= this.MaxSE)
            {
                Debug.Log("SE AudioSource is full");
                return;
            }

            source = this.gameObject.AddComponent<AudioSource>();
            this.seSources.Add(source);
        }

        source.clip = this.seDict[seName];
        source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
        source.Play();
    }

    public void PlaySE(string seName , int power)
    {
        if (!this.seDict.ContainsKey(seName)) throw new ArgumentException(seName + " not found", "seName");

        AudioSource source = this.seSources.FirstOrDefault(s => !s.isPlaying);
        if (source == null)
        {
            if (this.seSources.Count >= this.MaxSE)
            {
                Debug.Log("SE AudioSource is full");
                return;
            }

            source = this.gameObject.AddComponent<AudioSource>();
            this.seSources.Add(source);
        }

        source.clip = this.seDict[seName];
        source.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
        float volpow = +(float)power * keisuu;
        if(volpow > 20.0f)
        {
            //volpow = 20.0f;
        }
        mixer.SetFloat("SEVolume", Mathf.Lerp(-80, 0, 0.0f + volpow));
        
        source.Play();
    }

    public void StopSE()
    {
        this.seSources.ForEach(s => s.Stop());
    }

    public void PlayBGM(string bgmName , float time)
    {
        if (!this.bgmDict.ContainsKey(bgmName)) throw new ArgumentException(bgmName + " not found", "bgmName");
        if (this.bgmSource.clip == this.bgmDict[bgmName]) return;
        if ( this.bgmSource != null)
        {
            PlayBGM();
            fadeOutTime = fadeInTime;
            fadeInTime = time;
            nowTime = 0.0f;
            fadenowTime = 0.0f;
        }
        else
        {
            fadeInTime = time;
            nowTime = 0.0f;
            fadenowTime = 0.0f;
        }
        this.bgmSource.Stop();
        this.bgmSource.clip = this.bgmDict[bgmName];
        this.readBGMText(bgmName);
        this.bgmSource.Play();
    }

    void PlayBGM()
    {
        obj = new GameObject("fadeObject");
        this.fadebgmSource = obj.AddComponent<AudioSource>();
        this.fadebgmSource.clip = this.bgmSource.clip;
        this.fadebgmSource.outputAudioMixerGroup = this.bgmSource.outputAudioMixerGroup;
        this.fadebgmSource.Play();
        this.fadebgmSource.timeSamples = this.bgmSource.timeSamples;
        this.fadebgmSource.time = this.bgmSource.time;
    }

    void Update()
    {
        if (this.bgmSource != null)
        {
            nowTime += Time.deltaTime;
            this.bgmSource.volume = nowTime / fadeInTime;

            if (nowTime >= fadeInTime)
            {
                this.bgmSource.volume = 1.0f;
                nowTime = this.bgmSource.time;
            }
            if (this.bgmSource.loop == true && endTime != 0.0f)
            {
                if (this.bgmSource.time >= endTime - fadeInTime)
                {
                    this.bgmSource.volume = 1.0f - (this.bgmSource.time - (endTime - fadeInTime)) / fadeInTime;
                    
                }
                if (this.bgmSource.time >= endTime)
                {
                    this.bgmSource.time = loopTime;
                    nowTime = 0;
                }
            }
        }
        if (this.fadebgmSource != null)
        {
           
            fadenowTime += Time.deltaTime;
            this.fadebgmSource.volume = 1.0f-fadenowTime / fadeOutTime;
            if (fadenowTime >= fadeOutTime)
            {
                this.fadebgmSource.volume = 0.0f;
                this.fadebgmSource = null;
                GameObject.Destroy(obj);
            }

        }
        if (fadeOut)
        {
            this.bgmSource.volume = 1.0f - (this.bgmSource.time - fadenowTime) / fadeOutTime;
            if(this.bgmSource.volume <= 0.0f)
            {
                this.bgmSource.volume = 0.0f;
                this.bgmSource = null;
                fadeOut = false;
            }
        }
    }

    public void StopBGM(float fadeouttime)
    {
        fadeOut = true;
        fadeOutTime = fadeouttime;
        fadenowTime = this.bgmSource.time;
        //this.bgmSource.Stop();
        //this.bgmSource.clip = null;
    }

}
