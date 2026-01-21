using UnityEngine;

public class MapManager : SingletonMono<MapManager>
{
    [SerializeField] private MapLoader mapLoader;
    [SerializeField] private MapData defaultMap;

    public MapData DefaultMap => defaultMap;

    protected override void Awake()
    {
        base.Awake();
        if (mapLoader == null)
            mapLoader = GetComponent<MapLoader>();
    }

    public void LoadMap(MapData data)
    {
        var toLoad = data != null ? data : defaultMap;
        if (toLoad == null)
        {
            Debug.LogWarning("MapManager: no MapData available to load.");
            return;
        }

        if (mapLoader == null)
        {
            Debug.LogWarning("MapManager: MapLoader not assigned or found in scene.");
            return;
        }

        mapLoader.LoadMap(toLoad);
    }

    public void UnloadCurrentMap()
    {
        if (mapLoader != null)
            mapLoader.UnloadMap();
    }

    public void SetDefaultMap(MapData map)
    {
        defaultMap = map;
    }
}
