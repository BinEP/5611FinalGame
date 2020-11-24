using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.Experimental.Rendering.Universal;

public class ShadowAdder : MonoBehaviour
{
    public BoundsInt area;

    void Start()
    {
        var tilemap = GetComponent<Tilemap>();
        GameObject shadowCasterContainer = GameObject.Find("shadow_casters");
        int i = 0;
        foreach (var position in tilemap.cellBounds.allPositionsWithin)
        {
            if (tilemap.GetTile(position) == null)
                continue;

            GameObject newShadowCaster = new GameObject("ShadowCaster2D");
            newShadowCaster.isStatic = true;
            newShadowCaster.transform.SetParent(shadowCasterContainer.transform, false);
            ShadowCaster2D component = newShadowCaster.AddComponent<ShadowCaster2D>();
            newShadowCaster.transform.position = new Vector3(position.x + 0.5f, position.y + 0.5f);

            i++;
        }
    }
}