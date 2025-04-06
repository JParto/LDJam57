using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    [Header("Audio control")]
    [SerializeField] private AudioMixer audioMixer = default;
    [Range(0f, 1f)]
    [SerializeField] private float _masterVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _musicVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField] private float _sfxVolume = 1f;

    [Header("Music")]
    private SoundEmitter _musicEmitter;
    [SerializeField] private SO_Sound _music;


    [Header("Sound Emitter Pool")]
    [SerializeField] private SO_SoundEmitterPool _pool;
    [SerializeField] private int _initialPoolSize;

    [Header("Listening on channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to play SFXs")]
    [SerializeField] private SO_AudioCueEventChannel _SFXEventChannel = default;

    [Header("Audio level channels")]
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change SFXs volume")]
    [SerializeField] private SO_FloatEventChannel _SFXVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Music volume")]
    [SerializeField] private SO_FloatEventChannel _musicVolumeEventChannel = default;
    [Tooltip("The SoundManager listens to this event, fired by objects in any scene, to change Master volume")]
    [SerializeField] private SO_FloatEventChannel _masterVolumeEventChannel = default;


    private void Awake(){
        if (_pool.HasBeenPrewarmed){
            Destroy(gameObject);
        } else {
            _pool.Prewarm(_initialPoolSize);
            _pool.SetParent(this.transform);
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start(){
        // if (_music != null)
        //     PlayMusic(_music);
    }

    private void OnEnable()
    {
        _SFXEventChannel.OnAudioCuePlayRequested += PlaySound;

        _masterVolumeEventChannel.onEventRaised += ChangeMasterVolume;
        _musicVolumeEventChannel.onEventRaised += ChangeMusicVolume;
        _SFXVolumeEventChannel.onEventRaised += ChangeSFXVolume;

    }

    private void OnDestroy()
    {
        _SFXEventChannel.OnAudioCuePlayRequested -= PlaySound;

        _masterVolumeEventChannel.onEventRaised -= ChangeMasterVolume;
        _musicVolumeEventChannel.onEventRaised -= ChangeMusicVolume;
        _SFXVolumeEventChannel.onEventRaised -= ChangeSFXVolume;

    }

    public void PlayMusic(SO_Sound music){
        _musicEmitter = _pool.Request();

        _musicEmitter.PlaySound(music, transform.position);
    }

    public void PlayGlobalMusic(bool play){
        if (play){
            if (_musicEmitter == null)
                _musicEmitter = _pool.Request();
            
            _musicEmitter.PlaySound(_music, transform.position);
        } else {
            _musicEmitter.Stop();
        }
    }

    public SoundEmitter PlaySound(SO_Sound sound, Vector3 position = default){
        SoundEmitter emitter = _pool.Request();

        emitter.PlaySound(sound, position);
        emitter.OnSoundFinishedPlaying += OnSoundEmitterFinishedPlaying;

        return emitter;
    }

    public bool FinishSound(SoundEmitter emitter){
        Debug.LogWarning("Not yet implemented: PoolTest/FinishSound");
        _pool.Return(emitter);
        
        return true;
    }

    public bool StopAudioCue(SoundEmitter emitter){
        Debug.LogWarning("Not yet implemented: PoolTest/StopAudioCue");

        _pool.Return(emitter);
        return true;
    }

    public void OnSoundEmitterFinishedPlaying(SoundEmitter emitter){
        // emitter.Stop();
        // Debug.Log("Called");
        _pool.Return(emitter);
        emitter.OnSoundFinishedPlaying -= OnSoundEmitterFinishedPlaying;
    }

    #region MixerVolume
    public void ChangeMasterVolume(float newVolume)
    {
        _masterVolume = newVolume;
        SetGroupVolume("Volume_Master", _masterVolume);
    }
    public void ChangeMusicVolume(float newVolume)
    {
        _musicVolume = newVolume;
        SetGroupVolume("Volume_Music", _musicVolume);
    }
    public void ChangeSFXVolume(float newVolume)
    {
        _sfxVolume = newVolume;
        SetGroupVolume("Volume_SFX", _sfxVolume);
    }

    public void SetGroupVolume(string parameterName, float normalizedVolume)
    {
        bool volumeSet = audioMixer.SetFloat(parameterName, NormalizedToMixerValue(normalizedVolume));
        if (!volumeSet)
            Debug.LogError("The AudioMixer parameter was not found");
    }

    public float GetGroupVolume(string parameterName)
    {
        if (audioMixer.GetFloat(parameterName, out float rawVolume))
        {
            return MixerValueToNormalized(rawVolume);
        }
        else
        {
            Debug.LogError("The AudioMixer parameter was not found");
            return 0f;
        }
    }

    // Both MixerValueNormalized and NormalizedToMixerValue functions are used for easier transformations
    // when using UI sliders normalized format
    private float MixerValueToNormalized(float mixerValue)
    {
        // We're assuming the range [-80dB to 0dB] becomes [0 to 1]
        return Mathf.Exp(mixerValue/20f);
        // return 1f + (mixerValue / 80f);
    }
    private float NormalizedToMixerValue(float normalizedValue)
    {
        // We're assuming the range [0 to 1] becomes [-80dB to 0dB]
        // This doesn't allow values over 0dB
        return Mathf.Log(normalizedValue) * 20;
        // float targetVolume = Mathf.Log(normalizedValue) * 20;
        // return (normalizedValue - 1f) * 80f;
    }
        
    #endregion
}
