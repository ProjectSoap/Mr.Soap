using UnityEngine;
using UnityEngine.Audio;
using System.Collections;
using System.Collections.Generic;

public class SoundPlayer {

    GameObject soundPlayerObj;
    AudioSource audioSource;
    AudioMixer mixer;
    Dictionary<string, AudioClipInfo> audioClips = new Dictionary<string, AudioClipInfo>();
    BGMPlayer curBGMPlayer;
    BGMPlayer fadeOutBGMPlayer;


    // AudioClip information
    class AudioClipInfo
    {
        public string resourceName;
        public string name;
        public AudioClip clip;

        public AudioClipInfo(string resourceName, string name)
        {
            this.resourceName = resourceName;
            this.name = name;
        }
    }

    public SoundPlayer() {
        mixer = (AudioMixer)Resources.Load("Sound/NewAudioMixer");
        //mixer.FindMatchingGroups("Sound/NewAudioMixer");
        audioClips.Add("se001", new AudioClipInfo("Sound/SE/pi", "se001"));
        //audioClips.Add("bgm000", new AudioClipInfo("sample001", "bgm000"));
        audioClips.Add("bgm001", new AudioClipInfo("Sound/BGM/STARTDASH", "bgm001"));
       // audioSource.outputAudioMixerGroup = 
    }

    public bool playSE( string seName ) {
        if ( audioClips.ContainsKey( seName ) == false )
            return false; // not register

        AudioClipInfo info = audioClips[ seName ];

        // Load
        if ( info.clip == null )
            info.clip = (AudioClip)Resources.Load( info.resourceName );

        if ( soundPlayerObj == null ) {
            soundPlayerObj = new GameObject( "SoundPlayer" ); 
            audioSource = soundPlayerObj.AddComponent<AudioSource>();
        }

        // Play SE
        audioSource.outputAudioMixerGroup = mixer.FindMatchingGroups("Master")[2];
        audioSource.PlayOneShot( info.clip );

        return true;
    }

    public void playBGM(string bgmName, float fadeTime)
    {
        // destory old BGM
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.destory();

        // change to fade out for current BGM
        if (curBGMPlayer != null)
        {
            curBGMPlayer.stopBGM(fadeTime);
            fadeOutBGMPlayer = curBGMPlayer;
        }

        // play new BGM
        if (audioClips.ContainsKey(bgmName) == false)
        {
            // null BGM
            curBGMPlayer = new BGMPlayer();
        }
        else
        {
            curBGMPlayer = new BGMPlayer(audioClips[bgmName].resourceName);
            curBGMPlayer.playBGM(fadeTime);
        }
    }

    public void playBGM()
    {
        if (curBGMPlayer != null && curBGMPlayer.hadFadeOut() == false)
            curBGMPlayer.playBGM();
        if (fadeOutBGMPlayer != null && fadeOutBGMPlayer.hadFadeOut() == false)
            fadeOutBGMPlayer.playBGM();
    }

    public void pauseBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.pauseBGM();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.pauseBGM();
    }

    public void stopBGM(float fadeTime)
    {
        if (curBGMPlayer != null)
            curBGMPlayer.stopBGM(fadeTime);
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.stopBGM(fadeTime);
    }

    public void updateBGM()
    {
        if (curBGMPlayer != null)
            curBGMPlayer.update();
        if (fadeOutBGMPlayer != null)
            fadeOutBGMPlayer.update();
    }
    
}

