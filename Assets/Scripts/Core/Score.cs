using System;
using UnityEngine;

namespace Trell.Flexus_TZ.ScoreSystem
{
    public class Score : MonoBehaviour
    {
        public event Action Changed;
    
        public int Value { get; private set; }

        public void Add(int value)
        {
            if(value <= 0)
            {
                return;
            }
            Value += value;
            Changed?.Invoke();
        }

        public void Reset()
        {
            Value = 0;
            Changed?.Invoke();
        }
    }
}