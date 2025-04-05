using UnityEngine;

public class PlayerLayerTransporter : MonoBehaviour
{
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    private void Transport(Vector2 position)
    {
        // disable player movement / input

        // set to position
        transform.position = position;

        // enable player movement / input

        // raise event to notify that the transport is finished
        transportFinishedEventChannel.RaiseEvent();
    }

    private void OnEnable()
    {
        transportToPositionEventChannel.onEventRaised += Transport;
    }

    private void OnDestroy()
    {
        transportToPositionEventChannel.onEventRaised -= Transport;
    }
}