using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Audio/AudioCue Event Channel")]
public class SO_AudioCueEventChannel : SO_DescriptionBase
{
    public AudioCuePlayAction OnAudioCuePlayRequested;
    public AudioCueStopAction OnAudioCueStopRequested;
    public AudioCueFinishAction OnAudioCueFinishRequested;

    public SoundEmitter RaisePlayEvent(SO_Sound sound, Vector3 positionInSpace = default)
    {
        SoundEmitter emitter = null;

        if (OnAudioCuePlayRequested != null)
        {
            emitter = OnAudioCuePlayRequested.Invoke(sound, positionInSpace);
        }
        else
        {
            Debug.LogWarning("An AudioCue play event was requested  for " + sound.name +", but nobody picked it up. " +
                "Check why there is no AudioManager already loaded, " +
                "and make sure it's listening on this AudioCue Event channel.");
        }

        return emitter;
    }

    public bool RaiseStopEvent(SoundEmitter emitter)
    {
        bool requestSucceed = false;

        if (OnAudioCueStopRequested != null)
        {
            requestSucceed = OnAudioCueStopRequested.Invoke(emitter);
        }
        else
        {
            Debug.LogWarning("An AudioCue stop event was requested, but nobody picked it up. " +
                "Check why there is no AudioManager already loaded, " +
                "and make sure it's listening on this AudioCue Event channel.");
        }

        return requestSucceed;
    }

    public bool RaiseFinishEvent(SoundEmitter emitter)
    {
        bool requestSucceed = false;

        if (OnAudioCueStopRequested != null)
        {
            requestSucceed = OnAudioCueFinishRequested.Invoke(emitter);
        }
        else
        {
            Debug.LogWarning("An AudioCue finish event was requested, but nobody picked it up. " +
                "Check why there is no AudioManager already loaded, " +
                "and make sure it's listening on this AudioCue Event channel.");
        }

        return requestSucceed;
    }
}

public delegate SoundEmitter AudioCuePlayAction(SO_Sound audioCue, Vector3 positionInSpace);
public delegate bool AudioCueStopAction(SoundEmitter emitter);
public delegate bool AudioCueFinishAction(SoundEmitter emitter);