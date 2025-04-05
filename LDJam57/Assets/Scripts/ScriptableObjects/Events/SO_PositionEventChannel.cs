using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Position Event Channel")]
public class SO_PositionEventChannel : SO_DescriptionBase
{
    public UnityAction<Vector2> onEventRaised;

    public void RaiseEvent(Vector2 value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}