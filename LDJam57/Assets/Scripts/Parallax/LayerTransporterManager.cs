using UnityEngine;

public class LayerTransporterManager : MonoBehaviour
{
    public static LayerTransporterManager instance;
    private ParallaxManager parallaxManager => ParallaxManager.instance;
    [SerializeField] private SO_PositionEventChannel transportFromPositionEventChannel;
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    private ParallaxState toLayer;

    void Awake()
    {
        if (!instance)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void TransportFromLayer(LayerTransporter fromTransporter)
    {
        transportFromPositionEventChannel.RaiseEvent(fromTransporter.transform.position);
    }

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

    private void OnEnable()
    {
        transportFinishedEventChannel.onEventRaised += EndTransport;
    }

    private void OnDisable()
    {
        transportFinishedEventChannel.onEventRaised -= EndTransport;
    }
}
