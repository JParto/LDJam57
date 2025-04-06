using UnityEngine;

public class PlayerLayerTransporter : MonoBehaviour
{
    [SerializeField] private SO_PositionEventChannel transportFromPositionEventChannel;
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;
    [SerializeField] private SO_VoidEventChannel toStartTransportFinishedEventChannel;
    [SerializeField] private AnimationCurve transportCurve;
    [SerializeField] private float transportDuration = 3f; // Duration of the transport animation
    [SerializeField] public float toTransportStartPositionDuration = 0.2f; // Duration of the transport animation

    private bool toStart = false;
    private bool toEnd = false;
    private bool isTransporting = false;
    private float transportStartTime;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private void Transport(Vector2 position)
    {
        // set to position
        startPosition = transform.position;
        targetPosition = position;

        transportStartTime = Time.time;
        isTransporting = true;
        toEnd = true;

        // enable player movement / input
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Enable Rigidbody2D

    }

    private void TransportFrom(Vector2 position)
    {
        // disable player movement / input
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Disable Rigidbody2D to prevent physics interactions

        startPosition = transform.position;
        targetPosition = position;

        transportStartTime = Time.time;
        isTransporting = true;
        toStart = true;
    }

    private void Update()
    {
        if (!isTransporting) return;

        // do the ToStart or ToEnd transport
        if (toStart)
        {
            ToStart();
            return;
        }

        if (toEnd)
        {
            ToEnd();
            return;
        }
    }

    private void ToEnd()
    {
        if (transportStartTime + transportDuration < Time.time)
        {
            isTransporting = false;
            toEnd = false;
            transform.position = targetPosition; // Ensure the player ends up at the target position
            transportFinishedEventChannel.RaiseEvent(); // Notify that the transport is finished
            return;
        }

        // Check if the player is in the middle of a transport
        if (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Calculate the transport progress based on time and the animation curve
            float t = Mathf.Clamp01((Time.time - transportStartTime) / transportDuration);
            float curveValue = transportCurve.Evaluate(t);

            // Move the player towards the target position using the animation curve
            transform.position = Vector2.Lerp(startPosition, targetPosition, curveValue);
        }

    }

    private void ToStart(){
        if (transportStartTime + toTransportStartPositionDuration < Time.time)
        {
            transform.position = targetPosition; // Ensure the player ends up at the target position
            // notify that the transport to the other layer can start
            toStart = false;
            toStartTransportFinishedEventChannel.RaiseEvent();
            return;
        }

        // Check if the player is in the middle of a transport
        if (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            // Calculate the transport progress based on time and the animation curve
            float t = Mathf.Clamp01((Time.time - transportStartTime) / toTransportStartPositionDuration);
            float curveValue = transportCurve.Evaluate(t);

            // Move the player towards the target position using the animation curve
            transform.position = Vector2.Lerp(startPosition, targetPosition, curveValue);
        }
    }

    private void OnEnable()
    {
        transportFromPositionEventChannel.onEventRaised += TransportFrom;
        transportToPositionEventChannel.onEventRaised += Transport;
    }

    private void OnDestroy()
    {
        transportFromPositionEventChannel.onEventRaised -= TransportFrom;
        transportToPositionEventChannel.onEventRaised -= Transport;
    }
}