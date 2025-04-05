using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Parallax State Event Channel")]
public class SO_ParallaxStateEventChannel : SO_DescriptionBase
{
    public UnityAction<ParallaxState> onEventRaised;

    public void RaiseEvent(ParallaxState value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}