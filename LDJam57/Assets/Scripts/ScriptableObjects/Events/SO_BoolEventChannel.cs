using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Bool Event Channel")]
public class SO_BoolEventChannel : SO_DescriptionBase
{
    public UnityAction<bool> onEventRaised;

    public void RaiseEvent(bool value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}