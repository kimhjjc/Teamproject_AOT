
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class AudioManager : MonoBehaviour
{
    JsonManager<AudioData> JM = new JsonManager<AudioData>("GameSoundData.json");
    AudioData data;

    [Header("Audio Controle")]
    public Scrollbar music;
    public Scrollbar sound;

    [Header("Audio Sources")]
    public List<AudioSource> bgm;
    public List<AudioSource> player;
    public List<AudioSource> monster;

    private void OnEnable()
    {
        data = new AudioData();
        JM.Load(ref data);
        music.value = data.music_volum;
        sound.value = data.sound_volum;
    }

    private void Start()
    {
        SetAudioVolume(ref bgm, music.value);
        SetAudioVolume(ref player, sound.value);
        SetAudioVolume(ref monster, sound.value);
    }

    private void Update()
    {
        if(SetBgm() || SetSound()) JM.Save(data);
    }
    
    private void SetAudioVolume(ref List<AudioSource> audioList, float volume)
    {
        foreach (AudioSource audio in audioList)
        {
            audio.volume = volume;
        }
    }
    private bool SetBgm() // BackgoundMusic
    {
        if (data.music_volum == music.value) return false;
        data.music_volum = music.value;
        SetAudioVolume(ref bgm, music.value);
        return true;
    }
    private bool SetSound()
    {
        if (data.sound_volum == sound.value) return false;
        data.sound_volum = sound.value;
        SetAudioVolume(ref player, sound.value);
        SetAudioVolume(ref monster, sound.value);
        return true;
    }
    
    public void Play(List<AudioSource> audioList, string playType)
    {
        foreach (AudioSource audio in audioList)
        {
            if (audio.name.Equals(playType)) audio.Play();
        }
    }
}
