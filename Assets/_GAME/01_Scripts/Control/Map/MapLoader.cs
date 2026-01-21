using UnityEngine;

public class MapLoader : MonoBehaviour
{
    [SerializeField] private Transform mapRoot; // where to parent instantiated map
    private GameObject currentMapInstance;

    public GameObject CurrentMapInstance => currentMapInstance;

    public void LoadMap(MapData data)
    {
        if (data == null)
        {
            Debug.LogWarning("MapData is null");
            return;
        }

        if (currentMapInstance != null)
            UnloadMap();

        if (data.mapPrefab == null)
        {
            Debug.LogWarning($"Map prefab not assigned for {data.mapName}");
            return;
        }

        Transform parent = mapRoot != null ? mapRoot : this.transform;
        currentMapInstance = Instantiate(data.mapPrefab, parent);

        // Find WayPoint in the instantiated map and register it to LevelManager
        var wp = currentMapInstance.GetComponentInChildren<WayPoint>();
        if (wp != null && LevelManager.Instance != null)
        {
            LevelManager.Instance.SetWayPoint(wp);
        }

        // Additional registrations (markers, spawn points) can be added here
    }

    public void UnloadMap()
    {
        if (currentMapInstance != null)
        {
            Destroy(currentMapInstance);
            currentMapInstance = null;
        }
    }
}
