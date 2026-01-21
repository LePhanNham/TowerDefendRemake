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
        currentMapInstance.transform.position = Vector3.zero;
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
