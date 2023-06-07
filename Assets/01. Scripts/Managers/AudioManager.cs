using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioSource BGMPlayer, EffectPlayer;

    public AudioClip SettingBGM, GameBGM, TutorialBGM, RankingBGM;
    public AudioClip EndRepEffect, ClickButtonEffect;

    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }

    private void Start() {
        BGMPlayer = GetComponent<AudioSource>();
        EffectPlayer = transform.Find("Sound Effect").GetComponent<AudioSource>();

        BGMPlayer.clip = SettingBGM;
    }

    public void PlaySettingBGM()
    {
        if(BGMPlayer.clip == SettingBGM) return;
        BGMPlayer.clip = SettingBGM;
        BGMPlayer.Play();
    }

    public void PlayGameBGM()
    {
        if(BGMPlayer.clip == GameBGM) return;
        BGMPlayer.clip = GameBGM;
        BGMPlayer.Play();
    }

    public void PlayTutorialBGM()
    {
        if(BGMPlayer.clip == TutorialBGM) return;
        BGMPlayer.clip = TutorialBGM;
        BGMPlayer.Play();
    }

    public void PlayRankingBGM()
    {
        if(BGMPlayer.clip == RankingBGM) return;
        BGMPlayer.clip = RankingBGM;
        BGMPlayer.Play();
    }

    public void PlayEndRepEffect()
    {
        // if(EffectPlayer.clip == EndRepEffect) return;
        EffectPlayer.clip = EndRepEffect;
        EffectPlayer.Play();
    }

    public void PlayClickButtonEffect()
    {
        EffectPlayer.clip = ClickButtonEffect;
        EffectPlayer.Play();
    }
}
