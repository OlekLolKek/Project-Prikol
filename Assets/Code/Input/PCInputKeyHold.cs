using System;
using UnityEngine;

namespace ProjectPrikol
{
    public class PCInputKeyHold : IInputKeyHold
    {
        public event Action<bool> OnKeyHeld = delegate(bool isKeyPressed) {  };
        private readonly KeyCode _keyCode;

        public PCInputKeyHold(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void GetKey()
        {
            if (Input.GetKey(_keyCode))
            {
                OnKeyHeld.Invoke(true);
            }
            else
            {
                OnKeyHeld.Invoke(false);
            }
        }
    }
}