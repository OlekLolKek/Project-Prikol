using System;
using UnityEngine;

namespace ProjectPrikol
{
    public class PCInputAxis : IInputAxisChange
    {
        public event Action<float> OnAxisChanged;
        public string _axis;

        public PCInputAxis(string axis)
        {
            _axis = axis;
        }

        public void GetAxis()
        {
            OnAxisChanged.Invoke(Input.GetAxis(_axis));
        }
    }
}