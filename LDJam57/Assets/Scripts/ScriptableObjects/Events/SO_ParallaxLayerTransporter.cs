using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "ScriptableObjects/Events/Layer Transporter Event Channel")]
public class SO_LayerTransporter : SO_DescriptionBase
{
    public UnityAction<LayerTransporter> onEventRaised;

    public void RaiseEvent(LayerTransporter value){
        if(onEventRaised != null){
            onEventRaised.Invoke(value);
        }
    }
}