using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Splines;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Spline Event Channel")]
public class SO_SplineEventChannel : SO_DescriptionBase
{
    public UnityAction<Spline> onEventRaised;

    public void RaiseEvent(Spline value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}