using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuelAnimationController : MonoBehaviour
{

    [SerializeField] Animator FuelAnimator;

    public void FuelAnimation()
    {
        FuelAnimator.SetTrigger("Plug");
    }

}
