using UnityEngine;
using System;
using System.Collections;
using Unity.Mathematics;

public enum SoundType
{
    PLAYER_WALK,
    EFFECT,
    AMBIENT,
    MAINMENU,
}

[RequireComponent(typeof(AudioSource))]
public class Soundmanager : MonoBehaviour
{
    public float musicVolume;
    public float sfxVolume;
    public SoundList[] soundList;
    public static Soundmanager instance;
    public AudioSource musicSource; // For background/ambient
    public AudioSource sfxSource;   // For sound effects

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        if (musicSource == null || sfxSource == null)
        {
            var sources = GetComponents<AudioSource>();
            if (sources.Length > 1)
            {
                musicSource = sources[0];
                sfxSource = sources[1];
            }
            else
            {
                musicSource = sfxSource = GetComponent<AudioSource>();
            }
        }
    }
    public void PlaySound(SoundType sound, AudioSource audioSource ,float volume = 1)
    {
        if (instance == null || audioSource == null) return;
        int index = (int)sound;
        if (instance.soundList == null || index >= instance.soundList.Length) return;

        var clips = instance.soundList[index].sounds;
        if (clips == null || clips.Length == 0) return;

        var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlaySpecificClip(AudioClip clip, AudioSource audioSource, float volume = 1)
    {
        if (instance == null || audioSource == null) return;
        audioSource.PlayOneShot(clip, volume);
    }
    public void PlaySpecificClipInSoundType(SoundType soundType, int clipIndex, AudioSource audioSource, float volume = 1)
    {
        var clip = (soundList[(int)soundType].sounds)[clipIndex];
        if (instance == null || audioSource == null || clip == null) return;
    }
    public void PlayOnBackGroundConstantly(SoundType sound, AudioSource audioSource, float volume = 0.5f)
    {
        if (instance == null || instance.musicSource == null) return;
        int index = (int)sound;
        if (instance.soundList == null || index >= instance.soundList.Length) return;

        var clips = instance.soundList[index].sounds;
        if (clips == null || clips.Length == 0) return;

        var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
        audioSource.clip = clip;
        audioSource.PlayOneShot(clip, volume);
    }

    public void PlayRandomLoop(SoundType sound,AudioSource audioSource, float volume = 0.5f)
    {
        StartCoroutine(PlayRandomCoroutine(sound, audioSource ,volume));
    }

    private IEnumerator PlayRandomCoroutine(SoundType sound,AudioSource audioSource, float volume)
    {
        while (true)
        {
            int index = (int)sound;
            if (soundList == null || index >= soundList.Length) yield break;

            var clips = soundList[index].sounds;
            if (clips == null || clips.Length == 0) yield break;

            var clip = clips[UnityEngine.Random.Range(0, clips.Length)];
            audioSource.clip = clip;
            musicSource.volume = volume;
            musicSource.loop = false;
            musicSource.Play();

            yield return new WaitForSeconds(clip.length);
        }
    }
}

[Serializable]
public struct SoundList
{
    [SerializeField] public string name;
    [SerializeField] public AudioClip[] sounds;
}