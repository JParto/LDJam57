using UnityEngine;

public class LayerTransporterManager : MonoBehaviour
{
    [SerializeField] private ParallaxManager parallaxManager;
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    private ParallaxState toLayer;

    public void TransportToLayer(LayerTransporter toTransporter)
    {
        // disable parallax
        parallaxManager.DisableParallax();

        // set the state to the new layer
        toLayer = toTransporter.parallaxLayer;

        // start the transportation process
        transportToPositionEventChannel.RaiseEvent(toTransporter.transform.position);
    }

    public void EndTransport()
    {
        // change and enable parallax 
        parallaxManager.ChangeParallaxLayer(toLayer);
    }

    public void OnEnable()
    {
        transportFinishedEventChannel.onEventRaised += EndTransport;
    }
    public void OnDestroy()
    {
        transportFinishedEventChannel.onEventRaised -= EndTransport;
    }
}
