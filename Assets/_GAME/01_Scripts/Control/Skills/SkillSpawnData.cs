using UnityEngine;

public class SkillSpawnData : IPoolableData
{
    public Vector3 targetPosition; 
    public SkillConfig config;     

    public SkillSpawnData(Vector3 target, SkillConfig cfg)
    {
        this.targetPosition = target;
        this.config = cfg;
    }
}