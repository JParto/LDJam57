using UnityEngine;

public class PlayerSpriteToLayer : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private SO_ParallaxStateEventChannel parallaxStateEventChannel;
    public int ForeGroundOrder = 18;
    public int MidGroundOrder = 13;
    public int BackGroundOrder = 8;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }   
        if (parallaxStateEventChannel == null)
        {
            Debug.LogError("SO_ParallaxStateEventChannel is not assigned in the inspector.");
        }
    }

    private void SetOrder(int order)
    {
        spriteRenderer.sortingOrder = order;
    }

    private void OnParallaxStateChanged(ParallaxState state)
    {
        switch (state)
        {
            case ParallaxState.ForeGround:
                SetOrder(ForeGroundOrder);
                break;
            case ParallaxState.MidGround:
                SetOrder(MidGroundOrder);
                break;
            case ParallaxState.BackGround:
                SetOrder(BackGroundOrder);
                break;
            default:
                Debug.LogError("Unknown Parallax State: " + state);
                break;
        }
    }

    void OnEnable()
    {
        parallaxStateEventChannel.onEventRaised += OnParallaxStateChanged;
    }
    void OnDisable()
    {
        parallaxStateEventChannel.onEventRaised -= OnParallaxStateChanged;
    }
}