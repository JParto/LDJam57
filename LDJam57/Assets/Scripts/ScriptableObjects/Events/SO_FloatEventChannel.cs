using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Float Event Channel")]
public class SO_FloatEventChannel : SO_DescriptionBase
{
    public UnityAction<float> onEventRaised;

    public void RaiseEvent(float value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}
