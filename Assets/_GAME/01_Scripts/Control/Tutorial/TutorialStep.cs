using UnityEngine;

public enum TutorialActionType
{
    None,
    SelectNode,     
    BuildTower,     
    StartWave,      
    EnemyKilled,
    ShowTurretPopup,
    UpgradeTurret,
    SellTurret
}

[CreateAssetMenu(fileName = "New Tutorial Step", menuName = "Tutorial/Step")]
public class TutorialStep : ScriptableObject
{
    [Header("Cấu hình bước")]
    public string stepID;
    [TextArea] public string instructionText; 
    
    [Header("Điều kiện hoàn thành")]
    public TutorialActionType requiredAction; 
    
    [Header("Object cần Highlight")]
    public string targetID; 
}