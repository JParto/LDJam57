using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    [SerializeField] private CinemachinePositionComposer positionComposer;
    private Vector3 originalDamping;

    private void DisableOnTransport(Vector2 _) 
    {
        originalDamping = positionComposer.Damping;

        positionComposer.Damping = Vector3.zero;
    }

    private void EnableOnTransportFinish()
    {
        positionComposer.Damping = originalDamping;
    }

    private void OnEnable()
    {
        transportToPositionEventChannel.onEventRaised += DisableOnTransport;
        transportFinishedEventChannel.onEventRaised += EnableOnTransportFinish;
    }

    private void OnDestroy()
    {
        transportToPositionEventChannel.onEventRaised -= DisableOnTransport;
        transportFinishedEventChannel.onEventRaised -= EnableOnTransportFinish;
    }
}
