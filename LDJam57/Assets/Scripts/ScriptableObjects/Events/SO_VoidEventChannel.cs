using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Void Event Channel")]
public class SO_VoidEventChannel : SO_DescriptionBase
{
    public UnityAction onEventRaised;

    public void RaiseEvent(){
        if(onEventRaised != null){
            onEventRaised.Invoke();
        }
    }
}