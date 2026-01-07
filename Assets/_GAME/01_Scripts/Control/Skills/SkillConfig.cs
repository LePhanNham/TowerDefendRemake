using UnityEngine;

[CreateAssetMenu(fileName = "NewSkillConfig", menuName = "Skill System/Skill Config")]
public class SkillConfig : ScriptableObject
{
    [Header("UI Info")]
    public string skillName;
    public Sprite icon;
    public string description;

    [Header("System")]
    public string poolName; 
    public float cooldown;

    [Header("Combat Stats")]
    public int powerValue;   
    public float radius;     
    public float speed;      
}