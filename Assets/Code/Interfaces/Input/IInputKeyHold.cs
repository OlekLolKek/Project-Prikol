using System;

namespace ProjectPrikol
{
    public interface IInputKeyHold : IInput
    {
        event Action<bool> OnKeyHeld;
        void GetKey();
    }
}