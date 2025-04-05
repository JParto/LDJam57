using UnityEngine;

public class LayerTransporter : MonoBehaviour
{
    [SerializeField] public LayerTransporter connectedBlock;
    [SerializeField] private LayerTransporterManager layerTransporterManager;
    [SerializeField] private ParallaxManager parallaxManager;
    [SerializeField] public ParallaxState parallaxLayer;
    private PlayerMovement playerMovement;

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
