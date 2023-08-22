using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GuideType
{
    Rect,
    Circle
}

[RequireComponent(typeof(CirGuide))]
[RequireComponent(typeof(RecGuide))]
public class GuidingController : MonoBehaviour, ICanvasRaycastFilter
{

    private CirGuide circleGuide;
    private RecGuide rectGuide;

    public Material rectMat;
    public Material circleMat;

    private Image mask;

    private RectTransform target;

    private GuideType guideType;

    #region Center
    public Vector3 Center
    {
        get
        {
            switch (this.guideType)
            {
                case GuideType.Rect:
                    return rectGuide.Center;
                case GuideType.Circle:
                    return circleGuide.Center;
            }

            return rectGuide.Center;
        }
    }
    #endregion

    private void Awake()
    {
        mask = transform.GetComponent<Image>();

        if (mask == null) { throw new System.Exception("No mask."); }

        if (rectMat == null || circleMat == null) { throw new System.Exception("No materiel."); }

        circleGuide = transform.GetComponent<CirGuide>();
        rectGuide = transform.GetComponent<RecGuide>();

    }

    private void Guide(RectTransform target, GuideType guideType)
    {
        this.target = target;
        this.guideType = guideType;

        switch (guideType)
        {
            case GuideType.Rect:
                mask.material = rectMat;
                break;
            case GuideType.Circle:
                mask.material = circleMat;
                break;
        }
    }

    public void Guide(Canvas canvas, RectTransform target, GuideType guideType, TranslateType translateType = TranslateType.Direct, float time = 1)
    {

        Guide(target, guideType);

        switch (guideType)
        {
            case GuideType.Rect:
                rectGuide.Guide(canvas, target, translateType, time);
                break;
            case GuideType.Circle:
                circleGuide.Guide(canvas, target, translateType, time);
                break;
        }
    }

    public void Guide(Canvas canvas, RectTransform target, GuideType guideType, float scale, float time, TranslateType translateType = TranslateType.Direct, float moveTime = 1)
    {

        Guide(target, guideType);

        switch (guideType)
        {
            case GuideType.Rect:
                rectGuide.Guide(canvas, target, scale, time, translateType, moveTime);
                break;
            case GuideType.Circle:
                circleGuide.Guide(canvas, target, scale, time, translateType, moveTime);
                break;
        }
    }

    public bool IsRaycastLocationValid(Vector2 sp, Camera eventCamera)
    {
        if (target == null) { return false; }

        return !RectTransformUtility.RectangleContainsScreenPoint(target, sp, eventCamera);
    }
}
