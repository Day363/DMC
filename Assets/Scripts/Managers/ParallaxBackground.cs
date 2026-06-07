using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class ParallaxBackground : MonoBehaviour
{
    public ParallaxCamera parallaxCamera;
    List<MonoBehaviour> parallaxLayers = new List<MonoBehaviour>();

    void Start()
    {
        if (parallaxCamera == null)
            parallaxCamera = Camera.main.GetComponent<ParallaxCamera>();

        if (parallaxCamera != null)
            parallaxCamera.onCameraTranslate += Move;

        SetLayers();
    }

    void SetLayers()
    {
        parallaxLayers.Clear();
        for (int i = 0; i < transform.childCount; i++)
        {
            var child = transform.GetChild(i);
            ParallaxLayer layer = child.GetComponent<ParallaxLayer>();
            InfiniteParallaxLayer inf = child.GetComponent<InfiniteParallaxLayer>();
            if (layer != null) parallaxLayers.Add(layer);
            else if (inf != null) parallaxLayers.Add(inf);
        }
    }

    void Move(float delta)
    {
        foreach (MonoBehaviour layer in parallaxLayers)
        {
            if (layer is ParallaxLayer pl) pl.Move(delta);
            else if (layer is InfiniteParallaxLayer il) il.Move(delta);
        }
    }
}