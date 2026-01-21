using UnityEngine;

[CreateAssetMenu(fileName = "NewMapData", menuName = "Map System/Map Data")]
public class MapData : ScriptableObject
{
    [Header("Identity")]
    public string mapName;
    public string description;
    public int version = 1;

    [Header("Prefab")]
    public GameObject mapPrefab; // assign the map prefab asset (contains WayPoint, markers...)
}
