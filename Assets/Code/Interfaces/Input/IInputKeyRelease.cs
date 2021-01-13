using System;

namespace ProjectPrikol
{
    public interface IInputKeyRelease : IInput
    {
        event Action OnKeyReleased;
        void GetKeyUp();
    }
}