using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxManager : MonoBehaviour
{
    private ParallaxState currentState = ParallaxState.MidGround;

    [Header("Parallax Config")]
    [SerializeField] private ParallaxConfig parallaxConfig;

    [Header("Parallax Objects")]
    [SerializeField] private Parallax foreGround;
    [SerializeField] private Parallax midGround;
    [SerializeField] private Parallax backGround;

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
