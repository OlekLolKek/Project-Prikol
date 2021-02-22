using System;
using UnityEngine;


namespace ProjectPrikol
{
    public class PCInputKeyUp : IInputKeyRelease
    {
        public event Action OnKeyReleased = delegate {  };
        private KeyCode _keyCode;

        public PCInputKeyUp(KeyCode keyCode)
        {
            _keyCode = keyCode;
        }
        
        public void GetKeyUp()
        {
            if (Input.GetKeyUp(_keyCode))
            {
                OnKeyReleased.Invoke();
            }
        }
    }
}