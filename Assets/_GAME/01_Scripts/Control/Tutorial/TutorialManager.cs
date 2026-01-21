using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TutorialManager : SingletonMono<TutorialManager>
{

    [Header("Components")]
    public GameObject tutorialPanel;   // Panel chứa text hướng dẫn
    public TextMeshProUGUI instructionText;
    public GameObject handPointer;     // Mũi tên/Bàn tay chỉ dẫn

    [Header("Data")]
    public List<TutorialStep> steps;   // Kéo thả các file ScriptableObject vào đây
    
    private int currentStepIndex = -1;
    private TutorialStep currentStep;
    
    private readonly Dictionary<string, Transform> registeredTargets = new Dictionary<string, Transform>();
    private bool isTutorialFinished = false;
    public bool IsTutorialFinished => isTutorialFinished;
    private void Start()
    {
        // Do not auto-start tutorial on scene load. Tutorial will be started
        // explicitly when the game/level starts via `BeginTutorial()`.
        tutorialPanel.SetActive(false);
        handPointer.SetActive(false);
        if (PlayerPrefs.GetInt("TutorialCompleted", 0) != 0)
        {
            isTutorialFinished = true;
        }
    }

    // Start tutorial when the game/level actually begins
    public void BeginTutorial()
    {
        if (isTutorialFinished) return;

        if (PlayerPrefs.GetInt("TutorialCompleted", 0) == 0)
        {
            // small delay to allow UI/map to settle
            Invoke(nameof(NextStep), 0.25f);
        }
        else
        {
            tutorialPanel.SetActive(false);
            handPointer.SetActive(false);
            isTutorialFinished = true;
        }
    }

    public void RegisterTarget(TutorialTarget target)
    {
        if (!registeredTargets.ContainsKey(target.ID))
        {
            registeredTargets.Add(target.ID, target.transform);
        }
    }


    public void NextStep()
    {
        currentStepIndex++;

        // Hết tutorial
        if (currentStepIndex >= steps.Count)
        {
            EndTutorial();
            return;
        }

        currentStep = steps[currentStepIndex];
        UpdateUI();
    }

    [Header("Tutorial Settings")]
    public Vector3 pointerOffset = new Vector3(0, 150, 0); 
    public float pokeDistance = 30f; 
    private Tween typingTween;

    public void SetInstruction(string text)
    {
        typingTween?.Kill();

        instructionText.text = "";
        instructionText.maxVisibleCharacters = 0;
        instructionText.text = text;

        typingTween = DOTween.To(
            () => instructionText.maxVisibleCharacters,
            x => instructionText.maxVisibleCharacters = x,
            text.Length,
            0.8f // thời gian gõ
        );
    }
    void UpdateUI()
    {
        tutorialPanel.SetActive(true);
        SetInstruction(currentStep.instructionText);

        if (!string.IsNullOrEmpty(currentStep.targetID) && registeredTargets.ContainsKey(currentStep.targetID))
        {
            Transform targetTf = registeredTargets[currentStep.targetID];
            handPointer.SetActive(true);

            Vector3 targetScreenPos;
            if (targetTf is RectTransform)
                targetScreenPos = targetTf.position;
            else
            {
                targetScreenPos = Camera.main.WorldToScreenPoint(targetTf.position);
                targetScreenPos.z = 0;
            }

            Vector3 endPos = targetScreenPos + pointerOffset;


            Vector3 startPos = endPos;
            startPos.y += pokeDistance; 

            // 4. ANIMATION
            handPointer.transform.DOKill();
        
            // Đặt tay ở trên cao
            handPointer.transform.position = startPos;

            // Lao xuống dưới
            handPointer.transform
                .DOMove(endPos, 0.5f)
                .SetEase(Ease.InOutSine)
                .SetLoops(-1, LoopType.Yoyo); 
        }
        else
        {
            handPointer.transform.DOKill();
            handPointer.SetActive(false);
        }
    }

    public void ReportAction(TutorialActionType action)
    {
        if (currentStep == null) return;
        if (isTutorialFinished) return;
        if (currentStep.requiredAction == action)
        {
            NextStep();
        }
    }

    void EndTutorial()
    {
        tutorialPanel.SetActive(false);
        handPointer.SetActive(false);
        PlayerPrefs.SetInt("TutorialCompleted", 1);
    }
}