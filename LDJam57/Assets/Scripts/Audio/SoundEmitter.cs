using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(AudioSource))]
public class SoundEmitter : MonoBehaviour
{
    private AudioSource _audioSource;
    public event UnityAction<SoundEmitter> OnSoundFinishedPlaying;

    private void Awake(){
        _audioSource = GetComponent<AudioSource>();
        _audioSource.playOnAwake = false;
    }

    public void PlaySound(SO_Sound sound, Vector3 position){
        _audioSource.clip = sound.GetClip();
        _audioSource.outputAudioMixerGroup = sound.mixerGroup;
        _audioSource.volume = sound.volume;
        _audioSource.loop = sound.loop;
        _audioSource.pitch = sound.pitch;
        _audioSource.transform.position = position;
        _audioSource.Play();

        if (!sound.loop){
            StartCoroutine(FinishedPlaying(_audioSource.clip.length));
        }
    }

    public void Resume(){
        _audioSource.Play();
    }

    public void Pause(){
        _audioSource.Stop();
    }

    public void Stop(){
        _audioSource.Stop();
    }

    public void ChangePitch(float val){
        _audioSource.pitch = val;
    }

    IEnumerator FinishedPlaying(float clipLength)
    {
        yield return new WaitForSeconds(clipLength);

        NotifyFinished();
    }

    private void NotifyFinished(){
        // Debug.Log("Finished");
        OnSoundFinishedPlaying.Invoke(this);
    }
}
