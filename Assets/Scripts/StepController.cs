using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepController : MonoBehaviour
{
    public string EventName;

    public RectTransform target;
    public GuideType guideType = GuideType.Rect;

    public float scale = 1;
    public float scaleTime = 1;

    public TranslateType translateType = TranslateType.Direct;
    public float transTime = 1;

    public RectTransform targetPos;

    public void Excute(GuidingController guidingController, Canvas canvas)
    {
        gameObject.SetActive(true);

        guidingController.Guide(canvas, target, guideType, scale, scaleTime, translateType, transTime);

        targetPos.localPosition = guidingController.Center;
    }
}
