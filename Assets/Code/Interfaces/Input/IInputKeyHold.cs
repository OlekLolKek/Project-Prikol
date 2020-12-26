using System;

namespace ProjectPrikol
{
    public interface IInputKeyHold
    {
        event Action<bool> OnKeyHeld;
        void GetKey();
    }
}