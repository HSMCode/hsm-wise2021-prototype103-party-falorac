using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanTexture : MonoBehaviour
{
    // choose materials to pan
    public Material panningMaterial;
    public float speed = 0;
    private float offset = 0;
    private bool isPanning = true;

    // Update is called once per frame
    void Update()
    {
        if (isPanning) {
            offset = Time.time * speed;
            panningMaterial.mainTextureOffset = new Vector2(offset, panningMaterial.mainTextureOffset.y);
        }
    }

    void StopPanning() {
        isPanning = false;
        offset = panningMaterial.mainTextureOffset.x;
    }
}
