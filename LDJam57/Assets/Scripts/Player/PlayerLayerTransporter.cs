using UnityEngine;

public class PlayerLayerTransporter : MonoBehaviour
{
    [SerializeField] private SO_PositionEventChannel transportToPositionEventChannel;
    [SerializeField] private SO_VoidEventChannel transportFinishedEventChannel;
    [SerializeField] private AnimationCurve transportCurve;
    [SerializeField] private float transportDuration = 3f; // Duration of the transport animation

    private bool isTransporting = false;
    private float transportStartTime;
    private Vector2 startPosition;
    private Vector2 targetPosition;

    private void Transport(Vector2 position)
    {
        // disable player movement / input
        GetComponent<PlayerMovement>().enabled = false;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic; // Disable Rigidbody2D to prevent physics interactions

        // set to position
        startPosition = transform.position;
        targetPosition = position;

        transportStartTime = Time.time;
        isTransporting = true;
        // enable player movement / input
        GetComponent<PlayerMovement>().enabled = true;
        GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic; // Enable Rigidbody2D

    }

    private void Update()
    {
        if (!isTransporting) return;

        if (transportStartTime + transportDuration < Time.time)
        {
            // Transport finished, reset the state
            isTransporting = false;
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

    private void OnEnable()
    {
        transportToPositionEventChannel.onEventRaised += Transport;
    }

    private void OnDestroy()
    {
        transportToPositionEventChannel.onEventRaised -= Transport;
    }
}