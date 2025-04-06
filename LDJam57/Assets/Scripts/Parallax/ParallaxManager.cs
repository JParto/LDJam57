using System.Linq;
using UnityEngine;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager instance;
    public ParallaxState currentState = ParallaxState.MidGround;
    [SerializeField] private SO_ParallaxStateEventChannel parallaxStateEventChannel;

    [Header("Parallax Config")]
    [SerializeField] private ParallaxConfig parallaxConfig;

    [Header("Parallax Objects")]
    [SerializeField] private Parallax foreGround;
    [SerializeField] private Parallax midGround;
    [SerializeField] private Parallax backGround;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (parallaxConfig == null)
        {
            Debug.LogError("ParallaxConfig is not assigned in the inspector.");
        }

        if (foreGround == null || midGround == null || backGround == null)
        {
            Debug.LogError("One or more Parallax objects are not assigned in the inspector.");
        }

        OnParallaxChangeToMid();
    }

    public void OnParallaxChangeToMid()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("fore_fore"));
        midGround.SetParallaxLayer(GetParallaxLayers("mid_mid"), "Ground");
        backGround.SetParallaxLayer(GetParallaxLayers("back_back"));
    }

    public void OnParallaxChangeToFore()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("forefore_fore"), "Ground");
        midGround.SetParallaxLayer(GetParallaxLayers("fore_mid"));
        backGround.SetParallaxLayer(GetParallaxLayers("mid_back"));
    }

    public void OnParallaxChangeToBack()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("mid_fore"));
        midGround.SetParallaxLayer(GetParallaxLayers("back_mid"));
        backGround.SetParallaxLayer(GetParallaxLayers("backback_back"), "Ground");
    }

    public void DisableParallax()
    {
        foreGround.DisableParallax();
        midGround.DisableParallax();
        backGround.DisableParallax();
    }

    public void ChangeParallaxLayer(ParallaxState state)
    {
        currentState = state;
        parallaxStateEventChannel.RaiseEvent(currentState);
        switch (state)
        {
            case ParallaxState.ForeGround:
                OnParallaxChangeToFore();
                break;
            case ParallaxState.MidGround:
                OnParallaxChangeToMid();
                break;
            case ParallaxState.BackGround:
                OnParallaxChangeToBack();
                break;
        }
    }

    private ParallaxLayerConfig GetParallaxLayers(string name)
    {
        return parallaxConfig.layers.First(x => x.layerName == name);
    }
}
