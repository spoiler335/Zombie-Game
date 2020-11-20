using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideStopWeapon : WeaponsBase
{
    public override void PlayFireAnimation()
    {
        if(bulletsinclip>1)
        {
            animator.CrossFadeInFixedTime("Fire", 0.1f);
        }
        else
        {
            animator.CrossFadeInFixedTime("Default", 0.1f);
        }
    }
}
