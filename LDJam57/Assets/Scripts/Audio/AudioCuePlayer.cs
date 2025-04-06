using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioCuePlayer : MonoBehaviour
{
    [Header("Broadcast on channels")]
    [Tooltip("The channel to send requests and other stuff")]
    [SerializeField] private SO_AudioCueEventChannel _SFXEventChannel = default;

    [SerializeField] private List<SO_Sound> _sounds;

    private SO_Sound _default_sound => _sounds[0];

    public SoundEmitter PlayDefaultSound(){
        SoundEmitter se = _SFXEventChannel.RaisePlayEvent(_default_sound, transform.position);
        return se;
    }

    // public void PlaySound(string soundName){
    //     SoundEmitter se = audioManager.RequestSoundEmitter();
    //     SO_Sound sound = _sounds.Find(x => x.name == soundName);

    //     se.PlaySound(sound, transform.position);
    // }

    public void PlaySound(string soundName){
        SO_Sound sound = _sounds.Find(x => x.name == soundName);
        if (!sound){
            Debug.LogWarningFormat("Sound '{0}' not found on this player", soundName);
            return;
        }
        SoundEmitter se = _SFXEventChannel.RaisePlayEvent(sound, transform.position);
    }
}
