using UnityEngine;
using UnityEngine.Tilemaps;

public class Parallax : MonoBehaviour
{
    [Header("Parallax Settings")]
    public Transform cameraTransform; // Reference to the camera
    public float parallaxMultiplier = 0.5f; // Speed of the parallax effect
    public string startingLayerName;

    private Vector3 lastCameraPosition;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }
        lastCameraPosition = cameraTransform.position;
    }

    private void LateUpdate()
    {
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;
        transform.position += new Vector3(deltaMovement.x * parallaxMultiplier, deltaMovement.y * parallaxMultiplier, 0);
        lastCameraPosition = cameraTransform.position;
    }

    public void SetParallaxLayer(ParallaxLayerConfig config, string layerName = "Parallax")
    {
        parallaxMultiplier = config.parallaxMultiplier;
        GetComponentInChildren<Tilemap>().color = config.layerColor;

        int layer = LayerMask.NameToLayer(layerName);
        if (layer == -1)
        {
            Debug.LogError($"Layer '{layerName}' does not exist.");
            return;
        }

        SetLayerRecursively(gameObject, layer);
    }

    public static void SetLayerRecursively(GameObject obj, int layer)
    {
        obj.layer = layer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, layer);
        }
    }
}