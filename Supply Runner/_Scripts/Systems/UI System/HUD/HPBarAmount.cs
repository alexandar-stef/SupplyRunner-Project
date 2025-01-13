using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HPBarAmount : MonoBehaviour
{
    [SerializeField]
    private PlayerHealth playerHealth;

    [SerializeField]
    private RectTransform maskRect;

    [NonSerialized]
    public float health;

    private float maxWidth;

    void Start()
    {
        maxWidth = maskRect.sizeDelta.x;
    }

    void FixedUpdate()
    {
        health = (float)playerHealth.health / (float)playerHealth.maxHealth;

        maskRect.sizeDelta = new Vector2(
            maxWidth * health,
            maskRect.sizeDelta.y
        );
    }
}
