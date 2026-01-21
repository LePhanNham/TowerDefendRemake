using UnityEngine;
using UnityEngine.UI;

public class SkillButton : MonoBehaviour
{
    [Header("Setup")]
    [SerializeField] private SkillConfig skillConfig;
    [SerializeField] private Button btnComp;
    [SerializeField] private Image imgCooldown;
    [SerializeField] private Image imgIcon;

    private float cooldownTimer = 0;
    private bool isReady = true;

    public static System.Action<SkillConfig, SkillButton> OnSkillSelected;

    private void Start()
    {
        if (skillConfig != null)
        {
            imgIcon.sprite = skillConfig.icon;
            btnComp.onClick.AddListener(SelectSkill);
        }
    }

    private void Update()
    {
        if (!isReady)
        {
            cooldownTimer -= Time.deltaTime;
            imgCooldown.fillAmount = cooldownTimer / skillConfig.cooldown;

            if (cooldownTimer <= 0)
            {
                isReady = true;
                btnComp.interactable = true;
                imgCooldown.fillAmount = 0;
            }
        }
    }

    private void SelectSkill()
    {
        if (!isReady) return;
        // Bắn tin hiệu cho Manager biết: "Skill này đang được chọn, hãy vào chế độ ngắm!"
        OnSkillSelected?.Invoke(skillConfig, this);
    }

    // Hàm này sẽ được Manager gọi khi skill đã được tung ra thành công
    public void StartCooldown()
    {
        isReady = false;
        cooldownTimer = skillConfig.cooldown;
        btnComp.interactable = false;
    }
}