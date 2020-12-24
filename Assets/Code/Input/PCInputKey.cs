using System;
using ProjectPrikol;
using UnityEngine;

namespace ProjectPrikol
{
    public class PCInputKey : IInputKeyPress
    {
        public event Action OnKeyPressed = delegate {  };
        private KeyCode _keyCode;

        public PCInputKey(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void GetKey()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                OnKeyPressed.Invoke();
            }
        }
    }
}