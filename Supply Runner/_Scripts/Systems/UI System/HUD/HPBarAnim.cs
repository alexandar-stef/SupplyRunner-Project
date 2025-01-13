using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBar : MonoBehaviour
{
    public string label; // used entirely for managing the multiple instances of this script
                         // on the main HP bar parent. Yes, multiple instances of the same component
                         // on one gameobject is bad, but we shouldnt run into any problems in 
                         // this specific case.

    [Header("Properties")]

    [SerializeField]
    float animSpeed = 5.0f;

    [SerializeField]
    RectTransform barRect;
    [SerializeField]
    HPColor hpColor;

    float barDiff = 1180.0f;// this number is the point at which the beginning of the 
                            // second bar lines up with the beginning of the first bar


    void FixedUpdate()
    {
        barRect.Translate(-animSpeed * ((hpColor.contextFactor - 1)/5 + 1), 0, 0);

        if (barRect.localPosition.x < -barDiff) 
        {
            barRect.Translate(barDiff / barRect.localScale.x, 0, 0);
        }
    }
}
