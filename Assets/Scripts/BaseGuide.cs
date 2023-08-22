using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TranslateType
{
    Direct,
    Slow
}

[RequireComponent(typeof(Image))]
public class BaseGuide : MonoBehaviour
{
    protected Material material;

    protected Vector3 center;

    protected RectTransform target;

    protected Vector3[] targetCorners = new Vector3[4];

    #region ScaleChange

    protected float scaleTimer;
    protected float scaleTime;
    protected bool isScaling;
    #endregion

    #region CenterPointChange

    private Vector3 startCenter;

    private float centerTimer;
    private float centerTime;
    private bool isMoving;

    #endregion

    public Vector3 Center
    {
        get
        {
            if (material == null) { return Vector3.zero; }
            return material.GetVector("_Center");
        }
    }

    protected virtual void Update()
    {
        if (isScaling)
        {
            scaleTimer += Time.deltaTime * 1 / scaleTime;
            if (scaleTimer >= 1)
            {
                scaleTimer = 0;
                isScaling = false;
            }
        }

        if (isMoving)
        {
            centerTimer += Time.deltaTime * 1 / centerTime;

            material.SetVector("_Center", Vector3.Lerp(startCenter, center, centerTimer));

            if (centerTimer >= 1)
            {
                centerTimer = 0;
                isMoving = false;
            }
        }

    }

    public virtual void Guide(Canvas canvas, RectTransform target, TranslateType translateType = TranslateType.Direct, float time = 1)
    {
        material = transform.GetComponent<Image>().material;

        this.target = target;


        if (target != null)
        {
            target.GetWorldCorners(targetCorners);

            for (int i = 0; i < targetCorners.Length; i++)
            {
                targetCorners[i] = WorldToScreenPoint(canvas, targetCorners[i]);
            }

            center.x = targetCorners[0].x + (targetCorners[3].x - targetCorners[0].x) / 2;
            center.y = targetCorners[0].y + (targetCorners[1].y - targetCorners[0].y) / 2;


            switch (translateType)
            {
                case TranslateType.Direct:
                    material.SetVector("_Center", center);
                    break;
                case TranslateType.Slow:

                    startCenter = material.GetVector("_Center");

                    isMoving = true;
                    centerTimer = 0;
                    centerTime = time;
                    break;
            }
        }
        else
        {
            center = Vector3.zero;
            targetCorners[0] = new Vector3(-2000, -2000, 0);
            targetCorners[1] = new Vector3(-2000, 2000, 0);
            targetCorners[2] = new Vector3(2000, 2000, 0);
            targetCorners[3] = new Vector3(2000, -2000, 0);
        }

    }

    public virtual void Guide(Canvas canvas, RectTransform target, float scale, float time, TranslateType translateType = TranslateType.Direct, float moveTime = 1)
    {

    }

    public Vector2 WorldToScreenPoint(Canvas canvas, Vector3 world)
    {
        Vector2 screenPoint = RectTransformUtility.WorldToScreenPoint(canvas.worldCamera, world);
        Vector2 localPoint;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.GetComponent<RectTransform>(), screenPoint, canvas.worldCamera, out localPoint);
        return localPoint;
    }

}