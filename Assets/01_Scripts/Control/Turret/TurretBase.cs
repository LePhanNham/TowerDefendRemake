
using UnityEngine;

public class TurretBase : MonoBehaviour
{
    private int currentTurretID;
    private int currentTurretLevel;
    private TurretCard turretCard;


    public void Setup()
    {
        
    }

    private void LoadComponents(TurretCard turretCard)
    {
        this.turretCard = turretCard;
    }
}
