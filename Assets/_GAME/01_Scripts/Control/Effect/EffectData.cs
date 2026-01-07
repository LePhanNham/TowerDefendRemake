using UnityEngine;

public class EffectData : IPoolableData
{
    public Vector3 position { get; private set; }
    public Quaternion rotation { get; private set; }

    public EffectData(Vector3 pos, Quaternion rot)
    {
        this.position = pos;
        this.rotation = rot;
    }
    
    public EffectData(Vector3 pos)
    {
        this.position = pos;
        this.rotation = Quaternion.identity;
    }
}