using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapFollow : MonoBehaviour
{
    public Transform player;

    [Header("Position")]
    public RectTransform mapImage;
    public Transform worldAnchor1;
    public Transform worldAnchor2;

    [Header("Rotation")]
    public RectTransform arrowImage;

    Rect mapRect;
    int mapWidth, mapHeight;

    void Start()
    {
        mapRect.x = worldAnchor1.position.x;
        mapRect.y = worldAnchor1.position.z;

        mapRect.width = worldAnchor2.position.x - mapRect.x;
        mapRect.height = worldAnchor2.position.z - mapRect.y;

        // TODO: get these values from the map image instead of hardcoding them
        mapWidth = 820;
        mapHeight = 820; 
    }

    void FixedUpdate()
    {
        Vector2 posOnMap = MapCoords();
        mapImage.localPosition = new Vector3(
            posOnMap.x,
            posOnMap.y,
            0
        );

        arrowImage.eulerAngles = new Vector3(
            0,
            0,
            -player.eulerAngles.y
        );
    }

    Vector2 MapCoords()
    {
        Vector2 result = Vector2.zero;

        // adjust mapRect to match minimap

        Vector2 posOnMap = new Vector2(
            player.position.x - mapRect.x - mapRect.width / 2,
            player.position.z - mapRect.y - mapRect.height / 2
        );

        // normalize position on map

        posOnMap.x /= mapRect.width / 2;
        posOnMap.y /= mapRect.height / 2;

        result.x = -posOnMap.x * mapWidth / 2;
        result.y = -posOnMap.y * mapHeight / 2;

        return result;
    }
}
