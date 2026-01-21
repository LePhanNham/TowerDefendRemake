using UnityEngine;

public class SkillManager : SingletonMono<SkillManager>
{
    private SkillConfig currentSelectedSkill;
    private SkillButton currentButtonCallback; // Để gọi lại start cooldown
    private bool isAiming = false;
    
    [SerializeField] private Transform aimIndicator; 

    private void OnEnable()
    {
        SkillButton.OnSkillSelected += HandleSkillSelection;
    }

    private void OnDisable()
    {
        SkillButton.OnSkillSelected -= HandleSkillSelection;
    }

    private void HandleSkillSelection(SkillConfig config, SkillButton btn)
    {
        currentSelectedSkill = config;
        currentButtonCallback = btn;
        isAiming = true;
        
        if(aimIndicator) 
        {
            aimIndicator.gameObject.SetActive(true);
            aimIndicator.localScale = Vector3.one * config.radius * 2; 
        }
    }

    private void Update()
    {
        if (!isAiming || currentSelectedSkill == null) return;

        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;
        
        if(aimIndicator) aimIndicator.position = mousePos;

        if (Input.GetMouseButtonDown(0))
        {
            CastSkill(mousePos);
        }

        if (Input.GetMouseButtonDown(1))
        {
            CancelAiming();
        }
    }

    private void CastSkill(Vector3 targetPos)
    {
        var data = new SkillSpawnData(targetPos, currentSelectedSkill);
        PoolManager.Instance.Spawn(currentSelectedSkill.poolName, data);

        if (currentButtonCallback != null)
        {
            currentButtonCallback.StartCooldown();
        }
        CancelAiming();
    }

    private void CancelAiming()
    {
        isAiming = false;
        currentSelectedSkill = null;
        currentButtonCallback = null;
        if(aimIndicator) aimIndicator.gameObject.SetActive(false);
    }
}