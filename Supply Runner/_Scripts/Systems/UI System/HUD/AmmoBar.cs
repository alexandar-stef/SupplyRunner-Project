using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoBar : MonoBehaviour
{
    public Transform gunParent;

    public RectTransform rect;


    private RaycastWeapon gun;
    private float maxWidth, baseX;

    private float ratio;
    private float reloadCounter;

    private bool equipped;
    // Start is called before the first frame update

    bool GetGun()
    {
        try
        {
            gun = gunParent.GetChild(2).GetComponent<RaycastWeapon>();
            return true;
        }
        catch (Exception)
        {
            return false;
        }
    }

    void Setup(bool doRatio)
    {

        if (doRatio) ratio = (float)gun.bulletsLeft / (float)gun.trueMaxAmmo;
        else
        {
            baseX = rect.localPosition.x;
            maxWidth = rect.rect.width;
        }
    }

    void Start()
    {
        equipped = GetGun();
        Setup(equipped);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Start();
        if (equipped)
        {
            if (!gun.isReloading()) 
            {
                reloadCounter = 0;
                ratio = (float)gun.bulletsLeft / (float)gun.trueMaxAmmo;
            }
            else
            {
                reloadCounter += Time.fixedDeltaTime;
                ratio = reloadCounter / gun.reloadTime;

                float ammoRatio = (float)gun.bulletsLeft / (float)gun.trueMaxAmmo;
                float magRatio = (float)Math.Min(gun.magazineSize, 30) / (float)gun.trueMaxAmmo;
                ratio *= (1.0f - ammoRatio) - (1.0f - magRatio);
                ratio += ammoRatio;
            }

            
            rect.sizeDelta = new Vector2(maxWidth * ratio, rect.sizeDelta.y);
            rect.anchoredPosition = new Vector2(
                baseX - (ratio * maxWidth / 2) + (maxWidth / 2),
                rect.localPosition.y
            );
        }
    }
}
