using UnityEngine;

public class TutorialTarget : MonoBehaviour
{
    public string ID;

    public void SetID(string newID)
    {
        if (TutorialManager.Instance == null) return;
        if (TutorialManager.Instance.IsTutorialFinished) return; 
        this.ID = newID;
        TutorialManager.Instance.RegisterTarget(this); 
    }
}