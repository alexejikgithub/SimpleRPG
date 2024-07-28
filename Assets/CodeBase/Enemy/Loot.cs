using System;
using UnityEngine;

namespace SimpleRPG.Enemy
{
    [Serializable]
    public class Loot
    {
        [SerializeField] private int _value;

        public int Value => _value;

        public void SetValue(int value)
        {
            _value = value;
        }
    }
}