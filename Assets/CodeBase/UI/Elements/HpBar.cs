using System;
using UnityEngine;
using UnityEngine.UI;

namespace SimpleRPG.UI.Elements
{
    public class HpBar : MonoBehaviour
    {
        [SerializeField] private Image _fillerImage;

        public void SetValue(float current, float max) => _fillerImage.fillAmount = current / max;
    }
}