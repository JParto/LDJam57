using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

[CreateAssetMenu(fileName = "Sound", menuName = "ScriptableObjects/Audio/Sound")]
public class SO_Sound : ScriptableObject
{
    public string soundName;
    public AudioMixerGroup mixerGroup;
    [SerializeField] private List<AudioClip> clips;
    [Range(0f, 1f)]
    public float volume = 1f;
    [Range(-3f, 3f)]
    public float pitch = 1f;
    public bool loop = false;

    public AudioClip GetClip(){
        int r = Random.Range(0, clips.Count);
        return clips[r];
    }
}
