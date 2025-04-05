using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxManager : MonoBehaviour
{
    public static ParallaxManager instance;
    private ParallaxState currentState = ParallaxState.MidGround;

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

    public void OnParallaxChangeToMid()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("fore"));
        midGround.SetParallaxLayer(GetParallaxLayers("mid"), "Ground");
        backGround.SetParallaxLayer(GetParallaxLayers("back"));
    }

    public void OnParallaxChangeToFore()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("mid"), "Ground");
        midGround.SetParallaxLayer(GetParallaxLayers("back"));
        backGround.SetParallaxLayer(GetParallaxLayers("backback"));
    }

    public void OnParallaxChangeToBack()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("forefore"));
        midGround.SetParallaxLayer(GetParallaxLayers("fore"));
        backGround.SetParallaxLayer(GetParallaxLayers("mid"), "Ground");
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
        Debug.Log($"Changing Parallax Layer to: {state}");
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

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        OnParallaxChangeToMid();
    }

    private ParallaxLayerConfig GetParallaxLayers(string name)
    {
        return parallaxConfig.layers.First(x => x.layerName == name);
    }
}
