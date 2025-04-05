using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class ParallaxManager : MonoBehaviour
{
    private enum ParallaxState
    {
        ForeGround,
        MidGround,
        BackGround
    }

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
        midGround.SetParallaxLayer(GetParallaxLayers("mid"));
        backGround.SetParallaxLayer(GetParallaxLayers("back"));
    }

    public void OnParallaxChangeToFore()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("mid"));
        midGround.SetParallaxLayer(GetParallaxLayers("back"));
        backGround.SetParallaxLayer(GetParallaxLayers("backback"));
    }

    public void OnParallaxChangeToBack()
    {
        foreGround.SetParallaxLayer(GetParallaxLayers("forefore"));
        midGround.SetParallaxLayer(GetParallaxLayers("fore"));
        backGround.SetParallaxLayer(GetParallaxLayers("mid"));
    }

    public void OnJump(InputValue value)
    {
        Debug.Log("Jump pressed");
        switch (currentState)
        {
            case ParallaxState.ForeGround:
                OnParallaxChangeToMid();
                currentState = ParallaxState.MidGround;
                break;
            case ParallaxState.MidGround:
                OnParallaxChangeToBack();
                currentState = ParallaxState.BackGround;
                break;
            case ParallaxState.BackGround:
                OnParallaxChangeToFore();
                currentState = ParallaxState.ForeGround;
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

    // Update is called once per frame
    void Update()
    {
        
    }
}
