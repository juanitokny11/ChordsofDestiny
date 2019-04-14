using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MenuAnim : MonoBehaviour
{
    public bool Anim;
    public float endValue;
    public float endValue2;
    private RectTransform trans;
    private void Start()
    {
        trans = GetComponent<RectTransform>();
    }
    void Update()
    {
        if (Anim)
            trans.DOAnchorPosX(endValue, 1.0f, true);
        else if (!Anim)
            trans.DOAnchorPosX(endValue2, 1.0f, true);
    }       
}
