using UnityEngine;

public class LayerTransporter : MonoBehaviour
{
    private LayerTransporterManager layerTransporterManager => LayerTransporterManager.instance;
    [SerializeField] public LayerTransporter connectedBlock;
    [SerializeField] public ParallaxState parallaxLayer;
    private PlayerMovement playerMovement;
    [SerializeField] private float maxDistance = 4f; // Minimum distance to trigger transport
    private float distanceToConnectingBlock => Vector2.Distance(transform.position, connectedBlock.transform.position);

    private bool canTransport => distanceToConnectingBlock <= maxDistance;

    private bool toLowerLayer => parallaxLayer < connectedBlock.parallaxLayer;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Color defaultColor = Color.black;
    [SerializeField] private Color highlightColor = Color.green;

    [SerializeField] private ParticleSystem activeParticleSystem;
    private bool activeIsPlaying = false;
    [SerializeField] private ParticleSystem inActiveParticleSystem;
    private bool inActiveIsPlaying = false;

    [SerializeField] private SO_VoidEventChannel toStartTransportFinishedEventChannel;
    [SerializeField] private SO_ParallaxStateEventChannel toParallaxStateEventChannel;
    [SerializeField] private AudioCuePlayer audioCuePlayer;

    void Update()
    {
        if (canTransport)
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = highlightColor;
            }

            if (!activeIsPlaying){
                activeIsPlaying = true;
                activeParticleSystem.Play();
                inActiveIsPlaying = false;
                inActiveParticleSystem.Stop();
            }
        }
        else
        {
            if (spriteRenderer != null)
            {
                spriteRenderer.color = defaultColor;
            }
            if (!inActiveIsPlaying){
                activeIsPlaying = false;
                activeParticleSystem.Stop();
                inActiveIsPlaying = true;
                inActiveParticleSystem.Play();
            }

        }
    }

    public void TriggerTransport()
    {
        if (!canTransport) return;

        if (toLowerLayer) {
            audioCuePlayer.PlaySound("LayerDown");
        } else {
            audioCuePlayer.PlaySound("LayerUp");
        }

        layerTransporterManager.TransportFromLayer(this);

        toParallaxStateEventChannel.RaiseEvent(connectedBlock.parallaxLayer);

        toStartTransportFinishedEventChannel.onEventRaised += TriggerToLayerTransport;
    }

    public void TriggerToLayerTransport()
    {
        layerTransporterManager.TransportToLayer(connectedBlock);
        toStartTransportFinishedEventChannel.onEventRaised -= TriggerToLayerTransport;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            // Debug.Log("Player Entered Trigger: " + collision.name);
            playerMovement.transporter = this;
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        playerMovement = collision.GetComponent<PlayerMovement>();
        if (playerMovement != null && playerMovement.transporter == this)
        {
            // Debug.Log("Player Exited Trigger: " + collision.name);
            playerMovement.transporter = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (connectedBlock != null)
        {
            Gizmos.color = Color.red;
            if (connectedBlock.connectedBlock != null && connectedBlock.connectedBlock == this)
            {
                Gizmos.color = Color.green;
            }
            else if (connectedBlock.connectedBlock != null && connectedBlock.connectedBlock != this)
            {
                Gizmos.color = Color.blue;
            }
            Gizmos.DrawLine(transform.position, connectedBlock.transform.position);
            Gizmos.DrawSphere(connectedBlock.transform.position, 0.2f);
        }
    }
}
