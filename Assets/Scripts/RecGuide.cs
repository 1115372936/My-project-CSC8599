using UnityEngine;

public class RecGuide : BaseGuide
{
    protected float width;
    protected float height;

    private float scaleWidth;
    private float scaleHeight;

    public Vector2 WorldToScreenPoint(Canvas canvas, Vector3 world)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);
        Vector2 localPoint;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(),
            screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }

    public override void Guide(Canvas canvas, RectTransform target, TranslateType translateType = TranslateType.Direct, float time = 1)
    {
        base.Guide(canvas, target, translateType, time);

        width = (targetCorners[3].x - targetCorners[0].x) / 2;
        height = (targetCorners[1].y - targetCorners[0].y) / 2;

        material.SetFloat("_SliderX", width);
        material.SetFloat("_SliderY", height);

    }

    public override void Guide(Canvas canvas, RectTransform target, float scale, float time, TranslateType translateType = TranslateType.Direct, float moveTime = 1)
    {
        this.Guide(canvas, target, translateType, moveTime);

        scaleWidth = width * scale;
        scaleHeight = height * scale;

        isScaling = true;
        scaleTimer = 0;
        this.scaleTime = time;
    }

    protected override void Update()
    {
        base.Update();
        if (isScaling)
        {
            material.SetFloat("_SliderX", Mathf.Lerp(scaleWidth, width, scaleTimer));
            material.SetFloat("_SliderY", Mathf.Lerp(scaleHeight, height, scaleTimer));
        }
    }
}
