using System;

namespace ProjectPrikol
{
    public interface IInputKeyRelease
    {
        event Action OnKeyReleased;
        void GetKeyUp();
    }
}