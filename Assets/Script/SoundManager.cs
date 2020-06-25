
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum PLAYERSOUNDTYPE
{
    GAMEOVER,
    GAMECLEAR,
    ATTACK,
    DEMAGE,
    DEAD
}

[System.Serializable]
public struct SoundData
{
    [SerializeField]
    public float music_volum;
    public float sound_volum;
}

public class SoundManager : MonoBehaviour
{
    //[Header("Audio File")]
    JsonManager<SoundData> JM = new JsonManager<SoundData>("GameSoundData.json");
    SoundData data;


    [Header("Audio Controle")]
    public Scrollbar music;
    public Scrollbar sound;
    bool ismusicUpdate = false;
    bool isSoundUpdate = false;

    [Header("Audio Sources")]
    public AudioSource bgm;
    public List<AudioSource> sounds;
    
    private void Start()
    {
        data = new SoundData();
        JM.Load(ref data);
        music.value = data.music_volum;
        sound.value = data.sound_volum;
    }

    private void Update()
    {
        if (bgm.volume != music.value)
        {
            ismusicUpdate = true;
            bgm.volume = music.value;
            data.music_volum = music.value;
        }

        foreach (AudioSource s in sounds)
        {
            isSoundUpdate = (s.volume != sound.value) ? true : false;
            if (isSoundUpdate)
            {
                s.volume = sound.value;
                data.sound_volum = sound.value;
            }
        }

        if ((ismusicUpdate == true) || (isSoundUpdate == true))
        {
            JM.Save(data);
            ismusicUpdate = false;
            isSoundUpdate = false;
        }
    }

    public void PlayPlayerSound(PLAYERSOUNDTYPE type)
    {
        switch(type)
        {
            case PLAYERSOUNDTYPE.GAMEOVER:
                sounds[0].Play();
                break;
            case PLAYERSOUNDTYPE.GAMECLEAR:
                sounds[1].Play();
                break;
            case PLAYERSOUNDTYPE.ATTACK:
                sounds[2].Play();
                break;
            case PLAYERSOUNDTYPE.DEMAGE:
                sounds[3].Play();
                break;
            case PLAYERSOUNDTYPE.DEAD:
                sounds[4].Play();
                break;
        }
    }
}
