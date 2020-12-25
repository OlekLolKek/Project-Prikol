using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class PCInputKeyDown : IInputKeyPress
    {
        public event Action OnKeyPressed = delegate {  };
        private KeyCode _keyCode;

        public PCInputKeyDown(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void GetKeyDown()
        {
            if (Input.GetKeyDown(_keyCode))
            {
                OnKeyPressed.Invoke();
            }
        }
    }
}