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
    [TextArea] public string instructionText; // Ví dụ: "Bấm vào ô đất này!"
    
    [Header("Điều kiện hoàn thành")]
    public TutorialActionType requiredAction; // Hành động cần làm để qua bước này
    
    [Header("Object cần Highlight")]
    public string targetID; // ID của object cần chỉ mũi tên vào (VD: "Node_01", "Btn_Archer")
}