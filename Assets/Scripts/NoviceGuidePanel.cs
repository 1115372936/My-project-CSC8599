using UnityEngine;

public class NoviceGuidePanel : MonoBehaviour
{
    private GuidingController guidingController;

    private StepController[] steps;

    private int currentStep;

    private Canvas canvas;

    public static NoviceGuidePanel instance;

    private void Awake()
    {
        instance = this;

        guidingController = transform.GetComponent<GuidingController>();

        InitSteps();

        canvas = transform.GetComponentInParent<Canvas>();
    }

    private void InitSteps()
    {
        steps = new StepController[transform.childCount];

        for (int i = 0; i < transform.childCount; i++)
        {
            steps[i] = transform.GetChild(i).GetComponent<StepController>();
        }
    }

    public void ExcuteStep(int step)
    {
        gameObject.SetActive(true);
        HideAllSteps();
        currentStep = step;

        if(step >= 0 && step < steps.Length)
        {
            steps[step].Excute(guidingController, canvas);
        }
        
    }

    public void NextStep(string eventName)
    {
        if(eventName == steps[this.currentStep].EventName)
        {
            this.currentStep++;
            ExcuteStep(this.currentStep);
        }
    }

    private void HideAllSteps()
    {
        for (int i = 0; i < steps.Length; i++)
        {
            steps[i].gameObject.SetActive(false);
        }
    }
}
