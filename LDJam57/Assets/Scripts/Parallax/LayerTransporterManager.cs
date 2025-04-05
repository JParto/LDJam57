using UnityEngine;

public class LayerTransporterManager : MonoBehaviour
{
    public static LayerTransporterManager instance;
    private ParallaxManager parallaxManager => ParallaxManager.instance;
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;

    private ParallaxState toLayer;

    void Awake()
    {
        if (!instance)
        {
            Debug.Log("LayerTransporterManager instance created.");
            instance = this;
        }
        else
        {
            Debug.LogWarning("Multiple instances of LayerTransporterManager detected. Destroying the duplicate instance.");
            Destroy(gameObject);
        }
    }

    public void TransportToLayer(LayerTransporter toTransporter)
    {
        Debug.Log($"Transporting to layer: {toTransporter.parallaxLayer}");
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
        Debug.Log($"Changing Parallax Layer to: {toLayer}");
        parallaxManager.ChangeParallaxLayer(toLayer);
    }

    private void OnEnable()
    {
        Debug.Log("Enabling LayerTransporterManager instance.");
        transportFinishedEventChannel.onEventRaised += EndTransport;
    }

    private void OnDisable()
    {
        Debug.Log("Destroying LayerTransporterManager instance.");
        transportFinishedEventChannel.onEventRaised -= EndTransport;
    }
}
