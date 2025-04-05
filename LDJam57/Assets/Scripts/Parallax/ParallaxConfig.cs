using UnityEngine;

[CreateAssetMenu(fileName = "ParallaxConfig", menuName = "Configs/ParallaxConfig", order = 1)]
public class ParallaxConfig : ScriptableObject
{
    public ParallaxLayerConfig[] layers; // Array of layer configurations
}