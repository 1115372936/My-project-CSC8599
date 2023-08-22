using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuidingPanel : MonoBehaviour
{
    GuidingController guideController;

    Canvas canvas;

    private void Awake()
    {
        canvas = transform.GetComponentInParent<Canvas>();
    }

    void Start()
    {
        guideController = transform.GetComponent<GuidingController>();
        //guideController.Guide(canvas, GameObject.Find("Canvas").GetComponent<RectTransform>(), GuideType.Rect, 2, 0.5f);

        //Invoke("Step1", 4f);
    }

    void Update()
    {

    }
}
