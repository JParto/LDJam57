using UnityEngine;

public class LayerTransporter : MonoBehaviour
{
    private LayerTransporterManager layerTransporterManager => LayerTransporterManager.instance;
    private ParallaxManager parallaxManager => ParallaxManager.instance;
    [SerializeField] public LayerTransporter connectedBlock;
    [SerializeField] public ParallaxState parallaxLayer;
    private PlayerMovement playerMovement;

    void Awake()
    {
        if (layerTransporterManager == null)
        {
            Debug.LogError("LayerTransporterManager instance is null. Make sure it is assigned in the inspector.");
        }
        if (parallaxManager == null)
        {
            Debug.LogError("ParallaxManager instance is null. Make sure it is assigned in the inspector.");
        }
    } 

    public void DisableParallax()
    {
        parallaxManager.DisableParallax();
    }

    public void TriggerTransport()
    {
        layerTransporterManager.TransportToLayer(connectedBlock);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Player Entered Trigger: " + collision.name);
            playerMovement.transporter = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Player Exited Trigger: " + collision.name);
            playerMovement.transporter = null;
        }
    }
}
