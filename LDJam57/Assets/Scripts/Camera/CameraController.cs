using Unity.Cinemachine;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public PlayerLayerTransporter playerLayerTransporter;
    [SerializeField] private SO_PositionEventChannel transportFromPositionEventChannel;
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel toStartTransportFinishedEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    [SerializeField] private CinemachinePositionComposer positionComposer;
    private Vector3 originalDamping;
    private float transportStartTime;
    private float transportDuration => playerLayerTransporter.toTransportStartPositionDuration;
    private bool toStart = false;

    private void DisableOnTransport(Vector2 _) 
    {
    }

    private void EnableOnTransportFinish()
    {
        positionComposer.Damping = originalDamping;
    }

    private void StartRemovingDamping(Vector2 _)
    {
        originalDamping = positionComposer.Damping;
        toStart = true;
        transportStartTime = Time.time;
    }

    private void ReachedStartPosition()
    {
        toStart = false;
        positionComposer.Damping = Vector3.zero;

    }

    private void Update()
    {
        if (!toStart) return;
        
        if (transportStartTime + transportDuration < Time.time){
            return;
        }
        positionComposer.Damping = Vector3.Lerp(originalDamping, Vector3.zero, (Time.time - transportStartTime) / playerLayerTransporter.toTransportStartPositionDuration);
    }

    private void OnEnable()
    {
        transportFromPositionEventChannel.onEventRaised += StartRemovingDamping;
        // transportToPositionEventChannel.onEventRaised += DisableOnTransport;
        toStartTransportFinishedEventChannel.onEventRaised += ReachedStartPosition;
        transportFinishedEventChannel.onEventRaised += EnableOnTransportFinish;
    }

    private void OnDestroy()
    {
        transportFromPositionEventChannel.onEventRaised -= StartRemovingDamping;
        // transportToPositionEventChannel.onEventRaised -= DisableOnTransport;
        toStartTransportFinishedEventChannel.onEventRaised -= ReachedStartPosition;
        transportFinishedEventChannel.onEventRaised -= EnableOnTransportFinish;
    }
}
