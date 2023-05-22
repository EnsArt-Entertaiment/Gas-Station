using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

namespace Project.CleanLoop
{
    public class PipeAnimation : MonoBehaviour
    {
        [SerializeField] private Material m_HoseMaterial;

        private void Start()
        {
            HoseAnimation();
        }

        private void OnDestroy()
        {
            m_HoseMaterial.SetFloat("_Value", 0f);
        }

        private Tween m_Tween;
        private void HoseAnimation()
        {
            float value = 5f;
            m_Tween = DOTween.To(() => value, x => value = x, -4f, 2f)
                .SetEase(Ease.Linear)
                .OnUpdate(() => m_HoseMaterial.SetFloat("_Value", value))
                .SetLoops(-1, LoopType.Restart);
            m_Tween.Play();
        }
    }
}
